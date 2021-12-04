using UnityEngine;
using System.Collections;

public class TutorialNode_Movement : TutorialNode
{

    public GameObject knight;

    public WorldObject finishTile;

    public override void OnStart()
    {
        knight.GetComponent<MovingCharacter>().OnMove += TutorialNode_Movement_OnMove;


        infoWindow.SetActive(true);
    }

    private void TutorialNode_Movement_OnMove(object sender, MovingCharacter.MoveEventArg e)
    {
        if(e.newPos == finishTile.worldPosition)
        {
            tutorialController.StartNextNode();
        }
    }

    public override void OnEnd()
    {
        knight.GetComponent<MovingCharacter>().OnMove -= TutorialNode_Movement_OnMove;


        infoWindow.SetActive(false);
    }

}
