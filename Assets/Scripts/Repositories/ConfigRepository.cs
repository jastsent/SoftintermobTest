using System;
using System.Collections.Generic;
using Domain.Models;
using TriInspector;
using UnityEngine;

namespace Repositories
{
[CreateAssetMenu(fileName = "ConfigRepository", menuName = "Settings/Config Repository")]
[DeclareHorizontalGroup("FieldParams")]
public class ConfigRepository : ScriptableObject
{
    [field:SerializeField, Group("FieldParams"), Min(0)] public int FieldSizeX { get; private set; }
    [field:SerializeField, Group("FieldParams"), Min(0)] public int FieldSizeY { get; private set; }
    [field:SerializeField, Min(0)] public int StartGold { get; private set; }
    
    [SerializeField] private List<BuildingLevel> _houseLevels;
    [SerializeField] private List<BuildingLevel> _farmLevels;
    [SerializeField] private List<BuildingLevel> _mineLevels;
    
    public IReadOnlyList<BuildingLevel> HouseLevels => _houseLevels;
    public IReadOnlyList<BuildingLevel> FarmLevels => _farmLevels;
    public IReadOnlyList<BuildingLevel> MineLevels => _mineLevels;

    public IReadOnlyList<BuildingLevel> GetBuildingLevels(BuildingType buildingType)
    {
        return buildingType switch
        {
            BuildingType.House => HouseLevels,
            BuildingType.Farm => FarmLevels,
            BuildingType.Mine => MineLevels,
            _ => throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null)
        };
    }
    
    [Serializable]
    [DeclareHorizontalGroup("LevelParams")]
    public struct BuildingLevel
    {
        [field:SerializeField, Group("LevelParams"), Min(0)]  public int Cost { get; private set; }
        [field:SerializeField, Group("LevelParams"), Min(0)]  public int Income { get; private set; }
    }
}
}