using UnityEngine;
using System.Collections;

public class TutorialNode_KillNest : TutorialNode
{
    [SerializeField]
    KillableCharacter aiNestToKill;

    [SerializeField]
    GameObject victoryScreen;

    public override void OnStart()
    {
        base.OnStart();
        aiNestToKill.OnDeath.AddListener(OnNestDeath);
    }

    void OnNestDeath()
    {
        victoryScreen.SetActive(true);
        isCompleted = true;
    }
}
