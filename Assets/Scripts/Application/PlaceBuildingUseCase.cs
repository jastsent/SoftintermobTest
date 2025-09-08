using System;
using Domain.DTO;
using Domain.Models;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Application
{
public class PlaceBuildingUseCase : IInitializable, IDisposable, IMessageHandler<PlaceBuildingDTO>
{
    [Inject] private readonly GridModel _gridModel;
    [Inject] private readonly ISubscriber<PlaceBuildingDTO> _placeBuildingSubscriber;
    [Inject] private readonly BuildingModelFactory _factory;
    private IDisposable _disposable;
    
    public void Initialize()
    {
        _disposable = _placeBuildingSubscriber.Subscribe(this);
    }
    
    public void Handle(PlaceBuildingDTO message)
    {
        var buildingModel = _gridModel.Grid[message.Position.x, message.Position.y];

        if (buildingModel.BuildingType is BuildingType.Empty)
        {
            buildingModel = _factory.Create(message.Type);
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