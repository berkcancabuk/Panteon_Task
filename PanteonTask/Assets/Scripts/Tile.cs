using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;

    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private GameObject _highlight;

    public bool isTrigger;
    
    //public bool isTrigger;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }
    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }
    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
    }
}
