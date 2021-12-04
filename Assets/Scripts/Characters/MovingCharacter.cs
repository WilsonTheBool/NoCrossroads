using UnityEngine;
using System.Collections;

public class MovingCharacter : MonoBehaviour
{
    public event System.EventHandler<MoveEventArg> OnMove;
    public UnityEngine.Events.UnityEvent onMove;

    public UnitMovementData UnitMovementData;

    GameWorldMapManager game;

    WorldObject WorldObject;

    private void Awake()
    {
        UnitMovementData = new UnitMovementData();
        WorldObject = GetComponent<WorldObject>();
    }

    private void Start()
    {
        game = GameWorldMapManager.instance;
        
        //WorldObject.SetUp();
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
        OnMove?.Invoke(this, new MoveEventArg(WorldObject.worldPosition, pos, this, WorldObject));
        onMove?.Invoke();
        WorldObject.ChangePosition(pos);
    }

    public class MoveEventArg: System.EventArgs
    {
        public Vector3Int oldPos;
        public Vector3Int newPos;
        public MovingCharacter MovingCharacter;
        public WorldObject worldObject;

        public MoveEventArg(Vector3Int old, Vector3Int newPos)
        {
            this.oldPos = old;
            this.newPos = newPos;
        }

        public MoveEventArg(Vector3Int old, Vector3Int newPos, MovingCharacter movingCharacter, WorldObject worldObject)
        {
            this.oldPos = old;
            this.newPos = newPos;
            this.MovingCharacter = movingCharacter;
            this.worldObject = worldObject;
        }
    }
}
