using UnityEngine;

public enum GameState { Boot, Menu, Loading, Playing, Paused, GameOver}
public class GameManager : Singleton<GameManager>
{
    public GameState m_currentState { get; private set; }

    public float m_fGameoverTime;

    protected override void Awake()
    {
        base.Awake();

        m_fGameoverTime = 15f;
    }

    private void Update()
    {
        if(m_fGameoverTime > 0f)
            m_fGameoverTime -= Time.deltaTime;
    }

    public void Reset_Timer()
    {
        m_fGameoverTime = 15f;
    }
    public void Change_State(GameState newState)
    {
        if (newState == m_currentState) return;

        m_currentState = newState;
        OnStateChanged?.Invoke(newState);
    }

    public event System.Action<GameState> OnStateChanged;
}
