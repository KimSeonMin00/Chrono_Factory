using UnityEngine;
using System.Collections.Generic;

public class PoolManager : Singleton<PoolManager>
{
    [Header("Pool")]
    [SerializeField] private List<GameObject> m_objectPool;

    [Header("Prefab")]
    [SerializeField] private GameObject m_goPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();

        for(int i=0; i<30; i++)
        {
            m_objectPool.Add(Instantiate(m_goPrefab, this.transform));
        }
    }

    public GameObject Create_Pool()
    {
        foreach(GameObject go in m_objectPool)
        {
            if (go.activeSelf == false)
            {
                go.SetActive(true);
                return go;
            }
        }

        GameObject newgo = Instantiate(m_goPrefab, this.transform);
        m_objectPool.Add(newgo);

        return newgo;
    }
}
