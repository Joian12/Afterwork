using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts._EditRoom
{
    public class RoomTab : MonoBehaviour
    {
        [SerializeField] private Button _wallpaperButton;
        [SerializeField] private Button _floorTileButton;
        [SerializeField] private Button _applianceButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _openButton;
    
        private RoomEditModeManager _roomEditModeManager => RoomEditModeManager.Instance;
    
        private void OnEnable()
        {
            this._wallpaperButton.onClick.AddListener(() => this._roomEditModeManager.SetWallpaperMode());
            this._floorTileButton.onClick.AddListener(() => this._roomEditModeManager.SetFloorTileMode());
            this._applianceButton.onClick.AddListener(ApplianceEditMode);
            this._openButton.onClick.AddListener(OpenTab);
            this._closeButton.onClick.AddListener(CloseTab);
        }
        
        private void OnDisable()
        {
            this._wallpaperButton.onClick.RemoveListener(() => this._roomEditModeManager.SetWallpaperMode());
            this._floorTileButton.onClick.RemoveListener(() => this._roomEditModeManager.SetFloorTileMode());
            this._applianceButton.onClick.RemoveListener(ApplianceEditMode);
            this._openButton.onClick.RemoveListener(OpenTab);
            this._closeButton.onClick.RemoveListener(CloseTab);
        }
        
        private void CloseTab()
        {
            this._roomEditModeManager.ExitEditMode();
            this._openButton.gameObject.SetActive(true);
            this._closeButton.gameObject.SetActive(false);
        }

        private void OpenTab()
        {
            this._roomEditModeManager.OpenEditMode();
            this._openButton.gameObject.SetActive(false);
            this._closeButton.gameObject.SetActive(true);
        }
    
        private void ApplianceEditMode()
        {
            this._roomEditModeManager.SetApplianceMode();
        
            EquipmentSelectionPopUpData equipmentSelectionPopUpData = new()
            {
                InteriorObjectType = InteriorObjectType.Equipment,
                PopUpTitle = string.Empty
            };
        
            PopUpController.Instance.SetCurrentPopUp(PopUpType.EquipmentSelectionPopUp);
            PopUpController.Instance.PopulateContent(equipmentSelectionPopUpData);
            PopUpController.Instance.ShowPopUp();
        }
    }

    internal class EquipmentSelectionPopUpData : IPopUpData
    {
        public InteriorObjectType InteriorObjectType { get; set; }
        public string PopUpTitle { get; set; }
    }
}