using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GridDataManager : Singleton<GridDataManager>
{
    private Dictionary<Vector3Int, PlacedBuilding> m_placedObjects = new Dictionary<Vector3Int, PlacedBuilding>();
    [SerializeField] private Tilemap m_Tilemap;
    public event Action<Vector3Int> OnTileChanged;

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

    public ItemData Get_ResourceOnTile(Vector3Int vecCellPos)
    {
        TileBase tile = m_Tilemap.GetTile(vecCellPos);

        if(tile is ResourceTile resTile)
        {
            return resTile.data;
        }

        return null;
    }
    public void Add_Object(Vector3Int vecCellPos, BuildingData data, Building PlacedBuilding)
    {
        if (IsOccupied(vecCellPos) == false)
        {
            m_placedObjects.Add(vecCellPos, new PlacedBuilding(data, PlacedBuilding));

            PlacedBuilding.RecalculateBonus();

            OnTileChanged?.Invoke(vecCellPos);

            //Notify_NearCell(PlacedBuilding.Get_NearCellPos());
        }
    }

    public void Remove_Object(Vector3Int vecCellPos)
    {
        if (IsOccupied(vecCellPos) == true)
        {
            if (m_placedObjects.TryGetValue(vecCellPos, out var building))
            {
                List<Vector3Int> CellList = building.m_placedBuilding.Get_NearCellPos();
                OnTileChanged?.Invoke(vecCellPos);
                //Notify_NearCell(CellList);

                building.m_placedBuilding.OnDestroyed();
                m_placedObjects.Remove(vecCellPos);              
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
