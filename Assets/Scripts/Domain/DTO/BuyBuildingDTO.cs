using Domain.Models;

namespace Domain.DTO
{
public struct BuyBuildingDTO
{
    public readonly (int x, int y) Position;
    public readonly BuildingType Type;
}
}