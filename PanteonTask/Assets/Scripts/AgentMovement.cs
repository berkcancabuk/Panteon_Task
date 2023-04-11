using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentMovement : MonoBehaviour
{
    private Vector3 target;
    public NavMeshAgent agent;
    private bool _isStartedNavmesh;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetMouseButtonDown(1) && !_isStartedNavmesh)
        {
            agent.enabled = true;
            GetComponent<NavMeshObstacle>().enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            SetTargetPosition();
            _isStartedNavmesh = true;
        }
        if (_isStartedNavmesh && agent.remainingDistance != 0)
        {
            if (agent.remainingDistance <= agent.stoppingDistance +0.01f)
            {
                print("buraya giriyor ");
                _isStartedNavmesh = false;
                agent.enabled = false;
                enabled = false;
                transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f,87f/255);
                transform.GetComponent<NavMeshObstacle>().enabled = true;
            }
        }
    }
    private void LateUpdate()
    {
        
    }
    void SetTargetPosition()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0)), Vector2.zero);

        if (hit.collider != null && (hit.collider.tag == "Tile" || hit.collider.tag == "Enemy"))
        {
            Vector3 hitTransform = hit.transform.GetComponent<SpriteRenderer>().bounds.center;
            target = new Vector3(hitTransform.x, hitTransform.y, 0);

        }
        SetAgentPosition();
    }
    void SetAgentPosition()
    {
        agent.SetDestination(target);
    }

}
