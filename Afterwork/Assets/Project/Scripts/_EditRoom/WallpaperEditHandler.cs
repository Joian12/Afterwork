public sealed class WallpaperEditHandler : SurfaceTextureEditHandler
{
    protected override InteriorObjectType TargetSurface
    {
        get { return InteriorObjectType.Wall; }
    }

    protected override PopUpType TargetPopUpType
    {
        get { return PopUpType.WallPaperSelectionPopUp; }
    }
}
