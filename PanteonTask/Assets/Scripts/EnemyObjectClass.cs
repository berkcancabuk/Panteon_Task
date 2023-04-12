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

    /// <summary>
    /// IEnumerator that enables the enemy to attack the soldier every 5 seconds.
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyAttack()
    {
        if (_soldier != null)
        {
            _soldier.GetComponent<SoldierObjectClass>().soldierHealth -= _enemyAttack;
            yield return new WaitForSeconds(5);
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
            if (!_isEnemyFight)
            {
                _soldier = collisionGO[0];
                _isEnemyFight = true;
                StartCoroutine(EnemyAttack());
            }
        }
    }
}
