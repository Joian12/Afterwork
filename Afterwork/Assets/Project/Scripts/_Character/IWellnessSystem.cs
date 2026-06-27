public interface IWellnessSystem
{
    WellnessState WellnessState { get; }
    void UpdateWellnessValue(float mentalChange, float physicalChange);
    float GetMentalHealth();
    float GetPhysicalHealth();
}