using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding;
using UnityEngine.UI;

public class MovementController2D : MonoBehaviour
{
    
    [SerializeField] float gridSize = 0.5f;
    [SerializeField] float speed = 0.05f;
    
    Pathfinder<Vector2> pathfinder;
    [SerializeField] LayerMask obstacles;
    [SerializeField] bool searchShortcut =false; 
    [SerializeField] bool snapToGrid =false; 
    Vector2 targetNode; 
    List <Vector2> path;
    List<Vector2> pathLeftToGo= new List<Vector2>();
    [SerializeField] bool drawDebugLines;

    
    void Start()
    {
        pathfinder = new Pathfinder<Vector2>(GetDistance,GetNeighbourNodes,500); 
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
                GetMoveCommand(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (pathLeftToGo.Count > 0) 
        {
            GameManager.instance.isMoveSoldier = true;
            GameManager.instance.soldierInstLevel1.enabled = false;
            GameManager.instance.soldierInstLevel2.enabled = false;
            GameManager.instance.soldierInstLevel3.enabled = false;
            Vector3 dir =  (Vector3)pathLeftToGo[0]-transform.position ;
            transform.position += dir.normalized * speed;
            if (((Vector2)transform.position - pathLeftToGo[0]).sqrMagnitude <speed*speed) 
            {
                transform.position = pathLeftToGo[0];
                pathLeftToGo.RemoveAt(0);
                if (pathLeftToGo.Count == 0) 
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 87f / 255);
                    GameManager.instance.clickLastGO = null;
                    GameManager.instance.TileColliderOn();
                    GameManager.instance.isMoveSoldier = false;
                    GameManager.instance.soldierInstLevel1.enabled = true;
                    GameManager.instance.soldierInstLevel2.enabled = true;
                    GameManager.instance.soldierInstLevel3.enabled = true;

                }
            }
        }

        if (drawDebugLines)
        {
            for (int i=0;i<pathLeftToGo.Count-1;i++)
            {
                Debug.DrawLine(pathLeftToGo[i], pathLeftToGo[i+1]);
            }
        }
    }

    void GetMoveCommand(Vector2 target) 
    {
        Vector2 closestNode = GetClosestNode(transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path))
        {
            if (searchShortcut && path.Count>0)
                pathLeftToGo = ShortenPath(path);
            else
            {
                pathLeftToGo = new List<Vector2>(path);
                if (!snapToGrid) pathLeftToGo.Add(target);
            }

        }
        
    }

    /// <summary>
    /// Finds closest point on the grid
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    Vector2 GetClosestNode(Vector2 target) 
    {
        return new Vector2(Mathf.Round(target.x/gridSize)*gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    /// <summary>
    /// A distance approximation. 
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    float GetDistance(Vector2 A, Vector2 B) 
    {
        return (A - B).sqrMagnitude; 
    }

    /// <summary>
    /// Finds possible conenctions and the distances to those connections on the grid.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    Dictionary<Vector2,float> GetNeighbourNodes(Vector2 pos) 
    {
        Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
        for (int i=-1;i<2;i++)
        {
            for (int j=-1;j<2;j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2 dir = new Vector2(i, j)*gridSize;
                if (!Physics2D.Linecast(pos,pos+dir, obstacles))
                {
                    neighbours.Add(GetClosestNode( pos + dir), dir.magnitude);
                }
            }

        }
        return neighbours;
    }

    
    List<Vector2> ShortenPath(List<Vector2> path)
    {
        List<Vector2> newPath = new List<Vector2>();
        
        for (int i=0;i<path.Count;i++)
        {
            newPath.Add(path[i]);
            for (int j=path.Count-1;j>i;j-- )
            {
                if (!Physics2D.Linecast(path[i],path[j], obstacles))
                {
                    
                    i = j;
                    break;
                }
            }
            newPath.Add(path[i]);
        }
        newPath.Add(path[path.Count - 1]);
        return newPath;
    }

}
