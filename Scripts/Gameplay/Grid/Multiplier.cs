using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    public const float DEFAULT_HEIGHT = 0.05f;

    public Vector2Int OriginSlotPos => m_originSlotPos;
    public Vector2Int[] CellsOccupying => m_cellsOccupying;
    public Sprite ShadowSprite => m_shadowSprite;

    [SerializeField] private int m_level;
    [SerializeField] private TextMeshPro m_text;
    [SerializeField] private Vector2Int[] m_cellsOccupying;
    [SerializeField] private Sprite m_shadowSprite;

    private int m_spawnId;
    private Vector2Int m_originSlotPos;
    private Collider[] m_colliders;
    private MeshRenderer[] m_meshRenderers;

    public bool NeverAttachedToSlot { get; private set; } = true;
    public Vector3 InitPos { get; private set; }
    #region Config

    private float m_pressDownAmount = -0.33f;
    private float m_pressDuration = 0.2f;
    private Ease m_pressEase;
    
    #endregion

    public int SpawnID { get { return m_spawnId; } }

    public int Level
    {
        get { return m_level; }
        set
        {
            m_level = value;
            m_text.text = string.Concat("+", m_level);
        }
    }

    private void Start()
    {
        InitPos = transform.position;
    }

    private void OnEnable()
    {
        m_colliders = GetComponentsInChildren<Collider>();
        m_meshRenderers = GetComponentsInChildren<MeshRenderer>();

        SetConfigParams();
    }

    private void SetConfigParams()
    {
        //ConfigParams configInstance = ConfigParams.Instance;
        m_pressEase = Ease.Linear;
        m_pressDownAmount = -0.33f;
        m_pressDuration = 0.1f;
    }

    public void SetOriginSlotPos(Vector2Int posInGrid)
    {
        NeverAttachedToSlot = false;
        m_originSlotPos = posInGrid;
    }

    public void SetCollidersEnabled(bool value)
    {
        for (int i = 0; i < m_colliders.Length; i++)
        {
            m_colliders[i].enabled = value;
        }
    }

    public void SetShadowsEnabled(bool value)
    {
        for (int i = 0; i < m_meshRenderers.Length; i++)
        {
            m_meshRenderers[i].shadowCastingMode =
                value ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }

    private void OnValidate()
    {
        if (m_text != null)
            m_text.text = string.Concat("+", m_level);
    }

    public void SetID(int id)
    {
        m_spawnId = id;
    }

    public void Press()
    {
        Debug.Log("press");
        transform.DOMoveY(m_pressDownAmount, m_pressDuration).SetEase(m_pressEase)
            .OnComplete(() => transform.DOMoveY(DEFAULT_HEIGHT, m_pressDuration));
    }
    
}