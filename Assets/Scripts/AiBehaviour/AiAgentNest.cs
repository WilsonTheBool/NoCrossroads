﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class AiAgentNest : MonoBehaviour
{
    
    public WorldObject WorldObject;

    public KillableCharacter killable;

    public Vector3Int[] nestTerritory;

    public TerritoryCreator territoryCreator;

    public AiBehaviourController behaviourController;

    GameWorldMapManager GameWorldMapManager;



    [SerializeField]
    int nestRange;


    public UnityEvent OnNestAttacked;

    public UnityEvent OnNestCreate;
    public UnityEvent OnNestDestroy;

    public NestTarget curentTarget;

    private void Awake()
    {
        
        WorldObject.OnSetUpComplete.AddListener(OnWorldObjectSetUpComplete);
        
    }

    private void OnWorldObjectSetUpComplete()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        behaviourController = AiBehaviourController.instance;

        CreateTerrittory();
    }

    public void DoAction()
    {
        
    }

    public void CreateTerrittory()
    {
        territoryCreator.createRadius = nestRange;
        territoryCreator.CreateTerritory(WorldObject.worldPosition, GameWorldMapManager, out nestTerritory);
    }




    public class NestTarget
    {
        public Vector3Int targetPos;

        public int maxAgents;

        int curentAgents;

        public bool CanAddAgent()
        {
            return curentAgents < maxAgents;
        }

        public void AddAgent()
        {
            curentAgents++;
        }

        public void RemoveAgent()
        {
            curentAgents--; 
        }
    }
}