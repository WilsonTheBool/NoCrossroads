using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ShopItemDescription : MonoBehaviour
{
    [SerializeField]
    TMP_Text itemNameText;

    [SerializeField]
    TMP_Text itemDescriptionText;

    [SerializeField]
    ShopItem_ResourceInfoObject ResourceInfoPrefab;

    [SerializeField]
    Transform infoHolder;

    List<ShopItem_ResourceInfoObject> infos = new List<ShopItem_ResourceInfoObject>();

    GameResourceManager GameResourceManager;

    public void SetUp(ShopElement el)
    {
        GameResourceManager = GameResourceManager.instance;

        itemNameText.text = el.itemName;
        itemDescriptionText.text = el.itemDescription;

        foreach (GameResourceManager.ResourceHolder holder in el.itemPrice)
        {
            var info = Instantiate(ResourceInfoPrefab, infoHolder);
            info.SetUp(holder, GameResourceManager.HasMoreThanXResource(holder.data, holder.value));
            infos.Add(info);
        }
    }

    private void OnDisable()
    {
        foreach(ShopItem_ResourceInfoObject info in infos)
        {
            Destroy(info);
        }

        infos.Clear();
    }
}
