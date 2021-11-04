using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcePanel_Holder: MonoBehaviour
{
    public ResourceData_SO resource;

    public TMP_Text countText;



    public void UpdateHolder(float newCount)
    {
        countText.text = Mathf.Round(newCount).ToString();
    }
}

