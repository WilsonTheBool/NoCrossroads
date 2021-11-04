using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItem_ResourceInfoObject : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    TMPro.TMP_Text value;

    public void SetUp(GameResourceManager.ResourceHolder holder)
    {
        value.text = Mathf.Round(holder.value).ToString();
        image.sprite = holder.data.icon;

    }
}
