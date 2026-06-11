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
    
        private RoomEditModeManager _roomEditModeManager => RoomEditModeManager.Instance;
    
        private void OnEnable()
        {
            _wallpaperButton.onClick.AddListener(() => _roomEditModeManager.SetWallpaperMode());
            _floorTileButton.onClick.AddListener(() => _roomEditModeManager.SetFloorTileMode());
            _applianceButton.onClick.AddListener(ApplianceEditMode);
            _closeButton.onClick.AddListener(() => _roomEditModeManager.ExitEditMode());
        }

        private void OnDisable()
        {
            _wallpaperButton.onClick.RemoveListener(() => _roomEditModeManager.SetWallpaperMode());
            _floorTileButton.onClick.RemoveListener(() => _roomEditModeManager.SetFloorTileMode());
            _applianceButton.onClick.RemoveListener(ApplianceEditMode);
            _closeButton.onClick.RemoveListener(() => _roomEditModeManager.ExitEditMode());
        }
    
        private void ApplianceEditMode()
        {
            _roomEditModeManager.SetApplianceMode();
        
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