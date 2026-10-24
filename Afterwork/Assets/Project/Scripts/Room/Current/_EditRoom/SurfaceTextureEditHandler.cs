using UnityEngine;
using UnityEngine.EventSystems;

public abstract class SurfaceTextureEditHandler : MonoBehaviour, IEditModeHandler
{
    private const float MaxDistance = 100f;
    [SerializeField] protected LayerMask _raycastMask;

    private Camera _camera;

    protected abstract InteriorObjectType TargetSurface { get; }

    protected abstract PopUpType TargetPopUpType { get; }

    protected virtual void Awake()
    {
        this._camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false) TrySelectSurface();
    }

    public void Enable()
    {
        this.enabled = true;
    }

    public void Disable()
    {
        this.enabled = false;
    }

    private void TrySelectSurface()
    {
        var ray = this._camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, MaxDistance, this._raycastMask) == false) return;

        if (hit.transform.TryGetComponent<TileSurface>(out var surface) == false) return;

        if (surface.interiorObjectType != this.TargetSurface) return;

        RoomManager.SelectedTileSurface = surface;

        PopUpController.Instance.SetCurrentPopUp(this.TargetPopUpType);

        var data = new PopUpData
        {
            InteriorObjectType = this.TargetSurface,
            PopUpTitle = string.Empty
        };

        PopUpController.Instance.PopulateContent(data); // populate depending on type. 
        PopUpController.Instance.ShowPopUp();
    }
}