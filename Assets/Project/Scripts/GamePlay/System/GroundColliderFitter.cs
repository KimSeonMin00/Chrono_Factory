using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundColliderFitter : MonoBehaviour
{
    [SerializeField] private Tilemap m_targetTilemap;
    [SerializeField] private BoxCollider m_targetCollider;

    public void Fit_CollidertoTilemap()
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
