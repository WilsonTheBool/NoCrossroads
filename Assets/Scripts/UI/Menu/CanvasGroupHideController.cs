using UnityEngine;
using System.Collections;

public class CanvasGroupHideController : MonoBehaviour
{

    public CanvasGroup CanvasGroup;

    public void HideGroup()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;
    }

    public void ShowGroup()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.interactable = true;
    }
}
