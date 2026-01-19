using TMPro;
using UnityEngine;

public class PointUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_TMPText;

    private void Awake()
    {
        m_TMPText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        UnlockManager.Instance.OnPointChanged += Update_Point;
        Update_Point(UnlockManager.Instance.Get_Point());
    }

    private void OnDestroy()
    {
        if (UnlockManager.Instance != null)
        {
            UnlockManager.Instance.OnPointChanged -= Update_Point;
        }
    }

    public void Update_Point(int iPoint)
    {
        m_TMPText.text = iPoint.ToString();
    }
}
