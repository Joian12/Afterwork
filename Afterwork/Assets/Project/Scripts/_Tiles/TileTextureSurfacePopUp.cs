using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileTextureSurfacePopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private GameObject _popUpGameObject;
    [SerializeField] private GameObject _selectionPrefab;

    // Example of assets manager
    [SerializeField] private Transform _container;

    [SerializeField] private Button _closeButton;

    private readonly Dictionary<InteriorObjectType, Dictionary<int, TileAsset>> _tileAssetsDictionary = new();

    private void Awake()
    {
        this._popUpGameObject.SetActive(false);
    }

    private void OnEnable()
    {
        this._closeButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        this._closeButton.onClick.RemoveListener(Hide);
    }

    public PopUpType PopUpType => PopUpType.WallPaperSelectionPopUp;

    public void Init()
    {
        this._tileAssetsDictionary.Clear();

        var tileAssets = RoomManager.Instance.GetTileAssets();

        foreach (var tile in tileAssets)
        {
            if (this._tileAssetsDictionary.ContainsKey(tile.interiorObjectType) == false)
                this._tileAssetsDictionary.Add(tile.interiorObjectType, new Dictionary<int, TileAsset>());

            if (this._tileAssetsDictionary[tile.interiorObjectType].ContainsKey(tile.TileID) == false)
                this._tileAssetsDictionary[tile.interiorObjectType].Add(tile.TileID, tile);
            else
                Debug.LogWarning($"Duplicate TileID {tile.TileID} found for SurfaceType {tile.interiorObjectType}!");
        }
    }

    public void Show()
    {
        this._popUpGameObject.SetActive(true);
    }

    public void Hide()
    {
        this._popUpGameObject.SetActive(false);
    }

    public void PopulateContent(IPopUpData popUpData)
    {
        ResetContent();

        if (this._tileAssetsDictionary.TryGetValue(popUpData.InteriorObjectType, out var targetedTiles))
            foreach (var tileAsset in targetedTiles.Values)
                CreateTileUI(tileAsset);
    }

    public void ResetContent()
    {
        for (var i = 0; i < this._container.childCount; i++) Destroy(this._container.GetChild(i).gameObject);

        this._popUpGameObject.SetActive(false);
    }

    private void CreateTileUI(TileAsset assets)
    {
        var tile = Instantiate(this._selectionPrefab, this._container);
        var tileSelectionController = tile.GetComponent<TileSelectionController>();
        tileSelectionController.SetTileSurface(assets);
    }
}