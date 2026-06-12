using System.Collections.Generic;

[System.Serializable]
public class PlacedApplianceData
{
    public int TileID;
    public string PrefabName;
    public float CellX;
    public float CellY;
    public float CellZ;
}

[System.Serializable]
public class ApplianceSaveData
{
    public List<PlacedApplianceData> Appliances = new();
}
