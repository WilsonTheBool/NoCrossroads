using UnityEngine;
using System.Collections;

public class TutorialNode_Select : TutorialNode
{

    public GameObject knight;

    public override void OnStart()
    {
        curentObjective.text = curentObjectiveString;

        knight.GetComponent<SelectableObject>().OnSelect.AddListener(Knight_OnSelect);

        groups.SetActiveWindows(new UI_CanvasGroupsController.CanvasGroupFlags() { setDefault = true }, false);

        infoWindow.SetActive(true);
    }


    public override void OnEnd()
    {
        knight.GetComponent<SelectableObject>().OnSelect.RemoveListener(Knight_OnSelect);

        infoWindow.SetActive(false);
    }

    private void Knight_OnSelect()
    {
        isCompleted = true;
        tutorialController.StartNextNode();
    }
}
