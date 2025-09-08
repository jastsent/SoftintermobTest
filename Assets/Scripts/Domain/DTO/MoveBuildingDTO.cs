namespace Domain.DTO
{
public struct MoveBuildingDTO
{
    public readonly (int x, int y) InitialPosition;
    public readonly (int x, int y) TargetPosition;
}
}