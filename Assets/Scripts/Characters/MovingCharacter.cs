using UnityEngine;
using System.Collections;

public class MovingCharacter : MonoBehaviour
{
    public event System.EventHandler<MoveEventArg> OnMove;

    GameWorldMapManager game;

    WorldObject WorldObject;

    private void Start()
    {
        game = GameWorldMapManager.instance;
        WorldObject = GetComponent<WorldObject>();
        WorldObject.SetUp();
    }

    public void RegenMovePoints()
    {
        movePoints = maxMovePoints;
    }

    public int movePoints;

    public int maxMovePoints;

    public void Move(Vector3Int pos, int cost)
    {

        movePoints -= cost;
        this.transform.position = game.GetTileCenterInWorld(pos);
        OnMove?.Invoke(this, new MoveEventArg(WorldObject.worldPosition, pos));
        
        WorldObject.ChangePosition(pos);
    }

    public class MoveEventArg: System.EventArgs
    {
        public Vector3Int oldPos;
        public Vector3Int newPos;

        public MoveEventArg(Vector3Int old, Vector3Int newPos)
        {
            this.oldPos = old;
            this.newPos = newPos;
        }
    }
}
