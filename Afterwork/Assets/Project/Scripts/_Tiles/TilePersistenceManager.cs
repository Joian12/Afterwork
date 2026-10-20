using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class TilePersistenceManager : MonoBehaviour
{
    private readonly string _tileSurfaceDataPath = "TileSurfaceData.json";

    private List<TileSurfaceData> _tileSurfaceDataList = new();
    public static TilePersistenceManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        LoadTileSurfaceData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) SaveTileSurfaceData();
    }

    private void OnApplicationQuit()
    {
        SaveTileSurfaceData();
    }

    public void AddTileSurfaceData(TileSurfaceData tileSurfaceData)
    {
        // just update it if it exists already
        var index = this._tileSurfaceDataList.FindIndex(x => x.TilePos == tileSurfaceData.TilePos);
        if (index != -1)
        {
            this._tileSurfaceDataList[index] = tileSurfaceData;
            return;
        }

        this._tileSurfaceDataList.Add(tileSurfaceData);
    }

    private void SaveTileSurfaceData()
    {
        TileSurfaceDataWrapper tileSurfaceDataWrapper = new();
        tileSurfaceDataWrapper.TileSurfaceDataList = this._tileSurfaceDataList;

        var json = JsonUtility.ToJson(tileSurfaceDataWrapper, true);
        File.WriteAllText(this._tileSurfaceDataPath, json);
    }

    public void LoadTileSurfaceData()
    {
        if (File.Exists(this._tileSurfaceDataPath) == false) return;

        var json = File.ReadAllText(this._tileSurfaceDataPath);
        var tileSurfaceDataWrapper = JsonUtility.FromJson<TileSurfaceDataWrapper>(json);
        this._tileSurfaceDataList = tileSurfaceDataWrapper.TileSurfaceDataList;
    }

    public List<TileSurfaceData> GetTileSurfaceDataList()
    {
        return this._tileSurfaceDataList;
    }
}

[Serializable]
public class TileSurfaceData
{
    public int TileId;
    public int TilePos;
    public string TileName;

    [FormerlySerializedAs("TileSurfaceType")]
    public InteriorObjectType interiorObjectType;
}

[Serializable]
public class TileSurfaceDataWrapper
{
    public List<TileSurfaceData> TileSurfaceDataList;
}