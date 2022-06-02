using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
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
    //BreadthFirstSearch
    private Queue<Node> frontier = new Queue<Node>();
    private Dictionary<Vector2Int, Node> explored = new Dictionary<Vector2Int, Node>();
    
    //DijsktraSearch
    private List<Node> priorityList = new List<Node>();

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
        //GetNewPath();
        //DijkstraGetNewPath();
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
            //Debug.Log(neighborNode.coordinates);
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
    
    public List<Node> DijkstraGetNewPath()
    {
        Debug.Log("Pathfinder");
        return DijkstraGetNewPath(startCoordinates);
    }

    public List<Node> DijkstraGetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        DijkstraSearch(coordinates);
        return BuildPath();
    }
    
    //TODO: Change list to Heap
    void DijkstraExploreNeighbors()
    {
        foreach (var direction in directions)
        {
            Node neighborNode = gridManager.GetNode(currentSearchNode.coordinates + direction);
            if (neighborNode == null || explored.ContainsKey(neighborNode.coordinates) || !neighborNode.isWalkable) continue;
            int totalDistanceFromStart = currentSearchNode.distanceFromStart + neighborNode.terrainDifficulty;
            //if we visit an already visited node
            if (neighborNode.distanceFromStart != int.MaxValue)
            {
                if(neighborNode.distanceFromStart < totalDistanceFromStart)
                    continue;
                
                neighborNode.distanceFromStart = totalDistanceFromStart;
                neighborNode.connectedTo = currentSearchNode;
                continue;
            }
            
            neighborNode.distanceFromStart = totalDistanceFromStart;
            neighborNode.connectedTo = currentSearchNode;
            priorityList.Add(neighborNode);
            

            //TODO:Remove after testing
            //Debug.Log(neighborNode.coordinates);
        }


    }

    void DijkstraSearch(Vector2Int coordinates)
    {
        gridManager.ResetNodesDistance();

        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
        startNode.distanceFromStart = 0;
        priorityList.Clear();
        explored.Clear();
        bool isRunning = true;

        priorityList.Add(gridManager.GetNode(coordinates));
        while (priorityList.Count > 0 && isRunning )
        {
            //Change the list to a binary / fibonacci heap so we don't have to reorder the list every time
            priorityList = priorityList.OrderBy(n => n.distanceFromStart).ToList();
            currentSearchNode = priorityList[0];
            DijkstraExploreNeighbors();
            currentSearchNode.isExplored = true;
            priorityList.Remove(currentSearchNode);
            explored.Add(currentSearchNode.coordinates, gridManager.GetNode(currentSearchNode.coordinates));
            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }


    void AStarSearch()
    {
        
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
            List <Node> newPath = DijkstraGetNewPath();
            //List <Node> newPath = GetNewPath();

            gridManager.GetNode(coordinates).isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                //GetNewPath();
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
