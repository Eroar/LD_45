﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDraggable : MonoBehaviour
{
    public bool IsSelected = false;
    public CellFactory Factory;
    public int SpawnedIdentifier=0;
    public Vector3 ReturnPosition;


    public void SetDraggableReturnPosition()
    {
        ReturnPosition = gameObject.transform.position;
    }

    private void OnMouseOver()
    {
        //When LMB is pressed, start following
        if (Input.GetMouseButtonDown(0))
        {
            SetDraggableReturnPosition();
            IsSelected = true;
        }
    }


    void Update()
    {
        //While holding LMB, object follows the mouse position
        if (IsSelected) ObjectFollowsMouse(gameObject);

        //When LMB is relesed, (drag ends)  do the following
        if (IsSelected &&Input.GetMouseButtonUp(0) )
        {
            
            IsSelected = false;
            Vector2 WorldPos =  Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            Vector2Int hPos = Cell.getHexCoords(WorldPos, 1);

                

            if (Factory.Find(hPos) == null&&ScoreCore.Cash>= ScoreCore.Prices[SpawnedIdentifier])
            {
                Factory.Add(hPos, SpawnedIdentifier);
                GameObject.Instantiate(Resources.Load<GameObject>("BuildParticles") as GameObject, Cell.getGlobalCoords(Cell.getHexCoords(WorldPos, 1), 1), Quaternion.identity);
                ScoreCore.Cash -= ScoreCore.Prices[SpawnedIdentifier];
            }
            gameObject.transform.position = ReturnPosition;
        }
        
    }

    public void ObjectFollowsMouse(GameObject ControlledObject)
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(     new Vector2(Input.mousePosition.x, Input.mousePosition.y)    );
        ControlledObject.transform.position = position;
    }
}
