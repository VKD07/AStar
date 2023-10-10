using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class AStar : MonoBehaviour
{
    [SerializeField] List<Node> neighbours;
    [SerializeField] List<Node> openList;
    [SerializeField] List<Node> closeList;
    [SerializeField] Grids grid;
    Node currentNode;
    Node startGoal;
    Node endGoal;
    private void Start()
    {

    }
    void FindPath(Vector3Int start, Vector3Int end)
    {
        startGoal = grid.GetNode(start);
        endGoal = grid.GetNode(end);
        endGoal.gCost = 0;
        endGoal.hCost = 0;
        endGoal.wasVisited = false;
        endGoal.parent = null;
        //color the start and end

        //startGoal.Mesh.GetComponent<Mesh>().material.color = Color.green;
        //endGoal.gameObject.GetComponent<Renderer>().material.color = Color.red;
        
        openList.Add(startGoal);
        while (openList.Count > 0)
        {
            //sorting the Fcost so that the current node will always be the the index 0 
            //meaning that the lowest Fcost will always be the index 0
            openList.Sort();
            currentNode = openList[0];
            openList.RemoveAt(0);
            currentNode.wasVisited = true;

            if (currentNode == endGoal)
            {
                return;
            }

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (closeList.Contains(neighbour))
                {
                    continue;
                }

                int newCost = currentNode.gCost + GetDistance(currentNode.GridPosition, neighbour.GridPosition);
                if (newCost < neighbour.gCost || closeList.Contains(neighbour))
                {
                    neighbour.Fcost = newCost;
                    neighbour.hCost = GetDistance(neighbour.GridPosition, endGoal.GridPosition);
                    neighbour.parent = currentNode;

                    if(!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }
        }
    }

    List<Node> GetNeighbours(Node node)
    {
        List<Node> listOfNeighbors = new List<Node>();
        if (node.GridPosition.x + 1 < grid.gridWidth)
        {
            Node neighbourRight = grid.GetNode(new Vector3Int(node.GridPosition.x + 1, 0, node.GridPosition.y));
            listOfNeighbors.Add(neighbourRight);
        }

        if (node.GridPosition.y + 1 < grid.gridHeight)
        {
            Node neighbourLeft = grid.GetNode(new Vector3Int(node.GridPosition.x, 0, node.GridPosition.y + 1));
            listOfNeighbors.Add(neighbourLeft);
        }

        if (node.GridPosition.x - 1 >= 0)
        {
            Node neighbourUp = grid.GetNode(new Vector3Int(node.GridPosition.x - 1, 0, node.GridPosition.y));
            listOfNeighbors.Add(neighbourUp);
        }

        if (node.GridPosition.y - 1 >= 0)
        {
            Node neighbourDown = grid.GetNode(new Vector3Int(node.GridPosition.x, 0, node.GridPosition.y - 1));
            listOfNeighbors.Add((neighbourDown));
        }
        return listOfNeighbors;
    }

    int GetDistance(Vector2Int position, Vector2Int destination)
    {
        Vector2Int nodeDistance = position - destination;
        int distance = Mathf.RoundToInt(Mathf.Abs(nodeDistance.x) + Mathf.Abs(nodeDistance.y));
        return distance;
    }
}