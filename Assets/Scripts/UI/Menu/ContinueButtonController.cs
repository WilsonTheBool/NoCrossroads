using UnityEngine;
using System.Collections;

public class ContinueButtonController : MonoBehaviour
{

    public GameSaveLoadController GameSaveLoadController;

    public TMPro.TMP_Text saveDescription;

    public UnityEngine.UI.Button continueButton;

    private void Start()
    {
        GameSaveLoadController = GameSaveLoadController.instance;

        if (GameSaveLoadController.HasSaveData())
        {
            continueButton.interactable = true;
            saveDescription.text = "Level: " + GameSaveLoadController.levelSaveData.sceneName +
           "\nPlay time: " + GameSaveLoadController.levelSaveData.playTime;
        }
        else
        {
            continueButton.interactable = false;
        }

       
        
    }

}
