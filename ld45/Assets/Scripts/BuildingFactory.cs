﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : CellFactory
{
    // Start is called before the first frame update
    [SerializeField] TerrainFactory tFactory;
    [SerializeField] Controller controller;

    [SerializeField] int buildingsAvailable;

    public void updateAvailable()
    {
        buildingsAvailable = 0;
        Debug.Log("Updating available");
        for(int i = 0;i<cells.Count;i++)
        {
            buildingsAvailable += ((Building)cells[i]).current.availableBuildings;
        }
    }
   
    public int getBuildingCount()
    {
        return cellCount[-1]-1;
    }
    public int getAvailableCount()
    {
        return buildingsAvailable;
    }

    private void Awake()
    {
        base.Awake();
    }

    public void Initialize()
    {
        if (tFactory == null||controller == null)
        {
            Debug.Log("In buildingFactory, Awake: Lacking tFactory object");
            return;
        }
        Build(new Vector2Int(tFactory.getSize().x / 2, tFactory.getSize().y / 2), 0);
    }

    public void Build(Vector2Int pos,int index)
    {
        for(int i = 0;i<cells.Count;i++)
        {
            if(pos==cells[i].pos)
            {
                return;
            }
        }
        Terrain under = (Terrain)tFactory.Find(pos);
        if(under.buildable!=0)
        {
            return;
        }
        Add(pos, index).setTerrain(under);
    }

    new Building Add(Vector2Int pos,int index)
    {
        Building b = (Building)base.Add(pos, index);
        Vector2Int npos;
        for(int i = 0;i<6;i++)
        {
            npos = getNeighbourGridPos(pos, i);
            for(int j = 0;j<cells.Count;j++)
            {
                if(cells[j].pos==npos)
                {
                    b.setNeighbour(cells[j], i);
                }
            }
        }
        b.setController(controller);
        cells.Add(b);
        return b;
    }

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}