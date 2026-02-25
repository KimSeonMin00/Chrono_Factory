using UnityEngine;
using UnityEngine.UI;

public class PenaltyBarHandler : MonoBehaviour
{
    [SerializeField] Image m_HeatBar;
    [SerializeField] Image m_PollutionBar;

    float m_fHeatRatio;
    float m_fPollutionRatio;

    bool m_bHeatAlert;
    bool m_bPollutionAlert;

    [SerializeField] Change_Color m_HeatChange;
    [SerializeField] Change_Color m_PollutionChange;
    [SerializeField] Change_Color m_LightChange;

    void Update()
    {
        m_fHeatRatio = ResourceManager.Instance.Get_HeatRatio();
        m_HeatBar.fillAmount = m_fHeatRatio;
        m_fPollutionRatio = ResourceManager.Instance.Get_PollutionRatio();
        m_PollutionBar.fillAmount = m_fPollutionRatio;

        if (m_fHeatRatio >= 0.5f)
            m_bHeatAlert = true;
        else
            m_bHeatAlert = false;

        if(m_fPollutionRatio >= 0.5f)
            m_bPollutionAlert = true;
        else
            m_bPollutionAlert = false;

        m_HeatChange.Activate(m_bHeatAlert);
        m_PollutionChange.Activate(m_bPollutionAlert);

        if(m_LightChange == null)
            m_LightChange = GameObject.FindWithTag("MainLight").GetComponent<Change_Color>();

        m_LightChange.Activate(m_bHeatAlert || m_bPollutionAlert);
    }
}
