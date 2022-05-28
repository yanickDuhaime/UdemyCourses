using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int destinationCoordinates;
    public Vector2Int StartCoordinates => startCoordinates;
    public Vector2Int DestinationCoordinates => startCoordinates;

    private Node startNode;
    private Node destinationNode;
    private Node currentSearchNode;

    // Les nodes à l'extremité de la recherche
    private Queue<Node> frontier = new Queue<Node>();
    private Dictionary<Vector2Int, Node> explored = new Dictionary<Vector2Int, Node>();

    private Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    private GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            startNode = gridManager.GetNode(startCoordinates);
            destinationNode = gridManager.GetNode(destinationCoordinates);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void ExploreNeighbors()
    {
        //List<Node> neighbors = directions.Select(direction => gridManager.GetNode(currentSearchNode.coordinates + direction)).Where(neighborNode => neighborNode != null).ToList();
        List<Node> neighbors = new List<Node>();
        foreach (var direction in directions)
        {
            Node neighborNode = gridManager.GetNode(currentSearchNode.coordinates + direction);
            if (neighborNode == null) continue;
            neighbors.Add(neighborNode);

            //TODO:Remove after testing
            Debug.Log(neighborNode.coordinates);
        }

        foreach (var neighbor in neighbors)
        {
            if (!explored.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                explored.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
        
        frontier.Clear();
        explored.Clear();
        bool isRunning = true;

        frontier.Enqueue(gridManager.GetNode(coordinates));
        explored.Add(coordinates, gridManager.GetNode(coordinates));
        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            ExploreNeighbors();
            currentSearchNode.isExplored = true;
            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }

    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node current = destinationNode;

        path.Add(current);
        current.isPath = true;
        
        while (current.connectedTo != null)
        {
            current = current.connectedTo;
            path.Add(current);
            current.isPath = true;
        }

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        
        if (gridManager.GetNode(coordinates) != null)
        {
            bool previousState = gridManager.GetNode(coordinates).isWalkable;
            gridManager.GetNode(coordinates).isWalkable = false;
            List <Node> newPath = GetNewPath();
            gridManager.GetNode(coordinates).isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;

    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath",false,SendMessageOptions.DontRequireReceiver);
    }


}
