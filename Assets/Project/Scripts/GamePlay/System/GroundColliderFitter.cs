using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

//마우스 클릭 감지를 위한 콜라이더
public class GroundColliderFitter : MonoBehaviour
{
    [SerializeField] private Tilemap m_targetTilemap;
    [SerializeField] private BoxCollider m_targetCollider;

    public void Fit_CollidertoTilemap()//grid 맵 크기에 맞춰 콜라이더 크기 변경
    {
        if (m_targetTilemap == null || m_targetCollider == null)
            return;

        m_targetTilemap.CompressBounds();
        Bounds bounds = m_targetTilemap.localBounds;

        m_targetCollider.size = new Vector3(bounds.size.x, 0.1f, bounds.size.y);
        m_targetCollider.center = new Vector3(-bounds.center.x, 0, -bounds.center.y);
    }

    [ContextMenu("Fit Collider")]
    private void ManualFit() => Fit_CollidertoTilemap();
    
}
