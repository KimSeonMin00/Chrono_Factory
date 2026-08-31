using UnityEngine;
using System.Collections.Generic;

public class PoolManager : Singleton<PoolManager>
{
    [Header("Pool")]
    [SerializeField] private List<GameObject> m_ObjectPool;

    [Header("Prefab")]
    [SerializeField] private GameObject m_goPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();

        for(int i=0; i<30; i++)
        {
            m_ObjectPool.Add(Instantiate(m_goPrefab, this.transform));
        }
    }

    public GameObject Create_Pool()
    {
        foreach(GameObject go in m_ObjectPool)
        {
            if (go.activeSelf == false)
            {
                go.SetActive(true);
                return go;
            }
        }

        GameObject newgo = Instantiate(m_goPrefab, this.transform);
        m_ObjectPool.Add(newgo);

        return newgo;
    }
}
