using System;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private GameInput m_GameInput;

    public event Action OnLeftClicked;
    public event Action OnRightClicked;
    public event Action OnInteract;

    public Vector2 m_MoveInput => m_GameInput.Player.Move.ReadValue<Vector2>();
    public Vector2 m_MousePos => m_GameInput.Player.MousePosition.ReadValue<Vector2>();
    protected override void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            m_GameInput = new GameInput();
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        m_GameInput.Enable();

        m_GameInput.Player.LeftClick.performed += _ => OnLeftClicked?.Invoke();
        m_GameInput.Player.RightClick.performed += _ => OnRightClicked?.Invoke();
        m_GameInput.Player.Interact.performed += _ => OnInteract?.Invoke();
    }

    private void OnDisable()
    {
        if (m_GameInput != null)
        {
            m_GameInput.Player.LeftClick.performed -= _ => OnLeftClicked?.Invoke();
            m_GameInput.Player.RightClick.performed -= _ => OnRightClicked?.Invoke();
            m_GameInput.Player.Interact.performed -= _ => OnInteract?.Invoke();

            m_GameInput.Disable();
        }
    }

    
}
