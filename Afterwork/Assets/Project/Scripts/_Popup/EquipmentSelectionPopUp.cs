using System.Collections.Generic;
using UnityEngine;

public sealed class EquipmentSelectionPopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private GameObject _popUpGameObject;
    [SerializeField] private GameObject _selectionPrefab;
    [SerializeField] private Transform _container;
    
    public PopUpType PopUpType => PopUpType.EquipmentSelectionPopUp;
    
    private Dictionary<int, EquipmentTileAsset> _equipmentAssetsDictionary = new();
    
    private void Awake()
    {
        _popUpGameObject.SetActive(false);
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
            
            if (_equipmentAssetsDictionary.ContainsKey(equipmentAsset.TileID) == false)
            {
                _equipmentAssetsDictionary.Add(equipmentAsset.TileID, (EquipmentTileAsset)equipmentAsset);
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
        foreach (var equipmentAsset in _equipmentAssetsDictionary.Values)
        {
            var selection = Instantiate(_selectionPrefab, _container);
            
            var equipment = selection.GetComponent<EquipmentTileAsset>();
            
            CreateSelection(equipmentAsset);
        }
    }
    
    private void CreateSelection(EquipmentTileAsset equipmentAsset)
    {
        var selection = Instantiate(_selectionPrefab, _container);
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

