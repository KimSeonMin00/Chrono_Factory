using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[System.Serializable]
public struct PlacementInfo
{
    public BuildingData m_BuildingData;
    public RecipeData m_RecipeData;
}
public class PlacementController : Singleton<PlacementController>
{
    [Header("References")]
    [SerializeField] private Grid m_Grid;

    [Header("Settings")]
    [SerializeField] private float m_fMaxRayDist = 100f;

    [Header("Prefab to Spawn")]
    [SerializeField] private PlacementInfo m_PlacementInfo;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnLeftClicked += OnLeftClicked;
            InputManager.Instance.OnRightClicked += OnRightClicked;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnLeftClicked -= OnLeftClicked;
            InputManager.Instance.OnRightClicked -= OnRightClicked;
        }
    }

    private void OnLeftClicked()
    {
        if(MouseCursorPointer.Instance.m_bIsGround && !InputManager.Instance.IsPointerOverUI())
        {
            Spawn_Object(MouseCursorPointer.Instance.m_vecCurrentCell);
        }
    }

    private void OnRightClicked()
    {
        if (MouseCursorPointer.Instance.m_bIsGround && !InputManager.Instance.IsPointerOverUI())
        {
            Remove_Object(MouseCursorPointer.Instance.m_vecCurrentCell);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_Grid = FindFirstObjectByType<Grid>();
    }

    public void Spawn_Object(Vector3Int vecCellPos)
    {
        Vector3 vecWorldPos = Get_WorldPos(vecCellPos);

        if (m_PlacementInfo.m_BuildingData != null)
        {
            if (!m_PlacementInfo.m_BuildingData.IsEnable_Spawn(vecCellPos))
                return;

            if (!Consume_Cost())
            {
                Debug.Log("Not Enough Resource");
                return;
            }

            if (m_PlacementInfo.m_BuildingData.m_goPrefab != null)
            {
                m_PlacementInfo.m_BuildingData.Spawn_Instance(vecWorldPos, vecCellPos, m_PlacementInfo.m_RecipeData);
            }
        }
    }

    public bool Consume_Cost()
    {
        List<ResourceAmount> m_Costs = m_PlacementInfo.m_BuildingData.m_Cost;

        foreach (var cost in m_Costs)
        {
            if (ResourceManager.Instance.Get_ResourceAmount(cost.m_item) < cost.m_iAmount)
                return false;
        }

        foreach (var cost in m_Costs)
            ResourceManager.Instance.Consume_Resource(cost.m_item, cost.m_iAmount);

        return true;
    }

    public void Remove_Object(Vector3Int vecCellPos)
    {
        GridDataManager.Instance.Remove_Object(vecCellPos);
    }

    public Vector3 Get_WorldPos(Vector3Int vecCellPos)
    {
        if (m_Grid != null)
        {
            Vector3 vecWorldPos = m_Grid.CellToWorld(MouseCursorPointer.Instance.m_vecCurrentCell);
            Vector3 vecCellSize = m_Grid.cellSize;

            vecWorldPos.x += vecCellSize.x * 0.5f * (float)m_PlacementInfo.m_BuildingData.m_iWidth;
            vecWorldPos.y = 0.01f;

            return vecWorldPos;
        }
        else
            return new Vector3(-999f, -999f, -999f);
    }

    public void Set_GhostObject(GhostObject ghostgo)
    {
        m_GhostObject = ghostgo;
    }

    public void Update_Ghost()
    {
        if (m_PlacementInfo.m_BuildingData != null)
        {
            Vector3Int vecCellPos = MouseCursorPointer.Instance.m_vecCurrentCell;

            if(m_GhostObject != null)
                m_GhostObject.Update_Ghost(Get_WorldPos(vecCellPos), m_PlacementInfo.m_BuildingData.IsEnable_Spawn(vecCellPos));
        }
    }

    public void Set_BuildingData(PlacementInfo placeInfo)
    {
        m_PlacementInfo = placeInfo;
        m_GhostObject.Set_Ghost(m_PlacementInfo.m_BuildingData);

        SoundManager.Instance.PlaySFX(SoundManager.Instance.m_ClickSound);
    }
}
