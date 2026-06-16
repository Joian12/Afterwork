using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class EquipmentSelectionPopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private GameObject _popUpGameObject;
    [SerializeField] private GameObject _selectionPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _closeButton;
    
    public PopUpType PopUpType => PopUpType.EquipmentSelectionPopUp;
    
    private Dictionary<int, EquipmentTileAsset> _equipmentAssetsDictionary = new();
    
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

    public void Init()
    {
        var equipmentAssets = RoomManager.Instance.GetTileAssets();
        
        foreach (TileAsset equipmentAsset in equipmentAssets)
        {
            if (equipmentAsset is not EquipmentTileAsset)
            {
                continue;
            }
            
            if (this._equipmentAssetsDictionary.ContainsKey(equipmentAsset.TileID) == false)
            {
                this._equipmentAssetsDictionary.Add(equipmentAsset.TileID, (EquipmentTileAsset)equipmentAsset);
            }
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
        foreach (var equipmentAsset in this._equipmentAssetsDictionary.Values)
        {
            var selection = Instantiate(this._selectionPrefab, this._container);
            
            var equipment = selection.GetComponent<EquipmentTileAsset>();
            
            CreateSelection(equipmentAsset);
        }
    }
    
    private void CreateSelection(EquipmentTileAsset equipmentAsset)
    {
        var selection = Instantiate(this._selectionPrefab, this._container);
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

