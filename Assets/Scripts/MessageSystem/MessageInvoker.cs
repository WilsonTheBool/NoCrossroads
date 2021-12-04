using UnityEngine;
using System.Collections;

public class MessageInvoker : MonoBehaviour
{
    public bool isError;
    public string messageText;

    public void ShowMessage()
    {
        if (isError)
        {
            MessageController.instance.ShowError(messageText);
        }
        else
        {
            MessageController.instance.ShowMessage(messageText);
        }
    }
}
