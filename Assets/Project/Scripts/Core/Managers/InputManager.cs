using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{
    private GameInput m_gameInput;

    private bool m_bisPointerOverUI;

    public event Action OnLeftClicked;
    public event Action OnRightClicked;
    public event Action OnInteract;

    public Vector2 m_MoveInput => m_gameInput.Player.Move.ReadValue<Vector2>();
    public Vector2 m_MousePos => m_gameInput.Player.MousePosition.ReadValue<Vector2>();
    protected override void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            m_gameInput = new GameInput();
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        m_bisPointerOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
    public bool IsPointerOverUI()
    {
        return m_bisPointerOverUI;
    }

    private void OnEnable()
    {
        m_gameInput.Enable();

        m_gameInput.Player.LeftClick.performed += _ => OnLeftClicked?.Invoke();
        m_gameInput.Player.RightClick.performed += _ => OnRightClicked?.Invoke();
        m_gameInput.Player.Interact.performed += _ => OnInteract?.Invoke();
    }

    private void OnDisable()
    {
        if (m_gameInput != null)
        {
            m_gameInput.Player.LeftClick.performed -= _ => OnLeftClicked?.Invoke();
            m_gameInput.Player.RightClick.performed -= _ => OnRightClicked?.Invoke();
            m_gameInput.Player.Interact.performed -= _ => OnInteract?.Invoke();

            m_gameInput.Disable();
        }
    }

    
}
