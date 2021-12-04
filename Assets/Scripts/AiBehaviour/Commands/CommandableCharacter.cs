using System;
using System.Collections.Generic;
using UnityEngine;
public partial class CommandableCharacter: MonoBehaviour
{
    public Command_Base curentCommand;

    public void SetCommand(Command_Base command)
    {
        curentCommand = command;
    }

    public virtual void AddBehaviour(BehaviourNode node)
    {

    }

    public virtual void RemoveBehaviour(BehaviourNode node)
    {

    }

}

