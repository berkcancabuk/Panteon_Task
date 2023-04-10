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
    public void InstantiateSpawnPoint()
    {
        _tileList = TileListClass.instance;

        for (int i = 0; i < _tileList.tiles.Count; i++)
        {
            int randomValue = Random.Range(i, _tileList.tiles.Count);
            if (!_tileList.tiles[randomValue].GetComponent<Tile>().isTrigger)
            {
                //spawnObject.SetActive(true);
                Instantiate(spawnObject, new Vector3(_tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center.x,
                    _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center.y
                    , 0), Quaternion.identity);
                //spawnObject.transform.position = _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center;

                break;
            }
        }
    }
}
