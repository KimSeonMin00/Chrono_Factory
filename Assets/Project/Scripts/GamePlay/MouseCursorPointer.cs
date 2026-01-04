using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseCursorPointer : Singleton<MouseCursorPointer>
{
    [Header("References")]
    [SerializeField] private Grid m_Grid;
    [SerializeField] private Tilemap m_Tilemap;
    [SerializeField] private LayerMask m_GroundLayermask;

    [Header("Settings")]
    [SerializeField] private float m_fMaxRayDist = 100f;

    public Vector3Int m_vecCurrentCell { get; private set; }
    public bool m_bIsGround { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        m_Grid = FindFirstObjectByType<Grid>();
        m_Tilemap = FindFirstObjectByType<Tilemap>();
        m_GroundLayermask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        Update_MouseCellPos();
    }

    public void Update_MouseCellPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.m_MousePos);

        if (Physics.Raycast(ray, out RaycastHit groundHit, m_fMaxRayDist, m_GroundLayermask))
        {
            Vector3Int CurrentCell = m_Grid.WorldToCell(groundHit.point);
            CurrentCell.z = 0;
            m_vecCurrentCell = CurrentCell;

            m_bIsGround = m_Tilemap.HasTile(m_vecCurrentCell);
        }
        else
            m_bIsGround = false;
    }
}
