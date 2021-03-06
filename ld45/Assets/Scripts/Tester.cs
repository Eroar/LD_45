﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    void Start()
    {
    }
    Vector2 getGlobalCoords(Vector2Int pos,float size)
    {
        return new Vector2((-(pos.y % 2) * 0.5f + pos.x)*size, (pos.y * Mathf.Sqrt(3) * 0.5f) * size);
    }
    Vector2Int getHexCoords(Vector2 pos,float size)
    {
        float xstep, ystep;
        xstep = size;
        ystep = size *Mathf.Sqrt(3)*0.5f;
        int x, y;
        x = (int)Mathf.Round(pos.x / xstep);
        y = (int)Mathf.Round(pos.y / ystep);
        float min = float.MaxValue;
        Vector2Int mV = new Vector2Int(0,0);
        Vector2 tPos = new Vector2(0,0);
        for(int i = x-3;i<x+3;i++)
        {
            for(int j = y-3;j<y+3;j++)
            {
                tPos = getGlobalCoords(new Vector2Int(i, j), size);
                if(Mathf.Pow(tPos.x-pos.x,2)+Mathf.Pow(tPos.y-pos.y,2)<min)
                {
                    min = Mathf.Pow(tPos.x - pos.x, 2) + Mathf.Pow(tPos.y - pos.y, 2);
                    mV = new Vector2Int(i, j);
                }
            }
        }
        return mV;
    }
    public static Vector2Int snap(Vector2 pos,float size)
    {
        float xstep, ystep;
        xstep = size * Mathf.Sqrt(3);
        ystep = size * 3*0.5f;
        int x, y;
        x = (int)Mathf.Round(pos.x / xstep);
        y = (int)Mathf.Round(pos.y / ystep);
        return new Vector2Int(0, 0);
    }
    private void Awake()
    {
        Vector2 position = (getGlobalCoords(new Vector2Int(40, 17),10));
        position += new Vector2(2, 3);
        Vector2Int p = getHexCoords(position, 10);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouse = Input.mousePosition;
        }
    }
}
