using UnityEngine;

public class TileSurface : MonoBehaviour
{
    [SerializeField] private Renderer _texture;

    private MaterialPropertyBlock _block;

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private void Awake()
    {
        this._block = new MaterialPropertyBlock();
    }

    public void SetWallpaper(Texture texture)
    {
        this._texture.GetPropertyBlock(this._block);
        this._block.SetTexture(MainTex, texture);
        this._texture.SetPropertyBlock(this._block);
    }
}