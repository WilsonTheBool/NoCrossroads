using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ShopElement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;

    [TextArea]
    public string itemDescription;

    public ShopPriceData priceData; 

    [HideInInspector]
    public GameResourceManager.ResourceHolder[] itemPrice;

    private GameResourceManager GameResourceManager;
    private GameWorldMapManager GameWorldMapManager;

    public ShopItem_DescriptionManager descriptionManager;
    public ShopItemPlacerController ShopItemPlacerController;

    public PrefabSpawner prefabSpawner;

    [SerializeField]
    public GameObject itemPrefab;

    public bool isGameWorldItem;



    public UnityEngine.Events.UnityEvent OnBuyItem;
    public UnityEngine.Events.UnityEvent OnCantBuyItem;

    private void Awake()
    {
        itemPrice = new GameResourceManager.ResourceHolder[priceData.ItemPrice.Length];

        for(int i = 0; i < itemPrice.Length; i++)
        {
            itemPrice[i] = new GameResourceManager.ResourceHolder() { data = priceData.ItemPrice[i].data, value = priceData.ItemPrice[i].value };
        }
    }

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameResourceManager = GameResourceManager.instance;
        prefabSpawner = PrefabSpawner.instance;

        
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
            if (isGameWorldItem)
            {
                ShopItemPlacerController.StartPlacment(this);
            }
            else
            {
                ConfirmBuy();
            }
            
            return true;
        }

        OnCantBuyItem?.Invoke();
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

    public void IncreacePrice()
    {
        foreach (GameResourceManager.ResourceHolder change in priceData.ItemPriceIncrease)
        {
            foreach (GameResourceManager.ResourceHolder resource in itemPrice)
            {

                if (resource.data == change.data)
                {
                    resource.value += change.value;
                }
            }
        }
    }

    public void DicreacePrice()
    {
        foreach (GameResourceManager.ResourceHolder change in priceData.ItemPriceIncrease)
        {
            foreach (GameResourceManager.ResourceHolder resource in itemPrice)
            {

                if (resource.data == change.data)
                {
                    resource.value -= change.value;
                }
            }
        }
    }

    public void ConfirmBuy()
    {
        foreach(GameResourceManager.ResourceHolder holder in itemPrice)
        {
            
            GameResourceManager.RemoveResource(holder.data, holder.value);

        }

        OnBuyItem?.Invoke();
    }

    public void SpawnItem(Vector3Int position)
    {
        string typeName = itemPrefab.GetComponent<WorldObject>().typeName;
        prefabSpawner.InstansiatePPrefab(typeName, GameWorldMapManager.GetTileCenterInWorld(position));
        
        //Instantiate(itemPrefab, GameWorldMapManager.GetTileCenterInWorld(position), Quaternion.Euler(0, 0, 0));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!TryStartBuySelect())
        {
            
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
