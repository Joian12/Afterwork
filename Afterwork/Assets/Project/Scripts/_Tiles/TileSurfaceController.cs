using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileSurfaceController : MonoBehaviour
{
    [SerializeField] private int _position;
    
    [SerializeField] private RawImage _tileSurfaceImage;
    [SerializeField] private TextMeshProUGUI _tileSurfaceText;
    
    [SerializeField] private Button _chooseButton;
    private TileAsset _cachedTileAsset;
    
    public static Action<EquipmentTileAsset> OnEquipmentSelect;

    private void OnEnable()
    {
        this._chooseButton.onClick.AddListener(OnSelectItem);
    }

    private void OnDisable()
    {
        this._chooseButton.onClick.RemoveListener(OnSelectItem);
    }

    public void SetTileSurface(TileAsset tileAsset)
    {
        this._cachedTileAsset = tileAsset;
        this._tileSurfaceImage.texture = tileAsset.Sprite;
        this._tileSurfaceText.text = tileAsset.TileName;
    }

    private void OnSelectItem()
    {
        Debug.Log($"Chose tile {this._cachedTileAsset.TileName}");

        switch (this._cachedTileAsset.interiorObjectType)
        {
            case InteriorObjectType.Wall:
            case InteriorObjectType.Floor:
                RoomManager.SelectedTileSurface.SetSurfaceTile(_cachedTileAsset);
                break;
            case InteriorObjectType.Equipment:
                OnEquipmentSelect?.Invoke((EquipmentTileAsset)this._cachedTileAsset);
                break;
            case InteriorObjectType.Door:
                break;
        }
        
        //should show pop up to confirm choice
    }
    
    public int Position => _position;
}