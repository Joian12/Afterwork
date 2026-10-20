using UnityEngine;

public class PopUpController : MonoBehaviour
{
    public static PopUpController Instance;

    [SerializeField] private TileTextureSurfacePopUp _textureSelectionPopUp;
    private IPopUp _currentPopUp;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowPopUp()
    {
        this._currentPopUp?.Show();
    }

    public void HidePopUp()
    {
        this._currentPopUp?.Hide();
    }

    // only one type for now
    public void SetCurrentPopUp(PopUpType popUpType)
    {
        switch (popUpType)
        {
            case PopUpType.EquipmentSelectionPopUp:
            case PopUpType.WallPaperSelectionPopUp:
            case PopUpType.FloorSelectionPopUp:
            default:
                this._currentPopUp = this._textureSelectionPopUp;
                break;
        }

        this._currentPopUp.Init();
    }

    public void PopulateContent(IPopUpData popUpData)
    {
        this._currentPopUp?.PopulateContent(popUpData);
    }
}