using UnityEngine;

public class Moving : MonoBehaviour
{
    public float m_fSpeed;
    public Vector3 m_fDirection;

    private bool m_bPaused = false;

    private void OnEnable()
    {
        GameManager.Instance.OnStateChanged += StateChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStateChanged -= StateChanged;
    }

    private void StateChanged(GameState state)
    {
        m_bPaused = (state == GameState.Paused);
    }

    private void Awake()
    {
        m_fDirection = m_fDirection.normalized;
    }
    private void Update()
    {
        if(!m_bPaused)
            transform.Translate(m_fDirection * m_fSpeed * Time.deltaTime);
    }
}
