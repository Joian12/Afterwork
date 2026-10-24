using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppliancePersistenceManager : MonoBehaviour
{
    private readonly string _savePath = "ApplianceData.json";
    private ApplianceSaveData _saveData = new();
    public static AppliancePersistenceManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        LoadAppliances();
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused) SaveAppliances();
    }

    private void OnApplicationQuit()
    {
        SaveAppliances();
    }

    public void AddOrUpdateAppliance(PlacedApplianceData data)
    {
        var index = this._saveData.Appliances.FindIndex(x =>
            x.CellX == data.CellX &&
            x.CellY == data.CellY &&
            x.CellZ == data.CellZ);

        if (index != -1)
            this._saveData.Appliances[index] = data;
        else
            this._saveData.Appliances.Add(data);
    }

    public void RemoveAppliance(PlacedApplianceData data)
    {
        this._saveData.Appliances.RemoveAll(x =>
            x.CellX == data.CellX &&
            x.CellY == data.CellY &&
            x.CellZ == data.CellZ);
    }

    public List<PlacedApplianceData> GetAppliances()
    {
        return this._saveData.Appliances;
    }

    public void SaveAppliances()
    {
        var json = JsonUtility.ToJson(this._saveData, true);
        File.WriteAllText(this._savePath, json);
    }

    public void LoadAppliances()
    {
        if (!File.Exists(this._savePath))
            return;

        var json = File.ReadAllText(this._savePath);
        this._saveData = JsonUtility.FromJson<ApplianceSaveData>(json) ?? new ApplianceSaveData();
    }
}