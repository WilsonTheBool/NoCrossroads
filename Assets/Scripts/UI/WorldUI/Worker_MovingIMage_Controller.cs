using UnityEngine;
using System.Collections;

public class Worker_MovingIMage_Controller : MonoBehaviour
{
    public Miner_Structure Miner_Structure;
    public CharacterUI_SpawnMovingImage CharacterUI_SpawnMovingImage;

    private void Start()
    {
        CharacterUI_SpawnMovingImage.spriteToAdd = Miner_Structure.resourceTile.resource.icon;
    }
}
