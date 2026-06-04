using System.Collections.Generic;
using UnityEngine;

public class TileTextureSurfacePopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private GameObject _popUpGameObject;
    [SerializeField] private TileSurfaceType _tileSurfaceType;
    
    [SerializeField] private GameObject _tilePrefab;
    
    // Example of assets manager
    [SerializeField] private List<TextureTileAsset> _tileAssets;
    [SerializeField] private Transform _container;
    
    private Dictionary<TileSurfaceType, Dictionary<int, TextureTileAsset>> _tileAssetsDictionary = new();

    public PopUpType PopUpType => PopUpType.TextureSelectionPopUp;

    public void Init()
    {
        _tileAssetsDictionary.Clear();

        foreach (var tile in _tileAssets)
        {
            // 1. If the SurfaceType doesn't exist yet, create the inner dictionary
            if (_tileAssetsDictionary.ContainsKey(tile.TileSurfaceType) == false)
            {
                _tileAssetsDictionary.Add(tile.TileSurfaceType, new Dictionary<int, TextureTileAsset>());
            }

            // 2. Add the asset to the nested inner dictionary using its TileID
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

    public void PopulateContent(IPopUpData popUpData)
    {
        // Clear old UI elements before generating new ones to avoid duplicating clones
        ResetContent();

        // If you only want to populate tiles matching THIS popup's specific surface type:
        if (_tileAssetsDictionary.TryGetValue((TileSurfaceType)popUpData.PopUpType, out var targetedTiles))
        {
            foreach (var tileAsset in targetedTiles.Values)
            {
                CreateTileUI(tileAsset);
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

    private void CreateTileUI(TextureTileAsset asset)
    {
        GameObject tile = Instantiate(_tilePrefab, _container);
        TileSurfaceController tileSurfaceController = tile.GetComponent<TileSurfaceController>();
        tileSurfaceController.SetTileSurface(asset);
    }

    public void ResetContent()
    {
        // Properly destroy instantiated UI game objects in the container
        for (int i = 0; i < _container.childCount; i++)
        {
            Destroy(_container.GetChild(i).gameObject);
        }

        _popUpGameObject.SetActive(false);
    }
}