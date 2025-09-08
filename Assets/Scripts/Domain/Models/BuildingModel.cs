namespace Domain.Models
{
public struct BuildingModel
{
    public BuildingType BuildingType;
    public (int x, int y) Position;
    public int Level;
}
}