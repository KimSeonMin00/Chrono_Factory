using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering;

//grid맵 타일 정보와 설치된 건물 관리
public class GridDataManager : Singleton<GridDataManager>
{
    //Key : 건물 좌표, Value : 건물 정보(buildingData+건물 오브젝트)
    private Dictionary<Vector3Int, PlacedBuilding> m_placedObjects = new Dictionary<Vector3Int, PlacedBuilding>();
    private HashSet<PlacedBuilding> m_buildingsNotify = new();
    [SerializeField] private Tilemap m_tilemap;

    public float m_fMinX, m_fMinY, m_fMaxX, m_fMaxY;

    protected override void Awake()
    {
        base.Awake();
        m_tilemap = FindFirstObjectByType<Tilemap>();
        Calculate_Bounds();
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
        m_tilemap = FindFirstObjectByType<Tilemap>();
        m_placedObjects.Clear();
    }
    //해당 좌표에 건물이 있는지 체크
    public bool IsOccupied(Vector3Int vecCellPos)
    {
        return m_placedObjects.ContainsKey(vecCellPos);
    }

    public void Calculate_Bounds()
    {      
        m_tilemap.CompressBounds(); 
        Bounds bounds = m_tilemap.localBounds;

        // 3. 맵 끝에서 카메라 크기만큼 안쪽으로 제한 범위 계산
        m_fMinX = bounds.min.x;
        m_fMaxX = bounds.max.x;
        m_fMinY = bounds.min.y;
        m_fMaxY = bounds.max.y;
    }
    //해당 좌표에 있는 건물을 가져온다. 없으면 null 리턴
    public PlacedBuilding Get_PlacedBuilding(Vector3Int vecCellPos)
    {
        m_placedObjects.TryGetValue(vecCellPos, out var building);
        return building;
    }

    public IReadOnlyDictionary<Vector3Int, PlacedBuilding> Get_All_PlacedBuilding()
    {
        return m_placedObjects;
    }
    //자원 타일에 있는 Resource Type을 가져온다.
    public ItemData Get_ResourceOnTile(Vector3Int vecCellPos)
    {
        TileBase tile = m_tilemap.GetTile(vecCellPos);

        if(tile is ResourceTile resTile)
        {
            return resTile.data;
        }

        return null;
    }
    //Dictonary에 값을 추가
    public void Add_Object(Vector3Int vecCellPos, BuildingData data, Building building)
    {

        PlacedBuilding placedBuilding = new PlacedBuilding(data, building);

        for (int i = 0; i < data.m_iWidth; i++)
        {
            for (int j = 0; j < data.m_iHeight; j++)
            {
                m_placedObjects.Add(vecCellPos + new Vector3Int(i, j, 0), placedBuilding);
            }
        }

        building.RecalculateBonus();

        Notify_NearCell(building.Get_NearCellPos());
    }
    //Dictionary에서 건물 제거
    public void Remove_Object(Vector3Int vecCellPos)
    {
        if (IsOccupied(vecCellPos) == true)
        {
            if (m_placedObjects.TryGetValue(vecCellPos, out var building))
            {
                List<Vector3Int> CellList = building.m_building.Get_NearCellPos();
                List<ResourceAmount> m_Costs = building.m_building.m_data.m_totalCost;

                foreach (var cost in m_Costs)
                {
                    ResourceManager.Instance.Add_Resource(cost.m_itemData, cost.m_iAmount);
                }

                

                for(int i = 0; i< building.m_data.m_iWidth; i++)
                {
                    for(int j = 0; j<building.m_data.m_iHeight; j++)
                    {
                        m_placedObjects.Remove(building.m_building.m_vecOriginCellPos + new Vector3Int(i, j, 0));
                    }
                }

                building.m_building.OnDestroyed();
                Notify_NearCell(CellList);//제거 후 주변 타일 건물에 삭제를 Notify
                /*
                 * 기존 : 삭제 시 모든 건물에 Notify
                 * 수정 : 삭제 시 인접한 건물에만 Notify
                 */
            }
        }
    }

    //인접 건물에게 Notify후 인접 보너스 재계산
    public void Notify_NearCell(List<Vector3Int> CellList)
    {
        m_buildingsNotify.Clear();

        foreach(Vector3Int vecCellPos in CellList)
        {
            if (m_placedObjects.TryGetValue(vecCellPos, out PlacedBuilding NearBuilding))
                m_buildingsNotify.Add(NearBuilding);
        }

        foreach(var building in m_buildingsNotify)
        {
            building.m_building.RecalculateBonus();
        }
    }
}
