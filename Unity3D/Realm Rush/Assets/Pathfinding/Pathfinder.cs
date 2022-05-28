using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int destinationCoordinates;

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
        startNode = new Node(startCoordinates, true);
        destinationNode = new Node(destinationCoordinates, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        startNode = gridManager.GetNode(startCoordinates);
        destinationNode = gridManager.GetNode(destinationCoordinates);
        BreadthFirstSearch();
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

    void BreadthFirstSearch()
    {
        bool isRunning = true;

        frontier.Enqueue(startNode);
        explored.Add(startCoordinates, startNode);
        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            ExploreNeighbors();
            currentSearchNode.isExplored = true;
            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
                BuildPath();
            }
        }

    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node current = destinationNode;
        
        do
        {
            path.Add(current);
            current.isPath = true;
            current = current.connectedTo;
        } while (current.connectedTo != null);

        path.Reverse();
        return path;
    }


}
