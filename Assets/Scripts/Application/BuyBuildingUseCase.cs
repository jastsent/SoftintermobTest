using System;
using Domain.DTO;
using Domain.Models;
using MessagePipe;
using Repositories;
using VContainer;
using VContainer.Unity;

namespace Application
{
public class  BuyBuildingUseCase : IInitializable, IDisposable, IMessageHandler<BuyBuildingDTO>
{
    [Inject] private readonly ISubscriber<BuyBuildingDTO> _subscriber;
    [Inject] private readonly ConfigRepository _configRepository;
    [Inject] private readonly GridModel _gridModel;
    [Inject] private readonly ResourcesModel _resourcesModel;
    [Inject] private readonly BuildingModelFactory _factory;
    private IDisposable _disposable;

    public void Initialize()
    {
        _disposable = _subscriber.Subscribe(this);
    }
    
    public void Handle(BuyBuildingDTO message)
    {
        var buildingModel = _gridModel.Grid[message.Position.x, message.Position.y];
        if (buildingModel.BuildingType is not BuildingType.Empty)
            return;
        
        var buildingLevelConfig = _configRepository.GetBuildingLevels(message.Type)[0];
        if (_resourcesModel.Gold < buildingLevelConfig.Cost)
            return;
        
        _resourcesModel.Gold -= buildingLevelConfig.Cost;
        var newBuildingModel = _factory.Create(message.Type);
        newBuildingModel.Position = message.Position;
        _gridModel.Grid[message.Position.x, message.Position.y] = newBuildingModel;
    }

    public void Dispose()
    {
        _disposable.Dispose();
        _disposable = null;
    }
}
}