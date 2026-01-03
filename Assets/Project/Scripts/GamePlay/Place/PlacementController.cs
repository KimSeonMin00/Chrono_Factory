using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlacementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Grid m_Grid;
    [SerializeField] private MouseCursorPointer m_MouseCusorPointer;

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
        if(m_MouseCusorPointer.m_bIsGround)
        {
            Spawn_Object(m_MouseCusorPointer.m_vecCurrentCell);
        }
    }

    private void OnRightClicked()
    {
        if (m_MouseCusorPointer.m_bIsGround)
        {
            Remove_Object(m_MouseCusorPointer.m_vecCurrentCell);
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
            if (m_BuildingData.m_goPrefab != null)
            {
                GameObject goSpawn = Instantiate(m_BuildingData.m_goPrefab, vecSpawnWorldPos, Quaternion.identity);               

                Debug.Log("Spawn");

                Building building = goSpawn.GetComponent<Building>();

                building.Init(m_BuildingData, vecCellPos, m_BuildingData.m_BuildingName);

                GridDataManager.Instance.Add_Object(vecCellPos, m_BuildingData, building);

                BillbordSprite billboard = goSpawn.AddComponent<BillbordSprite>();

                if (billboard != null)
                    billboard.Set_Billboard();
            }
        }
    }

    public void Remove_Object(Vector3Int vecCellPos)
    {
        GridDataManager.Instance.Remove_Object(vecCellPos);
    }
}
