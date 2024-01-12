using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    List<Node> path = new List<Node>();

    [SerializeField]
    [Range(0f, 10f)]
    float speed = 1f;

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        StartCoroutine(FollowPath());
    }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPosFromCoordinates(pathFinder.StartCoordinates);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPos(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath();
        StartCoroutine(FollowPath());
    }

    void FinishPath()
    {
        gameObject.SetActive(false);
        enemy.StealGold();
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPosFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }
}
