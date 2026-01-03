using UnityEngine;

public enum GameState { Boot, Menu, Loading, Playing, Paused, GameOver}
public class GameManager : Singleton<GameManager>
{
    public GameState m_currentState { get; private set; }

    public void Change_State(GameState newState)
    {
        if (newState == m_currentState) return;

        m_currentState = newState;
        OnStateChanged?.Invoke(newState);
    }

    public event System.Action<GameState> OnStateChanged;
}
