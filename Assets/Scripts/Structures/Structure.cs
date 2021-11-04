using UnityEngine;
using System.Collections;

public class Structure : MonoBehaviour
{
    public float workEffectivenessValue = 1.0f;

    public void AddEffectiveness_Additive(float value)
    {
        workEffectivenessValue += value;
    }

    public void Add_Effectiveness_Mult(float value)
    {
        workEffectivenessValue += workEffectivenessValue * value;
    }
    
    public void SetEffectivenessValue(float value)
    {
        workEffectivenessValue = value;
    }
}
