using System;
using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxDistance = 100f;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TrySelectTile();
        }
    }

    private void TrySelectTile()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _layerMask))
        {
            if (hit.transform.TryGetComponent<TileSurface>(out var tileSurface) == false)
            {
                return;
            }

            Debug.Log($"Selected Tile: {hit.transform.name} | Surface: {tileSurface._TileSurfaceType}");

            PopUpController.Instance.SetCurrentPopUp(PopUpType.TextureSelectionPopUp);
            
            PopUpData newPopUpData = new PopUpData
            {
                PopUpType = PopUpType.TextureSelectionPopUp,
                SurfaceType = tileSurface._TileSurfaceType,
                PopUpTitle = String.Empty,
            };
            
            PopUpController.Instance.PopulateContent(newPopUpData); 
            PopUpController.Instance.ShowPopUp();
        }
    }
}