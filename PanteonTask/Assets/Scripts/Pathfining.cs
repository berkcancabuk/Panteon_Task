using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using System.Collections;

public class Pathfining : MonoBehaviour
{
    public LayerMask unwalkableMask; //engel katmaný
    public Vector2 gridWorldSize; //grid boyutu
    public float nodeRadius; //her bir tile'ýn yarýçapý
    public Node[,] grid; //tile'larý tutacak iki boyutlu dizi

    float nodeDiameter;
    float moveSpeed = 4;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        //grid = new Node[gridSizeX, gridSizeY];
        //Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        //for (int x = 0; x < gridSizeX; x++)
        //{
        //    for (int y = 0; y < gridSizeY; y++)
        //    {
        //        Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
        //        bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask);
        //        grid[x, y] = new Node(walkable, worldPoint, x, y);
        //    }
        //}
    }

    // A* algoritmasý kullanýlarak en kýsa yolu bulan fonksiyon
    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = NodeFromWorldPoint(startPos);
        Node targetNode = NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    // Verilen iki Node arasýndaki mesafeyi hesaplayan fonksiyon
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY); 
        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            return 14 * distX + 10 * (distY - distX);
        }
    }

    // Verilen node'un komþularýný bulan fonksiyon
    List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    // Daha önce hesaplanmýþ yolu geri döndüren fonksiyon
    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    // Dünya konumundan nodu bulan fonksiyon
    Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }
    public void MoveToTarget(Vector3 targetPos)
    {
        StartCoroutine(MoveCoroutine(targetPos));
    }

    IEnumerator MoveCoroutine(Vector3 targetPos)
    {
        List<Node> path = FindPath(transform.position, targetPos);
        if (path == null)
        {
            yield break;
        }

        int targetIndex = 0;
        while (targetIndex < path.Count - 1)
        {
            targetIndex++;
            Vector3 targetPosition = path[targetIndex].worldPosition;
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}

[System.Serializable]
// Her bir nodu temsil eden sýnýf
public class Node
{
    public bool walkable; //engel olup olmadýðý
    public Vector3 worldPosition; //dünya konumu
    public int gridX; //nodun x konumu
    public int gridY; //nodun y konumu
    public int gCost; //baþlangýç nodundan bu noda gelmek için gerekli maliyet
    public int hCost; //bu noddan hedef noda gitmek için tahmini maliyet
    public Node parent; //bu nodun önceki nodu

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}








