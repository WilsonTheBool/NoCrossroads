using UnityEngine;
using System.Collections;

public class UI_CanvasGroupsController : MonoBehaviour
{

    public CanvasGroup[] defaultElements;

    public CanvasGroup resourcePanel;

    public CanvasGroup shopWindow;

    public CanvasGroup selectWindow;

    public void SetActiveWindows(CanvasGroupFlags groupFlags, bool value)
    {
        if (groupFlags.setDefault)
        {
            foreach (CanvasGroup obj in defaultElements)
            {
                SetCanvasGroup(obj, value);
            }
        }
        else
        if (groupFlags.setResources)
        {
            SetCanvasGroup(resourcePanel, value);

        }
        else
        if (groupFlags.setShopWindow)
        {
            SetCanvasGroup(shopWindow, value);
        }
        else
        if(groupFlags.setSelectUnitWondow)
        {
            SetCanvasGroup(selectWindow, value);
        }
    }

    private void SetCanvasGroup(CanvasGroup obj, bool value)
    {
        if (value)
        {
            obj.alpha = 1;
            obj.interactable = true;
            obj.blocksRaycasts = true;
        }
        else
        {
            obj.alpha = 0;
            obj.interactable = false;
            obj.blocksRaycasts = false;
        }

    }

   

    public struct CanvasGroupFlags
    {
        public bool setDefault;

        public bool setResources;

        public bool setShopWindow;
        public bool setSelectUnitWondow;
        
    }
}
