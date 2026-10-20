using System;
using System.Collections.Generic;

[Serializable]
public class PlacedApplianceData
{
    public int TileID;
    public string PrefabName;
    public float CellX;
    public float CellY;
    public float CellZ;
}

[Serializable]
public class ApplianceSaveData
{
    public List<PlacedApplianceData> Appliances = new();
}