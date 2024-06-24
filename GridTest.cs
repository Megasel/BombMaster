using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private float offset;
    [SerializeField] private float cellScale;
    private void GenerateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                var cell = Instantiate(cellPrefab, transform.position, Quaternion.identity);
                cell.transform.SetParent(transform);
                cell.transform.position = new Vector3((i * offset) + cellScale, 0, (j * offset) + cellScale);
            }
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }
    
}
