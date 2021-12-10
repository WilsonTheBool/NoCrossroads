using System;
using System.Collections;
using UnityEngine;

public class TutorialNode_Attack: TutorialNode
{
    public MovingCharacter knight;

    public KillableCharacter orcToKill;

    public CharacterDeathController[] blockingObjects;

    public GameObject ExpWindowInfo;

    public WorldObject[] finishTiles;

    public float endWaitTime;

    public override void OnStart()
    {
        base.OnStart();

        orcToKill.OnDeath.AddListener(OnOrcDeath);
        knight.OnMove += Knight_OnMove;

        infoWindow.SetActive(true);

        foreach (WorldObject tile in finishTiles)
        {
            tile.gameObject.SetActive(true);
        }

        orcToKill.gameObject.SetActive(true);
    }

    private void Knight_OnMove(object sender, MovingCharacter.MoveEventArg e)
    {
        foreach(WorldObject tile in finishTiles)
        {
            if(tile.worldPosition == e.newPos)
            {
                isCompleted = true;
                tutorialController.StartNextNode();
            }
        }
    }

    IEnumerator ExpWaitCo()
    {
        yield return new WaitForSeconds(endWaitTime);
        ExpWindowInfo.SetActive(true);
    }

    private void OnOrcDeath()
    {
        orcToKill.OnDeath.RemoveListener(OnOrcDeath);
        foreach (CharacterDeathController character in blockingObjects)
        {
            character.OnDeath_Destroy();
        }

        StartCoroutine(ExpWaitCo());
    }

    public override void OnEnd()
    {
        base.OnEnd();
        knight.OnMove -= Knight_OnMove;
    }
}

