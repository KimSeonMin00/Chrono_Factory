using TMPro;
using UnityEngine;

public class PointUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_tmpText;

    private void Awake()
    {
        m_tmpText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        UpgradeManager.Instance.OnPointChanged += Update_Point;
        Update_Point(UpgradeManager.Instance.Get_Point());
    }

    private void OnDestroy()
    {
        if (UpgradeManager.Instance != null)
        {
            UpgradeManager.Instance.OnPointChanged -= Update_Point;
        }
    }

    public void Update_Point(int iPoint)
    {
        m_tmpText.text = iPoint.ToString();
    }
}
