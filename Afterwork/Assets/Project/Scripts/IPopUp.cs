public interface IPopUp
{
    PopUpType PopUpType { get; }
    void Show();
    void Hide();
    void PopulateContent(IPopUpData popUpData);
    void ResetContent();
}