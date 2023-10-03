using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("TMP")]
    [SerializeField] TextMeshProUGUI worldLocation;
    [SerializeField] TextMeshProUGUI fCost;
    [SerializeField] TextMeshProUGUI gCost;
    [SerializeField] TextMeshProUGUI hCost;

    Vector3Int worldPosition;
    Vector2Int gridPosition;

    private void Start()
    {

    }


    #region properties
    public void SetWorldLocation(string text)
    {
        worldLocation.SetText(text);
    }

    public void SetHCost(string text)
    {
        hCost.SetText(text);
    }

    public Vector3Int WorldPosition { get => worldPosition; set => worldPosition = value; }
    public Vector2Int GridPosition { get => gridPosition; set => gridPosition = value; }
    #endregion
}
