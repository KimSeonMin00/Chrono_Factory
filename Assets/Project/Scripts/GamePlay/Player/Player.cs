using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float m_fSpeed;

    [Header("Components")]
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Rigidbody m_Rigid;
    private Vector3 m_vecMoveDirection;

    // Update is called once per frame
    void FixedUpdate()
    {
        OnMove();
    }

    private void OnMove()
    {
        Vector2 vecInput = InputManager.Instance.m_MoveInput;

        if (vecInput.x > 0)
            m_SpriteRenderer.flipX = false;
        else if (vecInput.x < 0)
            m_SpriteRenderer.flipX = true;

        m_vecMoveDirection = new Vector3(vecInput.x, 0f, vecInput.y).normalized;
        m_Rigid.linearVelocity = m_vecMoveDirection * m_fSpeed;
    }
}
