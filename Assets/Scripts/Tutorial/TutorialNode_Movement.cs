using UnityEngine;
using System.Collections;

public class TutorialNode_Movement : TutorialNode
{

    public GameObject knight;

    public WorldObject[] finishTiles;

    public CharacterDeathController[] blockade;

    public GameObject endTurnInfoWindow;

    public override void OnStart()
    {
        curentObjective.text = curentObjectiveString;

        knight.GetComponent<MovingCharacter>().OnMove += TutorialNode_Movement_OnMove;
        knight.GetComponent<MovingCharacter>().OnMove += TutorialNode_EndTurn_OnMove;

        groups.SetActiveWindows(new UI_CanvasGroupsController.CanvasGroupFlags() { setDefault = true }, false);

        infoWindow.SetActive(true);

        foreach (WorldObject tile in finishTiles)
        {
            tile.gameObject.SetActive(true);
        }
    }

    private void TutorialNode_Movement_OnMove(object sender, MovingCharacter.MoveEventArg e)
    {
        foreach(WorldObject finishTile in finishTiles)
        if(e.newPos == finishTile.worldPosition)
        {
            isCompleted = true;
            tutorialController.StartNextNode();
        }
    }

    private void TutorialNode_EndTurn_OnMove(object sender, MovingCharacter.MoveEventArg e)
    {
        if (e.MovingCharacter.movePoints == 0)
        {
            endTurnInfoWindow.SetActive(true);
            e.MovingCharacter.OnMove -= TutorialNode_EndTurn_OnMove;
        }
    }

    public override void OnEnd()
    {
        knight.GetComponent<MovingCharacter>().OnMove -= TutorialNode_Movement_OnMove;

        foreach(CharacterDeathController block in blockade)
        {
            block.OnDeath_Destroy();
        }

        infoWindow.SetActive(false);
    }

}
