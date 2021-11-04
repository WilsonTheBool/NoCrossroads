using UnityEngine;
using System.Collections;

public class ResourceIconController : MonoBehaviour
{
    private ResourceTile tile ;

    public SpriteRenderer IconObject;

    private bool isFree;

    private void Start()
    {
        tile = GetComponent<ResourceTile>();
        SetIcon(isFree);
    }

    public void ResourceStateChanged()
    {
        
    }

    public void HideIcon()
    {
        IconObject.gameObject.SetActive(false);
    }

    public void ShowIcon(bool isFree)
    {
        IconObject.gameObject.SetActive(true);

        if(this.isFree != isFree)
        {
            this.isFree = isFree;
            SetIcon(isFree);
        }
    }

    private void SetIcon(bool isFree)
    {
        if (isFree)
        {
            IconObject.sprite = tile.resource.icon;
        }
        else
        {
            IconObject.sprite = tile.resource.icon_selected;
        }
    }
}
