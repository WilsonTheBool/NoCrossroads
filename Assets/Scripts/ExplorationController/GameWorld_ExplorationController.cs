using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GameWorld_ExplorationController : GameWorldMap_Dependable
{
    public static GameWorld_ExplorationController instance;

    GameWorldMapManager GameWorldMapManager;

    public GameMap GameMap;

    public bool[,] isExploresArray;

    public Vector3Int offset;

    public Tilemap manTilemap;

    public Tilemap fogTilemap;

    public TileBase fogTile;

    //public List<ExplorerCharacter> explorerCharacters;

    public UnityEngine.Events.UnityEvent<Vector3Int> OnExplore;

    public bool isAllExploredOnStart;

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    // Use this for initialization
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        
    }

    private void Start()
    {


        

        
    }

    public void ExploreAll()
    {
        for(int i = 0; i < GameMap.MapSize.x; i++)
        {
            for (int j = 0; j < GameMap.MapSize.y; j++)
            {
                Explore(new Vector3Int(i, j, 0));
            }
        }
    }

    public override void SetUp()
    {
        GameWorldMapManager = GameWorldMapManager.instance;

        CreateEmptyGrid(GameMap.MapSize, GameMap.MapOffset);

        GameWorldMapManager.OnUnitMove += GameWorldMapManager_OnUnitMove;
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;

        if (isAllExploredOnStart)
        {
            ExploreAll();
        }
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.TryGetComponent<ExplorerCharacter>(out ExplorerCharacter explorerCharacter))
        {
            

            explorerCharacter.TryExplore(e.worldObject.worldPosition);

        }

        if (e.worldObject.TryGetComponent<RenderObject>(out RenderObject renderObject))
        {
            if (IsExploredFromGlobal(e.worldObject.worldPosition))
            {
                renderObject.OnFadeEnd?.Invoke();
            }
            else
            {
                renderObject.OnFadeStart?.Invoke();
            }
        }
   
       
    }

    private void GameWorldMapManager_OnUnitMove(object sender, MovingCharacter.MoveEventArg e)
    {
        if(IsExploredFromGlobal(e.oldPos) != IsExploredFromGlobal(e.newPos))
        {
           if(e.MovingCharacter.TryGetComponent<RenderObject>(out RenderObject renderObject))
            {
                if (IsExploredFromGlobal(e.newPos))
                {
                    renderObject.OnFadeEnd?.Invoke();
                    
                }
                else
                {
                    renderObject.OnFadeStart?.Invoke();
                }
            }
        }
    }

    public void CreateEmptyGrid(Vector3Int size, Vector3Int offset)
    {
        this.offset = offset;
        isExploresArray = new bool[size.x, size.y];
    }

    public void TryExplore(Vector3Int[] positions)
    {
        foreach(Vector3Int pos in positions)
        {
            Vector3Int local = GlobalToLocal(pos);
            if (IsVectorInsideArray(local) && !IsExplored(local))
            {
                Explore(local);
            }
        }
    }

    private void Explore(Vector3Int pos)
    {


        Vector3Int global = LocalToGLobal(pos);

        fogTilemap.SetTile(global, null);

        WorldObject[] worldObjects = GameWorldMapManager.GetAllWorldObjectsOnPosition(global);

        foreach(WorldObject worldObject in worldObjects)
        {
            if (worldObject.TryGetComponent<RenderObject>(out RenderObject renderObject))
            {
                renderObject.OnFadeEnd?.Invoke();
            }
        }

        Set(pos, true);
        OnExplore?.Invoke(global);
    }


    public bool IsExploredFromGlobal(Vector3Int vec)
    {
        return IsExplored(GlobalToLocal(vec));
    }

    private bool IsExplored(Vector3Int vec)
    {
        if(isExploresArray == null)
        {
            return false;
        }

        return isExploresArray[vec.x, vec.y];
    }

    private void Set(Vector3Int vec, bool value)
    {
        isExploresArray[vec.x, vec.y] = value;
    }

    private bool IsVectorInsideArray(Vector3Int vector)
    {
        return vector.x >= 0 && vector.x < isExploresArray.GetLength(0) && vector.y >= 0 && vector.y < isExploresArray.GetLength(1);
    }

    private Vector3Int GlobalToLocal(Vector3Int global)
    {
        return global - offset;
    }

    private Vector3Int LocalToGLobal(Vector3Int local)
    {
        return local + offset;
    }
}
