using UnityEngine;
using System.Collections;

public class SelectModule : ScriptableObject
{

   public virtual void OnSelect_Start(GameInputData inputData, SelectEventArgs selectEventArgs)
    {

    }

    public virtual void OnSelect_End(GameInputData inputData, SelectEventArgs selectEventArgs)
    {

    }

    public virtual void OnSelect_Update(GameInputData inputData, SelectEventArgs selectEventArgs)
    {

    }

    public virtual void OnSelect_AcceptPressed(GameInputData inputData, SelectEventArgs selectEventArgs)
    {

    }

    public class SelectEventArgs
    {
        public SpecialTilemapManager SpecialTilemapManager;

        public GameWorldMapManager GameWorldMapManager;

        public GameUnitMovementController GameUnitMovementController;

        public SelectableObject SelectableObject;

        public MovingCharacter MovingCharacter;

        public AttackingCharacter AttackingCharacter;
    }
}
