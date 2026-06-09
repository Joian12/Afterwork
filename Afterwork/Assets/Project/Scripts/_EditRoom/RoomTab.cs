using UnityEngine;
using UnityEngine.UI;

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
        _applianceButton.onClick.AddListener(() => _roomEditModeManager.SetApplianceMode());
        _closeButton.onClick.AddListener(() => _roomEditModeManager.ExitEditMode());
    }

    private void OnDisable()
    {
        _wallpaperButton.onClick.RemoveListener(() => _roomEditModeManager.SetWallpaperMode());
        _floorTileButton.onClick.RemoveListener(() => _roomEditModeManager.SetFloorTileMode());
        _applianceButton.onClick.RemoveListener(() => _roomEditModeManager.SetApplianceMode());
        _closeButton.onClick.RemoveListener(() => _roomEditModeManager.ExitEditMode());
    }
}