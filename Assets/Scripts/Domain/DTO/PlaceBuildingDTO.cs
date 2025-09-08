using Domain.Models;

namespace Domain.DTO
{
public struct PlaceBuildingDTO
{
    public readonly (int x, int y) Position;
    public readonly BuildingType Type;
    public readonly int Level;
}
}