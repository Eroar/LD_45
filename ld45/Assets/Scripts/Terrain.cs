﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : Cell
{

    // Start is called before the first frame update

    public int buildable = 0;

    public int distToGen;
   
    void Start()
    {
        
    }
    private void Awake()
    {
        base.Awake();
        distToGen = int.MaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        base.OnDestroy();
    }
}
