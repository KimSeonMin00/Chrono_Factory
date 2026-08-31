using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Update_MouseCellPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.m_MousePos);

        if (Physics.Raycast(ray, out RaycastHit groundHit, m_fMaxRayDist, m_GroundLayermask))
        {
            m_vecCurrentCell = Get_CellPos(groundHit.point);
            m_bIsGround = m_Tilemap.HasTile(m_vecCurrentCell);
        }
        else
            m_bIsGround = false;
    }

    public Vector3Int Get_CellPos(Vector3 vecPos)
    {
        if (m_Grid != null)
        {
            Vector3Int CurrentCell = m_Grid.WorldToCell(vecPos);
            CurrentCell.z = 0;
            return CurrentCell;
        }
        else
            return new Vector3Int(-999, -999, -999);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_Grid = FindFirstObjectByType<Grid>();
        m_Tilemap = FindFirstObjectByType<Tilemap>();
        m_GroundLayermask = LayerMask.GetMask("Ground");
    }
}
