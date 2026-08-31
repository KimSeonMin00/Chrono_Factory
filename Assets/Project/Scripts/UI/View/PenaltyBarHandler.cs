using UnityEngine;
using UnityEngine.UI;

public class PenaltyBarHandler : MonoBehaviour
{
    [SerializeField] Image m_heatBar;
    [SerializeField] Image m_pollutionBar;

    float m_fHeatRatio;
    float m_fPollutionRatio;

    bool m_bHeatAlert;
    bool m_bPollutionAlert;

    [SerializeField] Change_Color m_heatChange;
    [SerializeField] Change_Color m_pollutionChange;
    [SerializeField] Change_Color m_lightChange;

    void Update()
    {
        m_fHeatRatio = ResourceManager.Instance.Get_HeatRatio();
        m_heatBar.fillAmount = m_fHeatRatio;
        m_fPollutionRatio = ResourceManager.Instance.Get_PollutionRatio();
        m_pollutionBar.fillAmount = m_fPollutionRatio;

        if (m_fHeatRatio >= 0.5f)
            m_bHeatAlert = true;
        else
            m_bHeatAlert = false;

        if(m_fPollutionRatio >= 0.5f)
            m_bPollutionAlert = true;
        else
            m_bPollutionAlert = false;

        m_heatChange.Activate(m_bHeatAlert);
        m_pollutionChange.Activate(m_bPollutionAlert);

        if(m_lightChange == null)
            m_lightChange = GameObject.FindWithTag("MainLight").GetComponent<Change_Color>();

        if (m_bHeatAlert || m_bPollutionAlert)
        {
            m_lightChange.Activate(true);
            SoundManager.Instance.PlayAlert();
        }
        else
            m_lightChange.Activate(false);
    }
}
