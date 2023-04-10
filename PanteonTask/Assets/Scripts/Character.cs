using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Pathfining pathfining;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5)), Vector2.zero);
            print("abc");
                if (hit.collider.CompareTag("Tile"))
                {
                    print("abcd");
                    pathfining.MoveToTarget(hit.collider.transform.position);
                }
            
        }
    }
}
