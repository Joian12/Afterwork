using DG.Tweening;
using UnityEngine;

public class RoomEditModeManager : MonoBehaviour
{
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

    public static RoomEditModeManager Instance { get; private set; }
    public EditMode CurrentMode { get; private set; } = EditMode.None;

    private void Awake()
    {
        Instance = this;
        DisableAll();

        this._parentPanel.anchoredPosition = new Vector2(-80f, 59.762f);
    }

    private void SetMode(EditMode mode)
    {
        if (this.CurrentMode == mode)
        {
            DisableAll();
            this.CurrentMode = EditMode.None;
            return;
        }

        this.CurrentMode = mode;
        DisableAll();

        switch (mode)
        {
            case EditMode.Wallpaper:
                this._wallpaperHandler.Enable();
                break;
            case EditMode.FloorTile:
                this._floorTileHandler.Enable();
                break;
            case EditMode.Appliance:
                this._applianceHandler.Enable();
                break;
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
        this._parentPanel.DOAnchorPosX(80, 0.2f, true);
    }

    public void ExitEditMode()
    {
        SetMode(EditMode.None);
        this._parentPanel.DOAnchorPosX(-80, 0.2f, true);
    }

    private void DisableAll()
    {
        this._wallpaperHandler.Disable();
        this._floorTileHandler.Disable();
        this._applianceHandler.Disable();
    }
}