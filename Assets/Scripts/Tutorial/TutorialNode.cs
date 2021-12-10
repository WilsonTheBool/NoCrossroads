using UnityEngine;
using System.Collections;

public class TutorialNode : MonoBehaviour
{
    public TutorialController tutorialController;

    public bool isCompleted;

    public GameObject infoWindow;

    public UI_CanvasGroupsController groups;

    public WorldObject[] ObjectToSpawnAtStart;
    public CharacterDeathController[] ObjectsToDestroyOnEnd;

    public TMPro.TMP_Text curentObjective;
    public string curentObjectiveString;
    public virtual void OnStart()
    {
        curentObjective.text = curentObjectiveString;
        foreach (WorldObject worldObject in ObjectToSpawnAtStart)
        {
            worldObject.gameObject.SetActive(true);
        }
    }

    public virtual void OnEnd()
    {
        foreach (CharacterDeathController worldObject in ObjectsToDestroyOnEnd)
        {
            worldObject.OnDeath_Destroy();
        }
    }


}
