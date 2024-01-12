using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    int maxHp = 5;

    [Tooltip("Adds amount to maxHp when enemy dies")]
    [SerializeField]
    int difficultyRamp = 1;

    int currHp = 0;

    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        currHp = maxHp;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        if (currHp <= 1)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
            maxHp += difficultyRamp;
        }
        else
        {
            currHp--;
        }
    }
}
