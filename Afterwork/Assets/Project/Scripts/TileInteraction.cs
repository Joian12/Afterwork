using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _maxDistance = 100;
    
    private bool _isPopUpEditorOpen = false;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OpenTileSurfacePopUpEditor();
        }
    }

    private void OpenTileSurfacePopUpEditor()
    {
        Vector3 mousePosition = Input.mousePosition;
        
        mousePosition.z = _camera.nearClipPlane;
        
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, _maxDistance, _layerMask.value))
        {
            GameObject tile = hit.transform.gameObject;
            
            _isPopUpEditorOpen = !_isPopUpEditorOpen;

            TileSurface tileSurface = tile.GetComponent<TileSurface>();

            if (tileSurface == null)
            {
                return;
            }
            
            Debug.Log(tileSurface._TileSurfaceType.ToString());

            PopUpController.Instance.SetCurrentPopUp(PopUpType.TextureSelectionPopUp);
            
            PopUpData newPopUpData = new PopUpData
            {
                PopUpType = (int)tileSurface._TileSurfaceType
            };
            
            PopUpController.Instance.PopulateContent(newPopUpData); 
            
            if (_isPopUpEditorOpen)
            {
                PopUpController.Instance.ShowPopUp();
            }else
            {
                PopUpController.Instance.HidePopUp();
            }

            Debug.Log(tile.name);
        }
    }
}

public class PopUpData : IPopUpData
{
    public int PopUpType { get; set; }
    public string PopUpTitle { get; set; }
}

public interface IPopUpData
{
    int PopUpType { get; }
    string PopUpTitle { get; }
}
