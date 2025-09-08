using System;
using Domain.DTO;
using Domain.Models;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Application
{
public class RemoveBuildingUseCase : IInitializable, IDisposable, IMessageHandler<RemoveBuildingDTO>
{
    [Inject] private readonly GridModel _gridModel;
    [Inject] private readonly ISubscriber<RemoveBuildingDTO> _subscriber;
    [Inject] private readonly BuildingModelFactory _factory;
    private IDisposable _disposable;
    
    public void Initialize()
    {
        _disposable = _subscriber.Subscribe(this);
    }
    
    public void Handle(RemoveBuildingDTO message)
    {
        var buildingModel = _gridModel.Grid[message.Position.x, message.Position.y];

        if (buildingModel.BuildingType != BuildingType.Empty)
        {
            buildingModel = _factory.Create(BuildingType.Empty);
            buildingModel.Position = message.Position;
            _gridModel.Grid[message.Position.x, message.Position.y] = buildingModel;
        }
    }

    public void Dispose()
    {
        _disposable.Dispose();
        _disposable = null;
    }
}
}