using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    private TileListClass _tileList;
    [SerializeField] GameObject spawnObject;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    /// <summary>
    /// Function tries to instantiate enemy on a random point size of tiles's times which means it tries to instantiate on a random point n^2 times,
    /// If it is uncessfull, then we can assume that the level is complateted successfully.
    /// </summary>
    public void InstantiateSpawnPoint()
    {
        _tileList = TileListClass.instance;
        
        for (int i = 0; i < _tileList.tiles.Count; i++)
        {
            if (InstantitateEnemyOnRandomPoint())
                break;
        }
    }
    /// <summary>
    /// Tries to instantiate an enemy on a random point
    /// </summary>
    /// <returns> True if initiates succesfully, false otherwise </returns>
    public bool InstantitateEnemyOnRandomPoint()
    {
        int randomValue = Random.Range(0, _tileList.tiles.Count);
        if (!_tileList.tiles[randomValue].GetComponent<Tile>().isTrigger)
        {
            Instantiate(spawnObject, new Vector3(_tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center.x,
                _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center.y
                , 0), Quaternion.identity);

            return true;
        }
        return false;

    }
}
