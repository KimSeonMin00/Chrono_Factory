using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlacementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Grid m_Grid;

    [Header("Settings")]
    [SerializeField] private float m_fMaxRayDist = 100f;

    [Header("Prefab to Spawn")]
    [SerializeField] private BuildingData m_BuildingData;

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
        Vector3 vecSpawnWorldPos = m_Grid.CellToWorld(vecCellPos);
        Vector3 vecCellSize = m_Grid.cellSize;

        if (GridDataManager.Instance.IsOccupied(vecCellPos))
            return;

        vecSpawnWorldPos.x += vecCellSize.x * 0.5f;
        vecSpawnWorldPos.y = 0.01f;

        if (m_BuildingData != null)
        {
            if (!m_BuildingData.IsEnable_Spawn(vecCellPos))
                return;

            if (m_BuildingData.m_goPrefab != null)
            {
                m_BuildingData.Spawn_Instance(vecSpawnWorldPos, vecCellPos);
            }
        }
    }

    public void Remove_Object(Vector3Int vecCellPos)
    {
        GridDataManager.Instance.Remove_Object(vecCellPos);
    }
}
