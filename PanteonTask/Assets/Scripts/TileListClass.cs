using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileListClass : MonoBehaviour
{
    public static TileListClass instance = null;
    
    public List<GameObject> tiles = new List<GameObject>();
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            tiles.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
