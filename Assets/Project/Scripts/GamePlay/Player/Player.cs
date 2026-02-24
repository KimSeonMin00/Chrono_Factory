using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float m_fSpeed;

    [Header("Components")]
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Rigidbody m_Rigid;
    [SerializeField] private Animator m_Anim;
    private Vector3 m_vecMoveDirection;

    public static Vector3 m_vecPlayerPos;
    public static Vector3Int m_vecPlayerCellPos;
    // Update is called once per frame
    private void Update()
    {
        m_vecPlayerPos = new Vector3(transform.position.x, 0f, transform.position.z);
        m_vecPlayerCellPos = MouseCursorPointer.Instance.Get_CellPos(m_vecPlayerPos);
    }
    void FixedUpdate()
    {
        OnMove();
    }

    void LateUpdate()
    {
        float fClampedX = Mathf.Clamp(transform.position.x, GridDataManager.Instance.m_fMinX + 0.5f, GridDataManager.Instance.m_fMaxX - 0.5f);
        float fClampedY = Mathf.Clamp(transform.position.z, GridDataManager.Instance.m_fMinY + 0.5f, GridDataManager.Instance.m_fMaxY - 0.5f);

        transform.position = new Vector3(fClampedX, transform.position.y, fClampedY);
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

        if (m_vecMoveDirection.magnitude > 0f)
            m_Anim.SetBool("Move", true);
        else
            m_Anim.SetBool("Move", false);
    }
}
