using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ShopElement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;

    [TextArea]
    public string itemDescription;

    public GameResourceManager.ResourceHolder[] itemPrice;

    private GameResourceManager GameResourceManager;
    private GameWorldMapManager GameWorldMapManager;

    public ShopItem_DescriptionManager descriptionManager;
    public ShopItemPlacerController ShopItemPlacerController;

    [SerializeField]
    public GameObject itemPrefab;

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameResourceManager = GameResourceManager.instance;
    }

    public void ShowDescription()
    {
        descriptionManager.CreateDescription(this);
    }

    public void HideDescription()
    {
        descriptionManager.RemoveDescription();
    }

    public bool TryStartBuySelect()
    {
        if (CanBuyItem())
        {
            ShopItemPlacerController.StartPlacment(this);
            return true;
        }


        return false;
    }

    private bool CanBuyItem()
    {
        foreach(GameResourceManager.ResourceHolder res in itemPrice)
        {
            if(!GameResourceManager.HasMoreThanXResource(res.data, res.value))
            {
                return false;
            }
            
        }

        return true;
    }

    public void ConfirmBuy()
    {
        foreach(GameResourceManager.ResourceHolder holder in itemPrice)
        {
            
            GameResourceManager.RemoveResource(holder.data, holder.value);
        }
    }

    public void SpawnItem(Vector3Int position)
    {
        Instantiate(itemPrefab, GameWorldMapManager.GetTileCenterInWorld(position), Quaternion.Euler(0, 0, 0));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!TryStartBuySelect())
        {
            print("Cant buy item");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideDescription();
    }
}
