﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : Cell
{
   
    // Update is called once per frame
    void onImpulse()
    {
        ScoreCore.Cash++;
        
    }
    private void Awake()
    {
        setPulseAction(action);
    }
}
