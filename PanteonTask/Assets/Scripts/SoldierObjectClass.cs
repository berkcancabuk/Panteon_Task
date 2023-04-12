using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierObjectClass : MonoBehaviour
{
    public int soldierHealth;
    public int soldierAttack;
    public int soldierLevel;
    public int soldierCount;
    [SerializeField] private GameObject _enemy;
    bool _isExitTrigger = false;
    private void Update()
    {
        if (soldierHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// IEnumerator that enables the soldier to attack the enemy every 3 seconds.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SoldierAttack()
    {
        _enemy.GetComponent<EnemyObjectClass>()._enemyHealth -= soldierAttack;
        if (_isExitTrigger)
        {
            yield break;
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(SoldierAttack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _enemy = collision.transform.gameObject;
            StartCoroutine(SoldierAttack());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _isExitTrigger = true;
        }
        
    }
}
