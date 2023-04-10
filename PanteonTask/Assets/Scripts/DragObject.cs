using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragObject : MonoBehaviour
{
  
    private Vector3 offset;
    private float distance = -5f;
    private bool dragging = false;
    private Vector3 _firstPosition;
    private ObjectTriggerControl _objectTrigger;
    private NavMeshObstacle _meshObstacle;
    private bool _isStartPositionCheck;
    private void Start()
    {
        _objectTrigger = GetComponent<ObjectTriggerControl>();
        _meshObstacle = GetComponent<NavMeshObstacle>();
    }

    private void OnMouseEnter()
    {
        if (!_objectTrigger.isTrigger&& !_isStartPositionCheck)
        {
           _firstPosition= gameObject.transform.position;
            _isStartPositionCheck = true;
        }
    }
    void OnMouseDown()
    {
        
        dragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
        _objectTrigger.enabled = true;
        _meshObstacle.enabled = false;
       
    }

    void OnMouseUp()
    {
        if (_objectTrigger.isTrigger)
        {
            gameObject.transform.position = _firstPosition;
            _objectTrigger.spriteRenderer.color = _objectTrigger._spriteRendererStartColor;
            _objectTrigger.isTrigger = false;
            if (!_isStartPositionCheck)
            {
                Destroy(gameObject);
            }
            _isStartPositionCheck = true;
            dragging = false;
            _objectTrigger.enabled = false;
            _meshObstacle.enabled = true;
        }
        else
        {
            _firstPosition = gameObject.transform.position;
            dragging = false;
            _objectTrigger.enabled = false;
            _meshObstacle.enabled = true;
            _isStartPositionCheck = true;
        }
    }

    void Update()
    {
        if (dragging)
        {
            if (gameObject.transform.localScale.y % 2 != 0)
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                transform.position = new Vector3((int)(curPosition.x)+.5f, (int)(curPosition.y), 0);
            }
            else
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                transform.position = new Vector3((int)(curPosition.x) + .5f, (int)(curPosition.y) + .5f, 0);
            }
           
        }
       
    }

}

