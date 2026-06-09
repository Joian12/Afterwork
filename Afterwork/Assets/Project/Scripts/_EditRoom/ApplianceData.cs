using System.Collections.Generic;

[System.Serializable]
public class PlacedApplianceData
{
    public string PrefabName;
    public int CellX;
    public int CellY;
    public int CellZ;
}

[System.Serializable]
public class ApplianceSaveData
{
    public List<PlacedApplianceData> Appliances = new();
}
