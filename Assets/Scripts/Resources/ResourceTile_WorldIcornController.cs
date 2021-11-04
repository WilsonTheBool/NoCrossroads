using UnityEngine;
using System.Collections;

public class ResourceTile_WorldIcornController : MonoBehaviour
{
    public GameObject iconObject;

    public void ShowIcon()
    {
        iconObject.SetActive(true);
    }

    public void HideIcon()
    {
        iconObject.SetActive(false);
    }
}
