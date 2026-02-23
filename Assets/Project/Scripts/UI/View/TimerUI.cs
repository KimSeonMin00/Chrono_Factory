using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [Header("Component")]
    TextMeshProUGUI m_TimerText;

    public bool m_bIsRunning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_bIsRunning = true;
        m_TimerText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bIsRunning)
        {
            if (GameManager.Instance.m_fGameoverTimer <= 0f)
            {               
                m_bIsRunning = false;
                m_TimerText.text = "GAME OVER";            
                SceneLoader.Instance.Load_Scene("Result", GameState.GameOver);
                return;
            }

            Update_Timer(GameManager.Instance.m_fGameoverTimer);
        }
    }

    public void Reset_Timer()
    {
        m_bIsRunning = true;
    }

    public void Update_Timer(float fTime)
    {
        float fMinutes = Mathf.FloorToInt(fTime) / 60;
        float fSeconds = Mathf.FloorToInt(fTime) % 60;

        m_TimerText.text = string.Format("{0:00} : {1:00}", fMinutes, fSeconds); 
    }
}
