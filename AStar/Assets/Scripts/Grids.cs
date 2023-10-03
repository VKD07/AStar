using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] GameObject nodePrefab;
    [SerializeField] int height = 5;
    [SerializeField] int width = 5;
    [SerializeField] int cubeSize = 1;
    [SerializeField] List<Node> nodes;
    [SerializeField] Vector3 endPoint;

    void Awake()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3Int pos = new Vector3Int(x * cubeSize, 0, z * cubeSize);
                GameObject node = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
                Node nodeScript = node.GetComponent<Node>();
                nodes.Add(nodeScript);
                nodeScript.WorldPosition = pos;
                nodeScript.GridPosition = new Vector2Int(pos.x, pos.z);
                nodeScript.SetWorldLocation(pos.ToString());
            }
        }
    }

    public Node GetNode(Vector3Int gridPosition)
    {
        int index = gridPosition.x + gridPosition.z * width;
        return nodes[index];
    }

    public List<Node> ListOfNodes => nodes;
    public float gridWidth => width;
    public float gridHeight => height;
}
