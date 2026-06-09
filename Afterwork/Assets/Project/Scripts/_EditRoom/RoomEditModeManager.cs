using UnityEngine;

public class RoomEditModeManager : MonoBehaviour
{
    public static RoomEditModeManager Instance { get; private set; }

    public enum EditMode
    {
        None,
        Wallpaper,
        FloorTile,
        Appliance
    }
    
    [SerializeField] private WallpaperEditHandler _wallpaperHandler;
    [SerializeField] private FloorTileEditHandler _floorTileHandler;
    [SerializeField] private AppliancePlacementHandler _applianceHandler;

    private EditMode _currentMode = EditMode.None;
    public EditMode CurrentMode => _currentMode;

    private void Awake()
    {
        Instance = this;
        DisableAll();
    }

    private void SetMode(EditMode mode)
    {
        if (_currentMode == mode)
        {
            DisableAll();
            _currentMode = EditMode.None;
            return;
        }

        _currentMode = mode;
        DisableAll();

        switch (mode)
        {
            case EditMode.Wallpaper: _wallpaperHandler.Enable();  break;
            case EditMode.FloorTile: _floorTileHandler.Enable();  break;
            case EditMode.Appliance: _applianceHandler.Enable();  break;
            case EditMode.None:
            default:
                break;
        }
    }

    public void SetWallpaperMode()
    {
        SetMode(EditMode.Wallpaper);
    }

    public void SetFloorTileMode()
    {
        SetMode(EditMode.FloorTile);
    }

    public void SetApplianceMode()
    {
        SetMode(EditMode.Appliance);
    }   

    public void ExitEditMode()
    {
        SetMode(EditMode.None);
    }
    
    private void DisableAll()
    {
        _wallpaperHandler.Disable();
        _floorTileHandler.Disable();
        _applianceHandler.Disable();
    }
}
