using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppliancePersistenceManager : MonoBehaviour
{
    public static AppliancePersistenceManager Instance { get; private set; }

    private readonly string _savePath = "ApplianceData.json";
    private ApplianceSaveData _saveData = new();

    private void Awake()
    {
        Instance = this;
        LoadAppliances();
    }
    
    public void AddOrUpdateAppliance(PlacedApplianceData data)
    {
        int index = _saveData.Appliances.FindIndex(x =>
            x.CellX == data.CellX &&
            x.CellY == data.CellY &&
            x.CellZ == data.CellZ);

        if (index != -1)
            _saveData.Appliances[index] = data;
        else
            _saveData.Appliances.Add(data);
    }

    public void RemoveAppliance(PlacedApplianceData data)
    {
        _saveData.Appliances.RemoveAll(x =>
            x.CellX == data.CellX &&
            x.CellY == data.CellY &&
            x.CellZ == data.CellZ);
    }

    public List<PlacedApplianceData> GetAppliances()
    {
        return _saveData.Appliances;
    }

    public void SaveAppliances()
    {
        string json = JsonUtility.ToJson(_saveData, true);
        File.WriteAllText(_savePath, json);
    }

    public void LoadAppliances()
    {
        if (!File.Exists(_savePath))
            return;

        string json = File.ReadAllText(_savePath);
        _saveData = JsonUtility.FromJson<ApplianceSaveData>(json) ?? new ApplianceSaveData();
    }


    private void OnApplicationQuit()
    {
        SaveAppliances();
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            SaveAppliances();
        }
    }
}
