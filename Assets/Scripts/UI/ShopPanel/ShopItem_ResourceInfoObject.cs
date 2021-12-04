using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItem_ResourceInfoObject : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    TMPro.TMP_Text value;

    [SerializeField]
    Color defaultColor;

    [SerializeField]
    Color redColor;

    public void SetUp(GameResourceManager.ResourceHolder holder, bool hasResource)
    {
        value.text = Mathf.Round(holder.value).ToString();
        if (hasResource)
        {
            value.color = defaultColor;
        }
        else
        {
            value.color = redColor;
        }

        image.sprite = holder.data.icon;

    }
}
