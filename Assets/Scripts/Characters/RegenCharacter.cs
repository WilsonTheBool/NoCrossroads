using UnityEngine;
using System.Collections;

public class RegenCharacter : MonoBehaviour
{
    public
    int regenAmmount;

    public KillableCharacter KillableCharacter;

    public void Regen()
    {
        KillableCharacter.HealSelf(regenAmmount);
    }
}
