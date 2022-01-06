using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_UnlockableItem : MonoBehaviour
{
    public bool isLocked;

    public Color lockedColor;
    public Color unlockedColor;

    public Image image;

    private void Awake()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

    private void Start()
    {

        if (isLocked)
        {
            LockItem();
        }
        else
        {
            UnlockItem();
        }
    }

    public void UnlockItem()
    {
        isLocked = false;
        image.color = unlockedColor;
        image.raycastTarget = true;
    }

    public void LockItem()
    {
        isLocked = true;
        image.color = lockedColor;
        image.raycastTarget = false;
    }
}
