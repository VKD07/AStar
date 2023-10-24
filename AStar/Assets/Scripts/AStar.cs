using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class AStar : MonoBehaviour
{
    [SerializeField] List<Node> openList;
    [SerializeField] Grids grid;
    public List<Node> path;
    Node currentNode;
    Node startGoal;
    Node endGoal;
    public Transform start, goal;
    int version;

    private void Start()
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            openList.Clear();
            path.Clear();
            FindPath(start.position, goal.position);
        }
    }
    void FindPath(Vector3 start, Vector3 end)
    {
        startGoal = grid.GetNode(Vector3Int.FloorToInt(start));
        endGoal = grid.GetNode(Vector3Int.FloorToInt(end));

        //color the start and end
        startGoal.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        //endGoal.gameObject.GetComponent<Renderer>().material.color = Color.red;

        //Reseting node
        endGoal.gCost = 0;
        endGoal.hCost = 0;
        endGoal.wasVisited = false;
        endGoal.parent = null;

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
                print("Success");
                RetracePath(startGoal, endGoal);
                version++;
                return;
            }

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (neighbour.nodeVersion < version)
                {
                    neighbour.ResetNodeValues();
                    neighbour.nodeVersion = version;
                }
                if (neighbour.wasVisited)
                {
                    continue;
                }

                int newCost = currentNode.gCost + GetDistance(currentNode.GridPosition, neighbour.GridPosition);
                if (newCost < neighbour.gCost || !openList.Contains(neighbour))
                {
                    neighbour.gCost = newCost;
                    neighbour.hCost = GetDistance(neighbour.GridPosition, endGoal.GridPosition);
                    neighbour.parent = currentNode;

                    neighbour.SetNodeTextCost(neighbour.gCost, neighbour.hCost);

                    if (!openList.Contains(neighbour))
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

    void RetracePath(Node startNode, Node endNode)
    {

        if (startNode == null || endNode == null)
        {
            // Handle the case where startNode or endNode is null.
            return;
        }
        path = new List<Node>();
        //Retracing backwards
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        foreach (Node node in path)
        {
            if (node == endGoal)
            {
                node.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                node.gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
        }
    }

    int GetDistance(Vector2Int position, Vector2Int destination)
    {
        Vector2Int nodeDistance = position - destination;
        int distance = Mathf.RoundToInt(Mathf.Abs(nodeDistance.x) + Mathf.Abs(nodeDistance.y));
        return distance;
    }
}