using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] GameObject nodePrefab;
    [SerializeField] float height = 10f;
    [SerializeField] float width = 10f;
    [SerializeField] float cubeSize = 1.0f;

    [SerializeField] List<Node> nodes; 
    void Awake()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int z = 0; z < height; z++)
        {
            for(int x = 0; x < width; x++)
            {
                Vector3 pos = new Vector3(x * cubeSize, 0, z * cubeSize);
                var node = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
                Node nodeScript = node.GetComponent<Node>();
                nodes.Add(nodeScript);
                nodeScript.SetWorldLocation($"({(int)pos.x}, {(int)pos.y}, {(int)pos.z})");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
