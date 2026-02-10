using UnityEngine;
using UnityEngine.UI;

public class PenaltyBarHandler : MonoBehaviour
{
    [SerializeField] Image m_HeatBar;
    [SerializeField] Image m_PollutionBar;

    void Update()
    {
        m_HeatBar.fillAmount = ResourceManager.Instance.Get_HeatRatio();
        m_PollutionBar.fillAmount = ResourceManager.Instance.Get_PollutionRatio();
    }
}
