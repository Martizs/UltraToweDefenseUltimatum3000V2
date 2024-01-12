using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    int cost = 50;

    [SerializeField]
    [Range(0.1f, 5f)]
    float buildSpeed = 1f;

    private void Start()
    {
        StartCoroutine(Build());
    }

    public bool HaveMoneyForTower()
    {
        Bank bank = FindObjectOfType<Bank>();
        return bank != null && bank.CurrentBalance >= cost;
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank != null && bank.CurrentBalance >= cost)
        {
            GameObject newTower = Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }

        return false;
    }

    IEnumerator Build()
    {
        foreach (Transform towerpiece in transform)
        {
            towerpiece.gameObject.SetActive(false);
        }

        foreach (Transform towerpiece in transform)
        {
            towerpiece.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildSpeed);
        }
    }
}
