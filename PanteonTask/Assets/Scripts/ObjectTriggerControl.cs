using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTriggerControl : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public Color _spriteRendererStartColor;

    [SerializeField] public bool isTrigger;

    private void Start()
    {
        //_isTrigger = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererStartColor = spriteRenderer.color;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //_isTrigger = true;
        //if (collision.gameObject.GetComponent<ObjectTriggerControl>()._isTrigger)
        //{
        if (this.enabled != false && (collision.tag == "Cube" || collision.tag == "Soldier" || collision.tag == "PowerPlant" || collision.tag == "Enemy"))
        {
            isTrigger = true;
            spriteRenderer.color = new Color(212f / 255f, 49f / 255f, 49f / 255f, 120f / 255f);
        }
            
       // }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //    _isTrigger = false;
        //    if (!collision.gameObject.GetComponent<ObjectTriggerControl>()._isTrigger ),

        if (this.enabled != false&& (collision.tag =="Cube" || collision.tag == "Soldier" || collision.tag == "PowerPlant" || collision.tag == "Enemy"))
        {
            isTrigger = false;
            spriteRenderer.color = _spriteRendererStartColor;
        }
    }
}

