using UnityEngine;

public class Portal : Building
{
    private float m_fTime = 0f;

    private Vector3 m_Pos;
    private Vector3 m_PlayerPos;

    CustomizeCamera m_Camera;

    void Start()
    {
        GameManager.Instance.Change_State(GameState.Clear);

        m_PlayerPos = Player.m_vecPlayerPos;

        m_Camera = Camera.main.gameObject.GetComponent<CustomizeCamera>();
    }
    void Update()
    {
        m_fTime += Time.deltaTime;

        if (m_fTime < 3f)
        {
            m_Pos = Vector3.Lerp(m_PlayerPos, transform.position, Mathf.Min(m_fTime, 1f));           
        }
        else
            SceneLoader.Instance.Load_Scene("Clear", GameState.Clear);
    }

    void LateUpdate()
    {
        if (m_Camera != null)
            m_Camera.Set_Pos(m_Pos);
    }
    public override void OnInteract()
    {
        return;
    }

    public override void RecalculateBonus()
    {
        return;
    }
}
