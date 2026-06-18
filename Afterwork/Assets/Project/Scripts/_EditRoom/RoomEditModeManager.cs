using UnityEngine;
using DG.Tweening;

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

    [SerializeField] private RectTransform _parentPanel;
    [SerializeField] private WallpaperEditHandler _wallpaperHandler;
    [SerializeField] private FloorTileEditHandler _floorTileHandler;
    [SerializeField] private AppliancePlacementHandler _applianceHandler;

    private EditMode _currentMode = EditMode.None;
    public EditMode CurrentMode => _currentMode;

    private void Awake()
    {
        Instance = this;
        DisableAll();
        
        this._parentPanel.anchoredPosition = new Vector2(-80f, 59.762f);
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

    public void OpenEditMode()
    {
        this._parentPanel.DOAnchorPosX(80, 0.2f,true);
    }

    public void ExitEditMode()
    {
        SetMode(EditMode.None);
        this._parentPanel.DOAnchorPosX(- 80, 0.2f, true);
    }
    
    private void DisableAll()
    {
        _wallpaperHandler.Disable();
        _floorTileHandler.Disable();
        _applianceHandler.Disable();
    }
}
