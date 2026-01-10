using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlacementController : Singleton<PlacementController>
{
    [Header("References")]
    [SerializeField] private Grid m_Grid;

    [Header("Settings")]
    [SerializeField] private float m_fMaxRayDist = 100f;

    [Header("Prefab to Spawn")]
    [SerializeField] private BuildingData m_BuildingData;

    [Header("Ghost")]
    [SerializeField] private GhostObject m_GhostObject;

    protected override void Awake()
    {
        base.Awake();
        
    }

    private void Update()
    {
        Update_Ghost();
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnLeftClicked += OnLeftClicked;
            InputManager.Instance.OnRightClicked += OnRightClicked;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnLeftClicked -= OnLeftClicked;
            InputManager.Instance.OnRightClicked -= OnRightClicked;
        }
    }

    private void OnLeftClicked()
    {
        if(MouseCursorPointer.Instance.m_bIsGround)
        {
            Spawn_Object(MouseCursorPointer.Instance.m_vecCurrentCell);
        }
    }

    private void OnRightClicked()
    {
        if (MouseCursorPointer.Instance.m_bIsGround)
        {
            Remove_Object(MouseCursorPointer.Instance.m_vecCurrentCell);
        }
    }

    public void Spawn_Object(Vector3Int vecCellPos)
    {
        Vector3 vecWorldPos = Get_WorldPos(vecCellPos);

        if (m_BuildingData != null)
        {
            if (!m_BuildingData.IsEnable_Spawn(vecCellPos))
                return;

            if (m_BuildingData.m_goPrefab != null)
            {
                m_BuildingData.Spawn_Instance(vecWorldPos, vecCellPos);
            }
        }
    }

    public void Remove_Object(Vector3Int vecCellPos)
    {
        GridDataManager.Instance.Remove_Object(vecCellPos);
    }

    public Vector3 Get_WorldPos(Vector3Int vecCellPos)
    {
        Vector3 vecWorldPos = m_Grid.CellToWorld(MouseCursorPointer.Instance.m_vecCurrentCell);
        Vector3 vecCellSize = m_Grid.cellSize;

        vecWorldPos.x += vecCellSize.x * 0.5f;
        vecWorldPos.y = 0.01f;

        return vecWorldPos;
    }

    public void Update_Ghost()
    {
        if (m_BuildingData != null)
        {
            Vector3Int vecCellPos = MouseCursorPointer.Instance.m_vecCurrentCell;

            m_GhostObject.Update_Ghost(Get_WorldPos(vecCellPos), m_BuildingData.IsEnable_Spawn(vecCellPos));
        }
    }

    public void Set_BuildingData(BuildingData data)
    {
        m_BuildingData = data;
        m_GhostObject.Set_Ghost(m_BuildingData.m_goPrefab);
    }
}
