using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SoldierBarrackObjectClass : MonoBehaviour
{
    public int index = 0;
    public List<GameObject> soldiers = new();
    private TileListClass _tileList;
    private void Start()
    {
        _tileList = TileListClass.instance;
    }
    public void InstantiateSpawnPoint(GameObject spawnGO)
    {
        SoldierObjectClass soldierObject = spawnGO.GetComponent<SoldierObjectClass>();
        int newSoldierLevel = soldierObject.soldierLevel;
        soldiers = soldiers.Where(item => item != null).ToList();
        Dictionary<int, int> soldierLevelToSoldierHealth = soldiers.GroupBy(soldier => soldier.GetComponent<SoldierObjectClass>().soldierLevel)
           .ToDictionary(soldierGroup => soldierGroup.Key, soldierGroup => soldierGroup.Sum(soldier => soldier.GetComponent<SoldierObjectClass>().soldierHealth));
        
        for (int i = 0; i < _tileList.tiles.Count; i++)
        {
            int randomValue = Random.Range(i, _tileList.tiles.Count);
            if (!_tileList.tiles[randomValue].GetComponent<Tile>().isTrigger)
            {
                if(!CheckSoldiersLevel(1, soldierLevelToSoldierHealth, newSoldierLevel,10,2) &&
                    !CheckSoldiersLevel(2, soldierLevelToSoldierHealth, newSoldierLevel,10,5) &&
                    !CheckSoldiersLevel(3, soldierLevelToSoldierHealth, newSoldierLevel,10,10))
                {
                    GameObject GO = Instantiate(spawnGO, _tileList.tiles[randomValue].transform.GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity);
                    GO.transform.position = new Vector3(GO.transform.position.x, GO.transform.position.y, -0.1f);
                    soldiers.Add(GO);
                }
                break;
            }
        }
    }
    public bool CheckSoldiersLevel(int solderLevel, Dictionary<int, int> soldierLevelToSoldierHealth, int newSoldierLevel, int solderHeal, int soldierAttack)
    {

        if (newSoldierLevel == solderLevel && soldierLevelToSoldierHealth.Count != 0 && soldierLevelToSoldierHealth.ContainsKey(solderLevel) && soldierLevelToSoldierHealth[solderLevel] > 0)
        {
            GameObject level1Soldier = soldiers.Find(soldier => soldier.GetComponent<SoldierObjectClass>().soldierLevel == solderLevel);

            level1Soldier.GetComponent<SoldierObjectClass>().soldierHealth += solderHeal;
            level1Soldier.GetComponent<SoldierObjectClass>().soldierAttack += soldierAttack;
            level1Soldier.GetComponent<SoldierObjectClass>().soldierCount++;
            return true;
        }
        return false;
    }

}
