using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    [SerializeField] StorageUI m_storageUI;

    public void OpenUI()
    {
        m_storageUI.gameObject.SetActive(true);
    }
}
