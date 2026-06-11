public interface IPopUp
{
    void Init();
    PopUpType PopUpType { get; }
    void Show();
    void Hide();
    void PopulateContent(IPopUpData popUpData);
    void ResetContent();
}