using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour, IComparable
{
    [Header("TMP")]
    [SerializeField] TextMeshProUGUI worldLocation;
    [SerializeField] TextMeshProUGUI txtFcost;
    [SerializeField] TextMeshProUGUI txtGcost;
    [SerializeField] TextMeshProUGUI txtHcost;
    public int hCost;
    public int gCost;
    public int fCost;
    public bool wasVisited;
    public Node parent;
    Vector3Int worldPosition;
    Vector2Int gridPosition;
    public int nodeVersion;



    #region properties
    public void SetWorldLocation(string text)
    {
        worldLocation.SetText(text);
    }

    public int Fcost
    {
        get {
            int newfcost = gCost + hCost;
            fCost = newfcost;
            return newfcost;
        }
        set { fCost = value; }
    }

    

    public int CompareTo(object obj)
    {
        Node otherNode = (Node)obj;
        if(Fcost < otherNode.Fcost)
        {
            return -1;
        }else if(Fcost > otherNode.Fcost)
        {
            return 1;
        }

        return 0;
    }

    public void ResetNodeValues()
    {
        fCost = 0;
        gCost = 0;
        hCost = 0;
        parent = null;
        wasVisited = false;
    }
    public Vector3Int WorldPosition { get => worldPosition; set => worldPosition = value; }
    public Vector2Int GridPosition { get => gridPosition; set => gridPosition = value; }
    #endregion
}
