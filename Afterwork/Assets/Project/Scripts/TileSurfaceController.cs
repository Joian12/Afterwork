using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileSurfaceController : MonoBehaviour
{
    [SerializeField] private RawImage _tileSurfaceImage;
    [SerializeField] private TextMeshProUGUI _tileSurfaceText;
    
    public void SetTileSurface(TextureTileAsset tileAsset)
    {
        _tileSurfaceImage.texture = tileAsset.Sprite;
        _tileSurfaceText.text = tileAsset.TileSurfaceType.ToString();
    }
}