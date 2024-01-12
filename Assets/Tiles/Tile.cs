using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    Tower towerPrefab;

    [SerializeField]
    bool isPlaceable;
    public bool IsPlaceable
    {
        get { return isPlaceable; }
    }

    GridManager gridManager;

    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void OnEnable()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPos(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown PLACE TURRET");
        Debug.Log(
            $"gridManager.GetNode(coordinates).isWalkable - {gridManager.GetNode(coordinates).isWalkable}"
        );

        Debug.Log($"gridManager.Grid[coordinates].isPath - {gridManager.Grid[coordinates].isPath}");

        if (
            gridManager.GetNode(coordinates).isWalkable
            && towerPrefab.HaveMoneyForTower()
            && !pathFinder.WillBlockPath(coordinates)
        )
        {
            towerPrefab.CreateTower(towerPrefab, transform.position);
            gridManager.BlockNode(coordinates);
            pathFinder.NotifyReceivers();
        }
    }

    // public bool getIsPlaceable()
    // {
    //     return isPlaceable;
    // }
}

// class Testing
// {
//     private int lelz = 0;
//     int lolz = 1;

//     public int lolzers = 2;

//     protected int protecto = 3;

//     internal int internoLol = 4;
// }

// class Tester : Testing
// {
//     void TestMethod()
//     {
//         int a = lolzers;
//         int b = protecto;
//         int c = internoLol;
//     }
// }

// class Bruv
// {
//     void TestBruv()
//     {
//         Testing b = new Testing();
//         int z = b.lolzers;
//         int x = b.internoLol;
//     }
// }
