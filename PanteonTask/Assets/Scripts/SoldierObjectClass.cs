using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierObjectClass : MonoBehaviour
{
    public int soldierHealth;
    public int soldierPower;
    public int soldierLevel;
    public int soldierCount;
    [SerializeField] private GameObject _enemy;
    [SerializeField] SoldierSpawnPoint soldierSpawn;
    private GameManager gameManager;
    private bool isFight;
    private void Awake()
    {
        soldierSpawn = SoldierSpawnPoint.instance;
        gameManager = GameManager.instance;
    }
    private void Update()
    {
       
    }
    public IEnumerator SoldierAttack()
    {
            _enemy.GetComponent<EnemyObjectClass>()._enemyHealth -= soldierPower;
            if (soldierHealth <= 0)
            {
                if (gameManager.soldierLevel == 1&&soldierLevel == 1 )
                {
                    soldierSpawn.RepeatingLevel1();
                }
                else if (gameManager.soldierLevel == 2 && soldierLevel == 2)
            {
                    soldierSpawn.RepeatingLevel2();
                }
                else if (gameManager.soldierLevel == 3&& soldierLevel == 3)
            {
                    soldierSpawn.RepeatingLevel3();
                }
                isFight = false;
                Destroy(gameObject);
                yield break;

            }
            yield return new WaitForSeconds(3);
            StartCoroutine(SoldierAttack());
        
        
    }
    
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            
            if (!isFight)
            {
                _enemy = collision.transform.gameObject;
                StartCoroutine(SoldierAttack());
                isFight = true;
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
