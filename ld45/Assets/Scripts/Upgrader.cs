﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField]  private  CellFactory CF;

    public void Awake()
    {
        //Scaling size for 2k, 4k 
        float ScreenScaleX = Camera.main.scaledPixelWidth / 3840.0f   ;
        float ScreenScaleY = Camera.main.scaledPixelHeight / 2160.0f   ;

        transform.localScale = new Vector3(1.2f* ScreenScaleX, 1.2f* ScreenScaleY,1);

        CF = null;
        Transform SemiTarget = null;
        
        //Getting access to the CellFactory without .Find
        foreach (Transform trans in Camera.main.GetComponentsInChildren<Transform>())
        {
            if (trans.gameObject.name == "Canvas") SemiTarget = trans;
        }
        foreach (Transform trans in Camera.main.GetComponentsInChildren<Transform>())
        {
            if (trans.gameObject.name == "CellFactory") SemiTarget = trans;
        }
        CF = SemiTarget.GetComponent<CellFactory>();



    }

    public void Upgrade()
    {
        Debug.LogWarning("UP");
        Vector2Int pos;
        pos = Cell.getHexCoords(transform.position, 55f / 64f);
        Cell cell = CF.Find(pos);
        if(ScoreCore.Cash>=cell.uCost[cell.level]&&cell.level<3)
        {
            Debug.Log($"Penis{cell.level}");
            ScoreCore.Cash -= cell.uCost[cell.level];
            cell.Upgrade();
        }
        
    }


    public void Bulldoze()
    {
        CF.DestroyCell(Cell.getHexCoords(transform.position, 55f / 64f));    
    }
    private void FixedUpdate()
    {
        if (CF.Find(Cell.getHexCoords(transform.position, 55f / 64f))==null) Destroy(gameObject);
    }
}
