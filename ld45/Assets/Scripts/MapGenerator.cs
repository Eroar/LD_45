﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public CellFactory factory;
    private void Start()
    {
        CreateShape();
    }

    Vector2Int[] vertices;
    public int maxDepth;
    public int maxWidth;
    public int xSize;
    public int ySize;

   public float[,] GenerateNoiseMap(int mapDepth, int mapWidth, float scale)
    {
        float[,] noiseMap = new float[maxDepth, maxWidth];

        for (int yIndex = 0; yIndex < mapDepth; yIndex++)
        {
            for (int xIndex = 0; xIndex < mapWidth; xIndex++)
            {
                float sampleX = xIndex / scale;
                float sampleY = yIndex / scale;
                float noise = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[yIndex, xIndex] = noise;
            }
        }
        return noiseMap;
    }

    void CreateShape()
    {
        vertices = new Vector2Int[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector2Int(x, y);
                i++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;
        for (int i = 0; i < vertices.Length; i++)
        {
            factory.Add(vertices[i], 3);
        }
    } 
}
