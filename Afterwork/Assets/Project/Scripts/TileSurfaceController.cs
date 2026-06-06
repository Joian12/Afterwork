using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileSurfaceController : MonoBehaviour
{
    [SerializeField] private RawImage _tileSurfaceImage;
    [SerializeField] private TextMeshProUGUI _tileSurfaceText;
    
    [SerializeField] private Button _chooseButton;
    private TextureTileAsset _cachedTileAsset;
    private TileSurface _cachedTileSurface;

    private void OnEnable()
    {
        _chooseButton.onClick.AddListener(OnClickChooseTile);
    }

    private void OnDisable()
    {
        _chooseButton.onClick.RemoveListener(OnClickChooseTile);
    }

    public void SetTileSurface(TextureTileAsset tileAsset, ref TileSurface tileSurface)
    {
        _cachedTileAsset = tileAsset;
        _tileSurfaceImage.texture = tileAsset.Sprite;
        _tileSurfaceText.text = tileAsset.TileName;
        _cachedTileSurface = tileSurface;
    }

    private void OnClickChooseTile()
    {
        Debug.Log($"Chose tile {_cachedTileAsset.TileName}");
        _cachedTileSurface.SetWallpaper(_cachedTileAsset.Sprite);
    }
}