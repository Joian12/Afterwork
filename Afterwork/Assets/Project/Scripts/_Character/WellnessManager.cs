using System;
using UnityEngine;

public class WellnessManager : MonoBehaviour
{
    public static WellnessManager Instance;
    
    private readonly IWellnessSystem _actorWellness = new ActorWellness();
    
    public Action OnWellnessChanged;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.OnWellnessChanged?.Invoke();
    }

    public void UpdateWellnessValue(float mentalChange, float physicalChange)
    {
        this._actorWellness.UpdateWellnessValue(mentalChange, physicalChange);
        this.OnWellnessChanged?.Invoke();
    }
    
    public void DebugUpdateMentalHealth(float mentalChange)
    {
        this.UpdateWellnessValue(mentalChange, 0);
    }
    
    public void DebugUpdatePhysicalHealth(float physicalChange)
    {
        this.UpdateWellnessValue(0, physicalChange);
    }

    #region Getters
    
    public float GetMentalHealth()
    {
        return this._actorWellness.GetMentalHealth();
    }
    
    public float GetPhysicalHealth()
    {
        return this._actorWellness.GetPhysicalHealth();
    }
    
    public WellnessState GetWellnessState()
    {
        return this._actorWellness.WellnessState;
    }   
    
    #endregion
}