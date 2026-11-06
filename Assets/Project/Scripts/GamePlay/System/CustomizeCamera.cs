using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomizeCamera : MonoBehaviour
{
    [Header("Tilemap Ref")]
    [SerializeField] private Tilemap m_TargetTilemap;

    Camera m_Cam;
    [SerializeField] private Vector3 m_vecPositionOffset;
    private float m_fMinX, m_fMinY, m_fMaxX, m_fMaxY;

    void Start()
    {
        m_Cam = Camera.main;
        Calculate_Bounds();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(GameManager.Instance.m_currentState == GameState.Playing)
            transform.position = Get_ClampPos(Player.m_vecPlayerPos) + m_vecPositionOffset;
    }

    public void Set_Pos(Vector3 vecPos)
    {
        transform.position = Get_ClampPos(vecPos) + m_vecPositionOffset; 
    }
    public void Calculate_Bounds()
    {
        // 1. 타일들이 실제로 깔린 영역의 경계값(Bounds)을 가져옵니다.
        m_TargetTilemap.CompressBounds(); // 빈 공간을 제외하고 실제 타일이 있는 곳으로 크기 압축
        Bounds bounds = m_TargetTilemap.localBounds;

        // 2. 카메라의 화면 크기(절반) 계산
        float camHeight = m_Cam.orthographicSize;
        float camWidth = camHeight * m_Cam.aspect;

        // 3. 맵 끝에서 카메라 크기만큼 안쪽으로 제한 범위 계산
        m_fMinX = bounds.min.x + camWidth;
        m_fMaxX = bounds.max.x - camWidth;
        m_fMinY = bounds.min.y + camHeight;
        m_fMaxY = bounds.max.y - camHeight;
    }

    public Vector3 Get_ClampPos(Vector3 vecPos)
    {
        float fClampedX = Mathf.Clamp(vecPos.x, m_fMinX, m_fMaxX);
        float fClampedY = Mathf.Clamp(vecPos.z, m_fMinY, m_fMaxY);

        return new Vector3(fClampedX, vecPos.y, fClampedY);
    }
}
