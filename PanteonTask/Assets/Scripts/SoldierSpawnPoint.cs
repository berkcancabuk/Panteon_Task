using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawnPoint : MonoBehaviour
{
    public static SoldierSpawnPoint instance = null;
    private TileListClass _tileList;
    [SerializeField] GameObject spawnObjectLevel1, spawnObjectLevel2, spawnObjectLevel3;
    [SerializeField] SoldierObjectClass soldierObjectClass;
    private bool _isSpawnLevel1,_isSpawnLevel2, _isSpawnLevel3;

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
    private void Start()
    {
        RepeatingLevel1();
    }
    public void RepeatingLevel1()
    {
        Invoke("InstantiateSpawnPointForLevel1", 1f);
    }
    public void RepeatingLevel2()
    {
        Invoke("InstantiateSpawnPointForLevel2", 1f);
    }
    public void RepeatingLevel3()
    {
        Invoke("InstantiateSpawnPointForLevel3", 1f);
    }
    public void InstantiateSpawnPointForLevel1()
    {
        _tileList = TileListClass.instance;
        for (int i = 0; i < _tileList.tiles.Count; i++)
        {
            int randomValue = Random.Range(i, _tileList.tiles.Count);
            if (!_tileList.tiles[randomValue].GetComponent<Tile>().isTrigger)
            {
                //spawnObjectLevel1.SetActive(true);
                GameObject GO = Instantiate(spawnObjectLevel1, _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity);
                soldierObjectClass = GO.GetComponent<SoldierObjectClass>();
                //spawnObjectLevel1.transform.position = _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center;

                break;
            }
        }
        //spawnObjectLevel1.transform.SetParent(null);
        if (!_isSpawnLevel1)
        {
            _isSpawnLevel1 = true;
            StartCoroutine(SoldierSpawnLevel1());
            
        }
       
    }
    public void InstantiateSpawnPointForLevel2()
    {
        _tileList = TileListClass.instance;
        _isSpawnLevel1 = false;
        for (int i = 0; i < _tileList.tiles.Count; i++)
        {
            int randomValue = Random.Range(i, _tileList.tiles.Count);
            if (!_tileList.tiles[randomValue].GetComponent<Tile>().isTrigger)
            {
                //spawnObjectLevel2.SetActive(true);
                //spawnObjectLevel2.transform.position = _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center;
                GameObject GO = Instantiate(spawnObjectLevel2, _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity);
                soldierObjectClass = GO.GetComponent<SoldierObjectClass>();

                break;
            }
        }
        if (!_isSpawnLevel2)
        {
            _isSpawnLevel2 = true;
            StartCoroutine(SoldierSpawnLevel2());

        }
    }
    public void InstantiateSpawnPointForLevel3()
    {
        _tileList = TileListClass.instance;
        _isSpawnLevel2 = false;
        soldierObjectClass = spawnObjectLevel3.GetComponent<SoldierObjectClass>();
        for (int i = 0; i < _tileList.tiles.Count; i++)
        {
            int randomValue = Random.Range(i, _tileList.tiles.Count);
            if (!_tileList.tiles[randomValue].GetComponent<Tile>().isTrigger)
            {
                //spawnObjectLevel3.SetActive(true);
                //spawnObjectLevel3.transform.position = _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center;
                GameObject GO = Instantiate(spawnObjectLevel3, _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity);
                soldierObjectClass = GO.GetComponent<SoldierObjectClass>();

                break;
            }
        }
        if (!_isSpawnLevel3)
        {
            _isSpawnLevel3 = true;
            StartCoroutine(SoldierSpawnLevel3());

        }
    }

    public IEnumerator SoldierSpawnLevel1()
    {
        if (_isSpawnLevel1)
        {
            if (GameManager.instance.soldierLevel == 1)
            {
                soldierObjectClass.soldierHealth += 10;
                soldierObjectClass.soldierPower += 2;
                soldierObjectClass.soldierCount++;
                GameManager.instance.ownedSoldiersLevel1 = soldierObjectClass.soldierCount;
            }
            yield return new WaitForSeconds(6);
            StartCoroutine(SoldierSpawnLevel1());
        }
        else
        {
            yield break;
        }
    }
    public IEnumerator SoldierSpawnLevel2()
    {
        if (_isSpawnLevel2)
        {
            if (GameManager.instance.soldierLevel == 2)
            {
                soldierObjectClass.soldierHealth += 10;
                soldierObjectClass.soldierPower += 5;
                soldierObjectClass.soldierCount++;
                GameManager.instance.ownedSoldiersLevel2 = soldierObjectClass.soldierCount;
            }
            yield return new WaitForSeconds(8);
            StartCoroutine(SoldierSpawnLevel2());
        }
        else
        {
            yield break;
        }
    }
    public IEnumerator SoldierSpawnLevel3()
    {
        if (_isSpawnLevel3)
        {
            if (GameManager.instance.soldierLevel == 3)
            {
                soldierObjectClass.soldierHealth += 10;
                soldierObjectClass.soldierPower += 10;
                soldierObjectClass.soldierCount++;
                GameManager.instance.ownedSoldiersLevel3 = soldierObjectClass.soldierCount;
            }

                yield return new WaitForSeconds(10);
                StartCoroutine(SoldierSpawnLevel3());
        }
        else
        {
            yield break;
        }
    }
}
