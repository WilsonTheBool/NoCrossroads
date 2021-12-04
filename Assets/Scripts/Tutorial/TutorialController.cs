using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour
{

    public TutorialNode[] tutorialNodes;

    TutorialNode curentNode;

    public void StartNextNode()
    {
        foreach(TutorialNode node in tutorialNodes)
        {
            if (!node.isCompleted)
            {
                curentNode?.OnEnd();
                curentNode = node;
                curentNode?.OnStart();
            }

        }
    }

    private void Start()
    {
        StartNextNode();
    }
}
