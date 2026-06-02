using UnityEngine;

public class PopUpController : MonoBehaviour
{
    [SerializeField] private IPopUp _currentPopUp;
    
    [SerializeField] private TileTextureSurfacePopUp _textureSelectionPopUp;
    
    public static PopUpController Instance;
    
    private void Awake()
    {
        Instance = this;
        
        _currentPopUp?.ResetContent();
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
    }

    public void PopulateContent()
    {
        _currentPopUp?.PopulateContent();
    }
}
