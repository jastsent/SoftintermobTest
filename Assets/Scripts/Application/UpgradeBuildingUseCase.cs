using System;
using Domain.DTO;
using Domain.Models;
using MessagePipe;
using Repositories;
using VContainer;
using VContainer.Unity;

namespace Application
{
public class UpgradeBuildingUseCase : IInitializable, IDisposable, IMessageHandler<RemoveBuildingDTO>
{
    [Inject] private readonly GridModel _gridModel;
    [Inject] private readonly ResourcesModel _resourcesModel;
    [Inject] private readonly ISubscriber<RemoveBuildingDTO> _subscriber;
    [Inject] private readonly BuildingModelFactory _factory;
    [Inject] private readonly ConfigRepository _configRepository;
    private IDisposable _disposable;
    
    public void Initialize()
    {
        _disposable = _subscriber.Subscribe(this);
    }
    
    public void Handle(RemoveBuildingDTO message)
    {
        ref var buildingModel = ref _gridModel.Grid[message.Position.x, message.Position.y];
        if (buildingModel.BuildingType == BuildingType.Empty) 
            return;
        
        var levels = _configRepository.GetBuildingLevels(buildingModel.BuildingType);
        if (buildingModel.Level >= levels.Count - 1)
            return;
        
        var cost = levels[buildingModel.Level].Cost;
        if (_resourcesModel.Gold < cost)
            return;
        
        _resourcesModel.Gold -= cost;
        buildingModel.Level++;
    }

    public void Dispose()
    {
        _disposable.Dispose();
        _disposable = null;
    }
}
}