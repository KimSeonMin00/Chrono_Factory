using UnityEngine;

public class TestUI : MonoBehaviour
{
    public GameObject m_goPrefab;
    public void Create_Buldings()
    {
        for(int i=0; i<300; i++)
        {
            Instantiate(m_goPrefab);
        }
    }
}
