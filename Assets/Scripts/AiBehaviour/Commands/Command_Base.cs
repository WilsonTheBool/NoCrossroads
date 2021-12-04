using UnityEngine;
using System.Collections.Generic;

public class Command_Base : MonoBehaviour
{
    public int maxAgentNumber;

    List<CommandableCharacter> agents;

    void AddAgent(CommandableCharacter commandableCharacter)
    {
        //Add to agents;
        //Add behaviour to agent
    }

    void RemoveAgent(CommandableCharacter commandableCharacter)
    {

    }

    void OnComplete()
    {

    }

    void OnCancel()
    {

    }


    BehaviourNode commandNode;


}
