﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public MapGenerator MapGenerator;
    public float MoveSpeed;


    //50 x30


    //x 7 >>35
    //y 3 >> 19

    private float XMaxLimit;
    private float XMinLimit;
    private float YMaxLimit;
    private float YMinLimit;



    public void Start()
    {
        XMaxLimit = GetComponent<MapGenerator>().xSize - 17;
        XMinLimit = 8;
        YMaxLimit = GetComponent<MapGenerator>().ySize - 13;
        YMinLimit = 5;
    }

    void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > XMinLimit)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * MoveSpeed);
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < XMaxLimit)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * MoveSpeed);
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && transform.position.y < YMaxLimit)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * MoveSpeed);
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position.y > YMinLimit)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * MoveSpeed);
        }

        if (Input.GetKeyDown("g"))
        {
            transform.position = new Vector3(0.84252352941176470588235294117647f * (MapGenerator.xSize / 2 + 0.5f), 0.72023225806451612903225806451613f * (MapGenerator.ySize / 2 + 0.5f), -3);
        }
    }
}
