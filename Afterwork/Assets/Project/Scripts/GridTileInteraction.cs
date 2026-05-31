using UnityEngine;

public class GridTileInteraction : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _maxDistance = 100;

    public void GetLatestTilePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _camera.nearClipPlane;
        
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, _maxDistance, _layerMask.value))
        {
            GameObject tile = hit.transform.gameObject;
            
            Debug.Log(tile.name);
            
        }
    }
}
