using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class DistanceLabeler : MonoBehaviour
{
    [SerializeField]Color defaultColor = Color.white;
    [SerializeField]Color blockedColor = Color.gray;
    [SerializeField]Color exploredColor = Color.yellow;
    [SerializeField]Color pathColor =  new Color(1f,0.5f,0f);

    
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    private GridManager gridManager;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();
        label.enabled = false;
        DisplayDistance();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayDistance();
            label.enabled = true;
        }
        
        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Pressed D");
            label.enabled = !label.enabled;
        }
    }

    void SetLabelColor()
    {
        if (gridManager == null) return;
        
        Node node = gridManager.GetNode(coordinates);
        
        if (node == null) return;
        
        if (!node.isWalkable)
            label.color = blockedColor;
        else if (node.isPath)
            label.color = pathColor;
        else if (node.isExplored)
            label.color = exploredColor;
        else
            label.color = defaultColor;
        
        //label.color = waypoint.IsPlaceable ? defaultColor : blockedColor  ;
    }

    void DisplayDistance()
    {
        if (gridManager == null) return;
        Node node = gridManager.GetNode(coordinates);
        if (node == null) return;
        label.text = $"{node.distanceFromStart.ToString()} \n {node.terrainDifficulty}";

    }
}
