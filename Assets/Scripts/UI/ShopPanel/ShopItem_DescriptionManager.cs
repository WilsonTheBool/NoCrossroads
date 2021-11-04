using UnityEngine;
using System.Collections;

public class ShopItem_DescriptionManager : MonoBehaviour
{
    public ShopItemDescription descriptionObjectPrefab;

    [SerializeField]
    private ShopItemDescription descriptionObject;

    public void CreateDescription(ShopElement el)
    {
        descriptionObject = Instantiate(descriptionObjectPrefab, el.transform.position, Quaternion.Euler(0,0,0), transform) ;
        descriptionObject.SetUp(el);
    }

    public void RemoveDescription()
    {
        print("Remove");
        Destroy(descriptionObject.gameObject);
    }
}
