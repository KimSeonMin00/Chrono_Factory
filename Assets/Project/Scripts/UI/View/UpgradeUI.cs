using UnityEngine;

public class UpgradeUI : MonoBehaviour
{

    public UpgradeData m_UpgradeData;
    public void OnClick_Button()
    {
        UnlockManager.Instance.Try_Upgrade(m_UpgradeData);
    }
}
