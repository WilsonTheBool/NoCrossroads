using UnityEngine;
using System.Collections;

public class TutorialNode_Select : TutorialNode
{

    public GameObject knight;

    public override void OnStart()
    {
        knight.GetComponent<SelectableObject>().OnSelect.AddListener(Knight_OnSelect);

        infoWindow.SetActive(true);
    }


    public override void OnEnd()
    {
        knight.GetComponent<SelectableObject>().OnSelect.RemoveListener(Knight_OnSelect);

        infoWindow.SetActive(false);
    }

    private void Knight_OnSelect()
    {
        tutorialController.StartNextNode();
    }
}
