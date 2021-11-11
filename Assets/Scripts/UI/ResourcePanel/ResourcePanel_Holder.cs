using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcePanel_Holder: MonoBehaviour
{
    public ResourceData_SO resource;

    public TMP_Text countText;

    public TMP_Text perTurnText;

    [SerializeField]
    Color redColor;

    [SerializeField]
    Color greenColor;

    public void UpdateHolder(float newCount, float newPerTurn)
    {
        countText.text = Mathf.Round(newCount).ToString();


        
        
        if(newPerTurn <= 0)
        {
            perTurnText.color = redColor;
            perTurnText.text = Mathf.Round(newPerTurn).ToString();
        }
        else
        {
            perTurnText.color = greenColor;
            perTurnText.text = "+" + Mathf.Round(newPerTurn).ToString();
        }

    }
}

