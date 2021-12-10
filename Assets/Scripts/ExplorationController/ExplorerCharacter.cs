using UnityEngine;
using System.Collections;

public class ExplorerCharacter : MonoBehaviour
{
    [SerializeField]
    MovingCharacter MovingCharacter;

    GameWorldMapManager GameWorldMapManager;

    public GameWorld_ExplorationController GameWorld_ExplorationController;

    public WorldObject worldObject;

    public int ExploreRange;

    public void TryExplore(Vector3Int startPos)
    {
        GameWorld_ExplorationController.TryExplore(MathAdd.GetAllPositionInCircle(startPos, ExploreRange));
    }

    private void Awake()
    {
        worldObject = GetComponent<WorldObject>();
        worldObject.OnSetUpComplete.AddListener(SetUp);

        if (MovingCharacter != null)
            MovingCharacter.OnMove += MovingCharacter_OnMove;
    }

    private void Start()
    {
    }

    public void SetUp()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameWorld_ExplorationController = GameWorld_ExplorationController.instance;
    }

    private void MovingCharacter_OnMove(object sender, MovingCharacter.MoveEventArg e)
    {
        TryExplore(e.newPos);
    }
}
