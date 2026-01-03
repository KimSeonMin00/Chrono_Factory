using UnityEngine;
using UnityEngine.InputSystem;

public class StateTester : MonoBehaviour
{
    private GameInput m_input;

    private void Awake()
    {
        m_input = new GameInput();
    }

    private void OnEnable()
    {
        m_input.Player.Pause.performed += OnPause;
        m_input.Player.Enable();
    }

    private void OnDisable()
    {
        m_input.Player.Pause.performed -= OnPause;
        m_input.Player.Disable();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if(GameManager.Instance.m_currentState == GameState.Paused)
        {
            GameManager.Instance.Change_State(GameState.Playing);
        }
        else 
        {
            GameManager.Instance.Change_State(GameState.Paused);
        }
    }
}
