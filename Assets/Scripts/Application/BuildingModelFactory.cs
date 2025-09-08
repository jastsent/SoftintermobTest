using Domain.Models;

namespace Application
{
public class BuildingModelFactory
{
    public BuildingModel Create(BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.House:
                return new()
                {
                    BuildingType = BuildingType.House,
                    Position = (0,0),
                    Level = 0
                };
            case BuildingType.Farm:
                return new()
                {
                    BuildingType = BuildingType.Farm,
                    Position = (0,0),
                    Level = 0
                };
            case BuildingType.Mine:
                return new()
                {
                    BuildingType = BuildingType.Mine,
                    Position = (0,0),
                    Level = 0
                };
            default:
                return new()
                {
                    BuildingType = BuildingType.Empty,
                    Position = (0,0),
                    Level = 0
                };
        }
    }
}
}