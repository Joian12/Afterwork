using TMPro;
using UnityEngine;

public class WellnessUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _mentalHealthText;
    [SerializeField] private TextMeshProUGUI _physicalHealthText;
    [SerializeField] private TextMeshProUGUI _wellnessStateText;

    private void Start()
    {
        WellnessManager.Instance.OnWellnessChanged += UpdateWellnessUI;
    }

    private void UpdateWellnessUI()
    {
        this._mentalHealthText.text = WellnessManager.Instance.GetMentalHealth().ToString();
        this._physicalHealthText.text = WellnessManager.Instance.GetPhysicalHealth().ToString();
        this._wellnessStateText.text = WellnessManager.Instance.GetWellnessState().ToString();
    }
}