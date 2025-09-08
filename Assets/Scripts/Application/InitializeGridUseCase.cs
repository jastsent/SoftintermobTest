using Domain.Models;
using Repositories;
using VContainer;
using VContainer.Unity;

namespace Application
{
public class InitializeGridUseCase : IInitializable
{
    [Inject] private readonly ConfigRepository _configRepository;
    [Inject] private readonly GridModel _gridModel;
    [Inject] private readonly BuildingModelFactory _factory;

    public void Initialize()
    {
        _gridModel.Grid = new BuildingModel[_configRepository.FieldSizeX, _configRepository.FieldSizeY];

        for (int i = 0; i < _gridModel.Grid.GetLength(0); i++)
        {
            for (int j = 0; j < _gridModel.Grid.GetLength(1); j++)
            {
                var buildingModel = _factory.Create(BuildingType.Empty);
                buildingModel.Position = (i, j);
                _gridModel.Grid[i, j] = buildingModel;
            }
        }
    }
}
}