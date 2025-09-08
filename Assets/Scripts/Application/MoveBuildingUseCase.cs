using System;
using Domain.DTO;
using Domain.Models;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Application
{
public class MoveBuildingUseCase : IInitializable, IDisposable, IMessageHandler<MoveBuildingDTO>
{
    [Inject] private readonly GridModel _gridModel;
    [Inject] private readonly ISubscriber<MoveBuildingDTO> _subscriber;
    private IDisposable _disposable;
    
    public void Initialize()
    {
        _disposable = _subscriber.Subscribe(this);
    }
    
    public void Handle(MoveBuildingDTO message)
    {
        var initialBuildingModel = _gridModel.Grid[message.InitialPosition.x, message.InitialPosition.y];
        if (initialBuildingModel.BuildingType != BuildingType.Empty)
        {
            var targetBuildingModel = _gridModel.Grid[message.TargetPosition.x, message.TargetPosition.y];
            targetBuildingModel.Position = message.InitialPosition;
            initialBuildingModel.Position = message.TargetPosition;
            _gridModel.Grid[message.InitialPosition.x, message.InitialPosition.y] = targetBuildingModel;
            _gridModel.Grid[message.TargetPosition.x, message.TargetPosition.y] = initialBuildingModel;
        }
    }

    public void Dispose()
    {
        _disposable.Dispose();
        _disposable = null;
    }
}
}