using UnityEngine;

public class TileSurface : MonoBehaviour
{
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    [SerializeField] private int _tilePos;
    [SerializeField] private Renderer _texture;

    public InteriorObjectType interiorObjectType;

    private MaterialPropertyBlock _block;

    public int TilePos => this._tilePos;

    private void Awake()
    {
        this._block = new MaterialPropertyBlock();
    }

    public void SetSurfaceTile(TileAsset asset)
    {
        this._texture.GetPropertyBlock(this._block);
        this._block.SetTexture(MainTex, asset.Sprite);
        this._texture.SetPropertyBlock(this._block);

        TilePersistenceManager.Instance.AddTileSurfaceData(new TileSurfaceData
        {
            TileId = asset.TileID,
            TilePos = this._tilePos,
            TileName = asset.TileName,
            interiorObjectType = this.interiorObjectType
        });
    }

    public void SetTilePosition(int pos)
    {
        this._tilePos = pos;
    }
}