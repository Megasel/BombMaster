using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierSlot : MonoBehaviour
{
    private Multiplier m_multiplierBox;
    public Multiplier MultiplierBox => m_multiplierBox;


    [SerializeField] private Vector2Int m_posInGrid;
    [SerializeField] private bool spawnPoint;

    public bool isSpawnPoint
    {
        get => spawnPoint;
    }
    public Vector2Int PositionInGrid => m_posInGrid;

    public bool IsEmpty()
    {
        return m_multiplierBox == null;
    }

    public void SetPositionInGrid(int x, int y)
    {
        m_posInGrid.x = x;
        m_posInGrid.y = y;
    }

    public void SetMultiplier(Multiplier value)
    {
        m_multiplierBox = value;
    }
}
