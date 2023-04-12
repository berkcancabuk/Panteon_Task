using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Factory;
public class ObjectTriggerControl : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color _spriteRendererStartColor;
    public bool isTrigger;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererStartColor = spriteRenderer.color;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ShouldTrigger(collision))
        {
            isTrigger = true;
            spriteRenderer.color = ColorFactory.GetColor(ColorType.OnCollision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ShouldTrigger(collision))
        {
            isTrigger = false;
            spriteRenderer.color = _spriteRendererStartColor;
        }
    }
    private bool ShouldTrigger(Collider2D collision)
    {
        return collision.tag == "Cube" || collision.tag == "Soldier" || collision.tag == "PowerPlant" || collision.tag == "Enemy";
    }
}

