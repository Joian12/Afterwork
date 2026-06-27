using System;

public enum WellnessState
{
    BurnOut,
    Sick,
    Stable,
    Peak
}

public class ActorWellness : IWellnessSystem
{
    private float _mentalHealth;
    private float _physicalHealth;
    
    public const float MaxPhysicalHealth = 100f;
    public const float MaxMentalHealth = 100f;

    public WellnessState WellnessState { get; private set; } = WellnessState.Peak;

    public ActorWellness()
    {
        // TODO: Load wellness values from save file
        this._mentalHealth = MaxPhysicalHealth;
        this._physicalHealth = MaxMentalHealth;
    }
    
    public void UpdateWellnessValue(float mentalChange, float physicalChange)
    {
        this._mentalHealth = Math.Clamp(this._mentalHealth + mentalChange, 0f, MaxPhysicalHealth);
        this._physicalHealth = Math.Clamp(this._physicalHealth + physicalChange, 0f, MaxPhysicalHealth);
        
        
        if (this._mentalHealth <= 10f || this._physicalHealth <= 10f)
        { 
            this.WellnessState = WellnessState.BurnOut;
        }
        else if (this._mentalHealth <= 50f || this._physicalHealth <= 50f)
        {
            this.WellnessState = WellnessState.Sick;
        }
        else if (this._mentalHealth <= 80f || this._physicalHealth <= 80f)
        {
            this.WellnessState = WellnessState.Stable;
        }
        else
        {
            this.WellnessState = WellnessState.Peak;
        }
    }
    
    public float GetMentalHealth()
    {
        return this._mentalHealth;
    }
    
    public float GetPhysicalHealth()
    {
        return this._physicalHealth;
    }
}