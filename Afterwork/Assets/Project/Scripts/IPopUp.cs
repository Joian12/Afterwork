public interface IPopUp
{
    void Init();
    PopUpType PopUpType { get; }
    void Show();
    void Hide();
    void PopulateContent(IPopUpData popUpData, ref TileSurface tileSurface);
    void ResetContent();
}