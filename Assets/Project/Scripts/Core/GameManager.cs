using UnityEngine;

public enum GameState { Boot, Menu, Loading, Playing, Paused, GameOver}
public class GameManager : Singleton<GameManager>
{
    public GameState m_currentState { get; private set; }

    public float m_fGameoverTimer;
    public float m_fLimitTime = 60f;

    protected override void Awake()
    {
        base.Awake();

        m_fGameoverTimer = m_fLimitTime;
    }

    private void Update()
    {
        if(m_fGameoverTimer > 0f)
            m_fGameoverTimer -= Time.deltaTime;
    }

    public void Reset_Timer()
    {
        m_fGameoverTimer = m_fLimitTime;
    }
    public void Change_State(GameState newState)
    {
        if (newState == m_currentState) return;

        m_currentState = newState;
        OnStateChanged?.Invoke(newState);
    }

    public event System.Action<GameState> OnStateChanged;
}
