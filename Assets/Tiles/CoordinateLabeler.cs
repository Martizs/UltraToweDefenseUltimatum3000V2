using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Will execute in editor & whle running the game
[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField]
    Color defaultColor = Color.white;

    [SerializeField]
    Color blockedColor = Color.gray;

    [SerializeField]
    Color exploredColor = Color.yellow;

    [SerializeField]
    Color pathColor = Color.blue;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    GridManager gridManager;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = !Application.isPlaying;

        gridManager = FindObjectOfType<GridManager>();

        DisplayCoordinates();
        UpdateObjectName();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            // Should execute in EDITOR only and we use this if, because we've set the script to run with ExecuteAlways
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelColor();
        DebugMethods();
    }

    void DebugMethods()
    {
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
    {
        if (gridManager != null)
        {
            Node node = gridManager.GetNode(coordinates);

            if (node != null)
            {
                if (!node.isWalkable)
                {
                    label.color = blockedColor;
                }
                else if (node.isPath)
                {
                    label.color = pathColor;
                }
                else if (node.isExplored)
                {
                    label.color = exploredColor;
                }
                else
                {
                    label.color = defaultColor;
                }
            }
        }
    }

    void DisplayCoordinates()
    {
        if (gridManager != null)
        {
            coordinates.x = Mathf.RoundToInt(
                transform.parent.position.x / gridManager.UnityGridSize
            );
            coordinates.y = Mathf.RoundToInt(
                transform.parent.position.z / gridManager.UnityGridSize
            );
            label.text = coordinates.x + "," + coordinates.y;
        }
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
