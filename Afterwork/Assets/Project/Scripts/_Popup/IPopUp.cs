public interface IPopUp
{
    PopUpType PopUpType { get; }
    void Init();
    void Show();
    void Hide();
    void PopulateContent(IPopUpData popUpData);
    void ResetContent();
}