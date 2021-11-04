using UnityEngine;
using System.Collections;

public class ResourceCountPanel : MonoBehaviour
{
    public GameResourceManager resourceManager;

    [SerializeField]
    ResourcePanel_Holder[] holders;

    private void Start()
    {
        resourceManager = GameResourceManager.instance;
        resourceManager.ResourcesChanged.AddListener(UpdateResourcePanel);

        holders = GetComponentsInChildren<ResourcePanel_Holder>();

        UpdateResourcePanel();
    }

    public void UpdateResourcePanel()
    {
        foreach(ResourcePanel_Holder holder in holders)
        {
            holder.UpdateHolder(resourceManager.GetResourceCount(holder.resource));
        }
    }
}
