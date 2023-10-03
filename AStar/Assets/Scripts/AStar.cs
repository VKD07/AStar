using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class AStar : MonoBehaviour
{
    [SerializeField] List<Node> neighbours;
    [SerializeField] Grids grid;
    [SerializeField] Vector3Int start;
    [SerializeField] Vector3Int end;
    Node startGoal;
    Node endGoal;
    int index;
    private void Start()
    {
        SetStartAndEndGoal();
        //AddNeighbours(grid.ListOfNodes[12]);
        AddNeighbours(grid.ListOfNodes[index]);
        CalculateHCost();
    }

    private void SetStartAndEndGoal()
    {
        //setting the start goal and end goal by converting node world position to index
        startGoal = grid.GetNode(start);
        endGoal = grid.GetNode(end);

        startGoal.gameObject.name = "start";
        endGoal.gameObject.name = "end";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            foreach (Node neighbour in neighbours)
            {
                neighbour.gameObject.GetComponent<Renderer>().material.color = Color.gray;
            }
            neighbours.Clear();
            index++;
            AddNeighbours(grid.ListOfNodes[index]);

        }
    }

    //void AddNeighbours()
    //{
    //    foreach (Node node in grid.ListOfNodes)
    //    {

    //        if ((int)node.transform.position.x + 1 < grid.gridWidth)
    //        {
    //            Node neighbourRight = grid.GetNode(new Vector3Int((int)node.transform.position.x + 1, (int)node.transform.position.y, (int)node.transform.position.z));
    //            neighbours.Add(neighbourRight);
    //        }

    //        if ((int)node.transform.position.z + 1 < grid.gridHeight)
    //        {
    //            Node neighbourLeft = grid.GetNode(new Vector3Int((int)node.transform.position.x, (int)node.transform.position.y, (int)node.transform.position.z + 1));
    //            neighbours.Add(neighbourLeft);
    //        }

    //        if ((int)node.transform.position.x - 1 > 0)
    //        {
    //            Node neighbourUp = grid.GetNode(new Vector3Int((int)node.transform.position.x - 1, (int)node.transform.position.y, (int)node.transform.position.z));
    //            neighbours.Add(neighbourUp);
    //        }

    //        if ((int)node.transform.position.z - 1 > 0)
    //        {
    //            Node neighbourDown = grid.GetNode(new Vector3Int((int)node.transform.position.x, (int)node.transform.position.y, (int)node.transform.position.z - 1));
    //            neighbours.Add((neighbourDown));
    //        }
    //    }
    //}

    void AddNeighbours(Node node)
    {
        if (node.GridPosition.x + 1 < grid.gridWidth)
        {
            Node neighbourRight = grid.GetNode(new Vector3Int(node.GridPosition.x + 1, 0, node.GridPosition.y));
            neighbours.Add(neighbourRight);
        }

        if (node.GridPosition.y + 1 < grid.gridHeight)
        {
            Node neighbourLeft = grid.GetNode(new Vector3Int(node.GridPosition.x, 0, node.GridPosition.y + 1));
            neighbours.Add(neighbourLeft);
        }

        if (node.GridPosition.x - 1 >= 0)
        {
            Node neighbourUp = grid.GetNode(new Vector3Int(node.GridPosition.x - 1, 0, node.GridPosition.y));
            neighbours.Add(neighbourUp);
        }

        if (node.GridPosition.y - 1 >= 0)
        {
            Node neighbourDown = grid.GetNode(new Vector3Int(node.GridPosition.x, 0, node.GridPosition.y - 1));
            neighbours.Add((neighbourDown));
        }

        neighbours.Add(node);
        foreach (Node neighbour in neighbours)
        {
            neighbour.gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        node.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    void CalculateHCost()
    {
        foreach (Node node in grid.ListOfNodes)
        {
            Vector2Int nodeDistance = node.GridPosition - endGoal.GridPosition;
            float hCost = nodeDistance.x + nodeDistance.y;
            node.SetHCost(hCost.ToString());
        }
        //subtract position of grid node 
        //add the x and y 
    }
}