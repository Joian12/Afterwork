using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileTextureSurfacePopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private GameObject _popUpGameObject;
    [SerializeField] private GameObject _selectionPrefab;
    
    // Example of assets manager
    [SerializeField] private Transform _container;
    
    private Dictionary<InteriorObjectType, Dictionary<int, TileAsset>> _tileAssetsDictionary = new();
    
    [SerializeField] private Button _closeButton;

    public PopUpType PopUpType => PopUpType.WallPaperSelectionPopUp;

    private void Awake()
    {
        _popUpGameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
    }
    
    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(Hide);
    }

    public void Init()
    {
        _tileAssetsDictionary.Clear();
        
        var tileAssets = RoomManager.Instance.GetTileAssets();
        
        foreach (var tile in tileAssets)
        {
            if (_tileAssetsDictionary.ContainsKey(tile.interiorObjectType) == false)
            {
                _tileAssetsDictionary.Add(tile.interiorObjectType, new Dictionary<int, TileAsset>());
            }

            if (_tileAssetsDictionary[tile.interiorObjectType].ContainsKey(tile.TileID) == false)
            {
                _tileAssetsDictionary[tile.interiorObjectType].Add(tile.TileID, tile);
            }
            else
            {
                Debug.LogWarning($"Duplicate TileID {tile.TileID} found for SurfaceType {tile.interiorObjectType}!");
            }
        }
    }
    
    public void Show()
    {
        _popUpGameObject.SetActive(true);
    }

    public void Hide()
    {
        _popUpGameObject.SetActive(false);
    }

    public void PopulateContent(IPopUpData popUpData)
    {
        ResetContent();

        if (_tileAssetsDictionary.TryGetValue(popUpData.InteriorObjectType, out var targetedTiles))
        {
            foreach (var tileAsset in targetedTiles.Values)
            {
                CreateTileUI(tileAsset);
            }
        }
    }

    private void CreateTileUI(TileAsset assets)
    {
        GameObject tile = Instantiate(_selectionPrefab, _container);
        TileSurfaceController tileSurfaceController = tile.GetComponent<TileSurfaceController>();
        tileSurfaceController.SetTileSurface(assets);
    }

    public void ResetContent()
    {
        for (int i = 0; i < _container.childCount; i++)
        {
            Destroy(_container.GetChild(i).gameObject);
        }

        _popUpGameObject.SetActive(false);
    }
}