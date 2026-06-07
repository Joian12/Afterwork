using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileTextureSurfacePopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private GameObject _popUpGameObject;
    [SerializeField] private TileSurfaceType _tileSurfaceType;
    
    [SerializeField] private GameObject _tilePrefab;
    
    // Example of assets manager
    [SerializeField] private Transform _container;
    
    private Dictionary<TileSurfaceType, Dictionary<int, TextureTileAsset>> _tileAssetsDictionary = new();
    [SerializeField] private Button _closeButton;

    public PopUpType PopUpType => PopUpType.TextureSelectionPopUp;

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
            if (_tileAssetsDictionary.ContainsKey(tile.TileSurfaceType) == false)
            {
                _tileAssetsDictionary.Add(tile.TileSurfaceType, new Dictionary<int, TextureTileAsset>());
            }

            if (_tileAssetsDictionary[tile.TileSurfaceType].ContainsKey(tile.TileID) == false)
            {
                _tileAssetsDictionary[tile.TileSurfaceType].Add(tile.TileID, tile);
            }
            else
            {
                Debug.LogWarning($"Duplicate TileID {tile.TileID} found for SurfaceType {tile.TileSurfaceType}!");
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

    public void PopulateContent(IPopUpData popUpData, ref TileSurface tileSurface)
    {
        ResetContent();

        if (_tileAssetsDictionary.TryGetValue(popUpData.SurfaceType, out var targetedTiles))
        {
            foreach (var tileAsset in targetedTiles.Values)
            {
                CreateTileUI(tileAsset, ref tileSurface);
            }
        }
        
        //  foreach (var innerDict in _tileAssetsDictionary.Values)
       //  {
       //      foreach (var tileAsset in innerDict.Values)
       //      {
       //          CreateTileUI(tileAsset);
       //      }
       //  }
       //  */
    }

    private void CreateTileUI(TextureTileAsset asset, ref TileSurface tileSurface)
    {
        GameObject tile = Instantiate(_tilePrefab, _container);
        TileSurfaceController tileSurfaceController = tile.GetComponent<TileSurfaceController>();
        tileSurfaceController.SetTileSurface(asset, ref tileSurface);
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