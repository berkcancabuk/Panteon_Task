using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectClass : MonoBehaviour
{
    [SerializeField] public int _enemyHealth;
    [SerializeField] public int _enemyAttack;
    [SerializeField] private GameObject _soldier;
    private bool _isEnemyFight;
    List<GameObject> collisionGO = new List<GameObject>();
    private void Update()
    {
        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
            EnemySpawner.instance.InstantiateSpawnPoint();
        }
    }
    public IEnumerator EnemyAttack()
    {
        if (_soldier != null)
        {
            _soldier.GetComponent<SoldierObjectClass>().soldierHealth -= _enemyAttack;
            if (_enemyHealth <= 0)
            {
                Destroy(gameObject);
                EnemySpawner.instance.InstantiateSpawnPoint();
                yield break;
            }
            yield return new WaitForSeconds(3);
            StartCoroutine(EnemyAttack());
        }
        else
        {
            if (collisionGO.Count != 0)
            {
                collisionGO.RemoveAt(0);
            }
            
            if (collisionGO.Count >= 1)
            {
               
                _soldier = collisionGO[0];
                _isEnemyFight = false;
                StartCoroutine(EnemyAttack());
            }
            else
            {
                _isEnemyFight = false;
                yield break;
            }
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Soldier")
        {
            collisionGO.Add(collision.gameObject);
            print("çarpýþma kontorl " + collision.name);
            if (!_isEnemyFight || collisionGO.Count == 1)
            {
                _soldier = collisionGO[0];
                _isEnemyFight = true;
                StartCoroutine(EnemyAttack());
            }
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke("EnemyAttack");
    }
}
