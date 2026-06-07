using UnityEngine;
using UnityEngine.Serialization;

public class TileSurface : MonoBehaviour
{
    [SerializeField] private int _tilePos;
    [SerializeField] private Renderer _texture;

    public TileSurfaceType _TileSurfaceType;
    
    private MaterialPropertyBlock _block;

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private void Awake()
    {
        this._block = new MaterialPropertyBlock();
    }

    public void SetSurfaceTile(TextureTileAsset asset)
    {
        this._texture.GetPropertyBlock(this._block);
        this._block.SetTexture(MainTex, asset.Sprite);
        this._texture.SetPropertyBlock(this._block);
        
        TilePersistenceManager.Instance.AddTileSurfaceData(new TileSurfaceData
        {
            TileId = asset.TileID,
            TilePos = _tilePos,
            TileName = asset.TileName,
            TileSurfaceType = _TileSurfaceType
        });
    }
    
    public int TilePos => _tilePos;
}