using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GridDataManager : Singleton<GridDataManager>
{
    private Dictionary<Vector3Int, PlacedBuilding> m_placedObjects = new Dictionary<Vector3Int, PlacedBuilding>();
    [SerializeField] private Tilemap m_Tilemap;

    protected override void Awake()
    {
        base.Awake();
        m_Tilemap = FindFirstObjectByType<Tilemap>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_Tilemap = FindFirstObjectByType<Tilemap>();
        m_placedObjects.Clear();
    }
    public bool IsOccupied(Vector3Int vecCellPos)
    {
        return m_placedObjects.ContainsKey(vecCellPos);
    }

    

    public PlacedBuilding Get_PlacedBuilding(Vector3Int vecCellPos)
    {
        m_placedObjects.TryGetValue(vecCellPos, out var building);
        return building;
    }

    public ResourceType Get_ResourceOnTile(Vector3Int vecCellPos)
    {
        TileBase tile = m_Tilemap.GetTile(vecCellPos);

        if(tile is ResourceTile resTile)
        {
            return resTile.type;
        }

        return ResourceType.None;
    }
    public void Add_Object(Vector3Int vecCellPos, BuildingData data, Building PlacedBuilding)
    {
        if (IsOccupied(vecCellPos) == false)
        {
            m_placedObjects.Add(vecCellPos, new PlacedBuilding(data, PlacedBuilding));

            PlacedBuilding.RecalculateBonus();

            Notify_NearCell(PlacedBuilding.Get_NearCellPos());
        }
    }

    public void Remove_Object(Vector3Int vecCellPos)
    {
        if (IsOccupied(vecCellPos) == true)
        {
            if (m_placedObjects.TryGetValue(vecCellPos, out var building))
            {
                List<Vector3Int> CellList = building.m_placedBuilding.Get_NearCellPos();

                building.m_placedBuilding.OnDestroyed();
                m_placedObjects.Remove(vecCellPos);

                Notify_NearCell(CellList);
            }
        }
    }

    public void Notify_NearCell(List<Vector3Int> CellList)
    {
        foreach(Vector3Int vecCellPos in CellList)
        {
            if (m_placedObjects.TryGetValue(vecCellPos, out PlacedBuilding NearBuilding))
                NearBuilding.m_placedBuilding.RecalculateBonus();
        }
    }
}
