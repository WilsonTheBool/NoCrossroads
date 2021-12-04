using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class GameWorldMapManager : MonoBehaviour
{
    public static GameWorldMapManager instance;

    public LandTilemapManager LandTilemapManager;
    public TerritoryTilemapManager TerritoryTilemapManager;
    public SpecialTilemapManager SpecialTilemapManager;
    public PathTilemapManager pathTilemap;

    public Tilemap mainTilemap;
    //public List<ResourceTile> resourceTiles;
    //public List<Miner_Structure> miners;

    public List<WorldObject> allObjectsOnMap;

    public List<Vector3Int> playerTerritory;

    //public List<King_Structure> kingStructures;

    public event System.EventHandler<UnitEventArgs> OnUnitCreate;
    public event System.EventHandler<UnitEventArgs> OnUnitDeath;
    public event System.EventHandler<MovingCharacter.MoveEventArg> OnUnitMove;

    public UnityEngine.Events.UnityEvent<Vector3Int[]> OnPlayerTerritoryAdd;
    public UnityEngine.Events.UnityEvent<Vector3Int[]> OnPlayerTerritoryRemove;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }


        ////Change later (!!!)......................
        //foreach (Vector3Int pos in TerritoryTilemapManager.territoryTilemap.cellBounds.allPositionsWithin)
        //{
        //    if(TerritoryTilemapManager.territoryTilemap.HasTile(pos))
        //    playerTerritory.Add(pos);
        //}
        ////.....................................

        if (mainTilemap == null)
        {
            mainTilemap = FindObjectOfType<Tilemap>();
        }
    }

    public bool isSetUpComplete;

    private void Start()
    {
        


        GameWorldMap_Dependable[] dependables = FindObjectsOfType<GameWorldMap_Dependable>();

        foreach(GameWorldMap_Dependable dependable in dependables)
        {
            dependable.SetUp();
        }

        allObjectsOnMap = new List<WorldObject>();

        foreach (WorldObject worldObject in FindObjectsOfType<WorldObject>(false))
        {

            worldObject.SetUp();
        }

        isSetUpComplete = true;
    }

    public void AddWorldObject(WorldObject worldObject)
    {
        
        allObjectsOnMap.Add(worldObject);
        


        MovingCharacter movingCharacter = worldObject.GetComponent<MovingCharacter>();

        if(movingCharacter != null)
        {
            movingCharacter.OnMove += MovingCharacter_OnMove;
        }
        

        OnUnitCreate?.Invoke(this, new UnitEventArgs(worldObject));
    }

    private void MovingCharacter_OnMove(object sender, MovingCharacter.MoveEventArg e)
    {
        OnUnitMove.Invoke(this, e);
    }

    public WorldObject[] GetAllWorldObjectsOnPosition(Vector3Int pos)
    {
        List<WorldObject> list = new List<WorldObject>();

        foreach(WorldObject obj in allObjectsOnMap)
        {
            if(obj.worldPosition == pos)
            {
                list.Add(obj);
            }
        }

        return list.ToArray();
    }

   public void RemoveWorldObject(WorldObject worldObject)
    {
        allObjectsOnMap.Remove(worldObject);
        OnUnitDeath.Invoke(this, new UnitEventArgs(worldObject));
    }
    public Vector3Int GetTilePosition(Vector3 position)
    {
        return mainTilemap.WorldToCell(position);
    }

    public Vector3 GetTileCenterInWorld(Vector3Int position)
    {
        return mainTilemap.GetCellCenterWorld(position);
    }

    public bool TryGetResourceTIle(Vector3Int position, out ResourceTile tile)
    {
        foreach(WorldObject obj in allObjectsOnMap)
        {
            if(obj.worldPosition == position)
            {
                ResourceTile resource = obj.GetComponent<ResourceTile>();
                if (resource != null)
                {
                    tile = resource;
                    return true;
                }
            }
           
        }

        tile = null;
        return false;
    }

    public void AddPlayerTerritory(Vector3Int[] positions)
    {
        playerTerritory.AddRange(positions);
        TerritoryTilemapManager.AddNewTerritory_Player(positions);

        OnPlayerTerritoryAdd?.Invoke(positions);
    }

    public void AddPlayerTerritory(Vector3Int position)
    {
        TerritoryTilemapManager.AddNewTerritory_Player(position);
    }


    public void AddEnemyTerritory(Vector3Int[] positions)
    {
        TerritoryTilemapManager.AddNewTerritory_Enemy(positions);
    }

    public void AddEnemyTerritory(Vector3Int position)
    {
        TerritoryTilemapManager.AddNewTerritory_Enemy(position);
    }

    public void ClearTerritory(Vector3Int[] positions)
    {
        TerritoryTilemapManager.RemoveTerritory(positions);
    }

    public void RemovePlayerTerritory(Vector3Int[] positions)
    {
        foreach (Vector3Int pos in positions)
        {
            playerTerritory.Remove(pos);
        }

        OnPlayerTerritoryRemove?.Invoke(positions);
    }

    public bool TryGetNearestKingPosition(Vector3Int startPos, out Vector3Int kingPosition)
    {
        //float minDistance = float.MaxValue;
        //kingPosition = new Vector3Int(-1, -1, -1);

        //if (kingStructures.Count > 0)
        //{
        //    foreach (King_Structure king in kingStructures)
        //    {
        //        float distance = Vector3Int.Distance(startPos, king.WorldObject.worldPosition);

        //        if (distance < minDistance)
        //        {
        //            minDistance = distance;
        //            kingPosition = king.WorldObject.worldPosition;
        //        }
        //    }

        //    return true;
        //}
        //else
        //{

        //    return false;
        //}

        kingPosition = new Vector3Int(-1, -1, -1);
        return false;
    }

    public bool TryGetKing(Vector3Int position, out King_Structure king)
    {
        //foreach(King_Structure k in kingStructures)
        //{
        //    if(k.WorldObject.worldPosition == position)
        //    {
        //        king = k;
        //        return true;
        //    }
        //}

        king = null;
        return false;
    }

    public WorldObject[] GetAllObjectsByTypeName(string name)
    {
        List<WorldObject> worldObjects = new List<WorldObject>();

        foreach(WorldObject worldObject in allObjectsOnMap)
        {
            if(worldObject.typeName == name)
            {
                worldObjects.Add(worldObject);
            }
        }

        return worldObjects.ToArray();
    }

    public class UnitEventArgs: System.EventArgs
    {
        public WorldObject worldObject;

        public UnitEventArgs(WorldObject worldObject)
        {
            this.worldObject = worldObject;
        }
    }
}
