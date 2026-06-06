using UnityEngine;

public class PopUpController : MonoBehaviour
{
    private IPopUp _currentPopUp;
    
    [SerializeField] private TileTextureSurfacePopUp _textureSelectionPopUp;
    
    public static PopUpController Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    public void ShowPopUp()
    {
        _currentPopUp?.Show();
    }

    public void HidePopUp()
    {
        _currentPopUp?.Hide();
    }
    
    // only one type for now
    public void SetCurrentPopUp(PopUpType popUpType)
    {
        switch (popUpType)
        {
            case PopUpType.TextureSelectionPopUp:
            case PopUpType.EquipmentSelectionPopUp:
            default:
                _currentPopUp = _textureSelectionPopUp;
                break;
        }
        
        _currentPopUp.Init();
    }

    public void PopulateContent(IPopUpData popUpData, ref TileSurface tileSurface)
    {
        _currentPopUp?.PopulateContent(popUpData, ref tileSurface);
    }
}
