public sealed class WallpaperEditHandler : SurfaceTextureEditHandler
{
    protected override InteriorObjectType TargetSurface => InteriorObjectType.Wall;

    protected override PopUpType TargetPopUpType => PopUpType.WallPaperSelectionPopUp;
}