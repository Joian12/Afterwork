public sealed class FloorTileEditHandler : SurfaceTextureEditHandler
{
    protected override InteriorObjectType TargetSurface => InteriorObjectType.Floor;

    protected override PopUpType TargetPopUpType => PopUpType.FloorSelectionPopUp;
}