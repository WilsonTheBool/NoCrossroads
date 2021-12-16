using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Shop/Price Data")]
public class ShopPriceData : ScriptableObject
{
    public GameResourceManager.ResourceHolder[] ItemPrice;

    public GameResourceManager.ResourceHolder[] ItemPriceIncrease;
}