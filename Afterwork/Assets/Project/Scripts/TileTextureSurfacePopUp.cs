using System.Collections.Generic;
using UnityEngine;

public class TileTextureSurfacePopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private GameObject _popUpGameObject;
    [SerializeField] private TileSurfaceType _tileSurfaceType;
    
    [SerializeField] private GameObject _tilePrefab;
    
    //example of assets manager
    [SerializeField] private List<TextureTileAsset> _tileAssets;
    [SerializeField] private Transform _container;
    
    private Dictionary<int, GameObject> _tileSurfaceControllers = new Dictionary<int, GameObject>();

    public PopUpType PopUpType => PopUpType.TextureSelectionPopUp;

    //add animation later.
    public void Show()
    {
        _popUpGameObject.SetActive(true);
    }

    public void Hide()
    {
        _popUpGameObject.SetActive(false);
    }

    //addresasble or resource and pooling
    public void PopulateContent()
    {
        if(_tileSurfaceControllers.Count == _tileAssets.Count)
        {
            return;
        }
        
        for (int i = 0; i < _tileAssets.Count; i++)
        {
            GameObject tile = Instantiate(_tilePrefab, _container);
            TileSurfaceController tileSurfaceController = tile.GetComponent<TileSurfaceController>();
            tileSurfaceController.SetTileSurface(_tileAssets[i]);
            _tileSurfaceControllers.Add(i, tile);
        }
    }

    public void ResetContent()
    {
        foreach (var tileSurfaceController in _tileSurfaceControllers.Values)
        {
            Destroy(tileSurfaceController.gameObject);
        }
        
        _popUpGameObject.SetActive(false);
    }
}

public interface IPopUp
{
    PopUpType PopUpType { get; }
    void Show();
    void Hide();
    void PopulateContent();
    void ResetContent();
}

public enum PopUpType
{
    TextureSelectionPopUp,
    EquipmentSelectionPopUp,
}