public sealed class FloorTileEditHandler : SurfaceTextureEditHandler
{
    protected override InteriorObjectType TargetSurface
    {
        get { return InteriorObjectType.Floor; }
    }

    protected override PopUpType TargetPopUpType
    {
        get { return PopUpType.FloorSelectionPopUp; }
    }
}
