using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("TMP")]
    [SerializeField] TextMeshProUGUI worldLocation;
    [SerializeField] TextMeshProUGUI fCost;
    [SerializeField] TextMeshProUGUI gCost;
    [SerializeField] TextMeshProUGUI hCost;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region properties
    public void SetWorldLocation(string text)
    {
        worldLocation.SetText(text);
    }
    #endregion
}
