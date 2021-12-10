using UnityEngine;
using System.Collections;

public class MessageController : MonoBehaviour
{
    public static  MessageController instance;

    public TMPro.TMP_Text errorMessagePrefab;
    public TMPro.TMP_Text defaultMessagePrefab;
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    public void ShowError(string message)
    {
        Instantiate(errorMessagePrefab, this.transform).text = message;
    }

    public void ShowMessage(string message)
    {
        Instantiate(defaultMessagePrefab, this.transform).text = message;
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
