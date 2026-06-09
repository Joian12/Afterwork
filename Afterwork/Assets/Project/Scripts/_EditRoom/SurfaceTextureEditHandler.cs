using UnityEngine;
using UnityEngine.EventSystems;

public abstract class SurfaceTextureEditHandler : MonoBehaviour, IEditModeHandler
{
    [SerializeField] protected LayerMask _raycastMask;

    private Camera _camera;
    private const float MaxDistance = 100f;

    protected abstract InteriorObjectType TargetSurface { get; }

    protected abstract PopUpType TargetPopUpType { get; }

    protected virtual void Awake()
    {
        _camera = Camera.main;
    }
    
    public void Enable()
    {
        this.enabled = true;
    }

    public void Disable()
    {
        this.enabled = false;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            TrySelectSurface();
        }
    }
    
    private void TrySelectSurface()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, MaxDistance, _raycastMask) == false)
        {
            return;
        }

        if (hit.transform.TryGetComponent<TileSurface>(out var surface) == false)
        {
            return;
        }

        if (surface.interiorObjectType != TargetSurface)
        {
            return;
        }

        PopUpController.Instance.SetCurrentPopUp(TargetPopUpType);

        var data = new PopUpData
        {
            InteriorObjectType = TargetSurface,
            PopUpTitle = string.Empty
        };

        PopUpController.Instance.PopulateContent(data, ref surface);
        PopUpController.Instance.ShowPopUp();
    }
}
