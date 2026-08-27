using System.Collections;
using UnityEngine;

public class Extractor : Building
{
    public static int currentPlayingCount = 0;
    private const int MAX_BUILDING_SOUNDS = 3;
    [SerializeField] private ItemData m_itemdata = null;

    [Header("Upgrade Data")]
    [SerializeField] private UpgradeData m_ExtratorAdj = null;

    [Header("Building Setting")]
    public float m_fProduceCooldown = 3f;
    public float m_fTime = 0f;

    public int m_iCount = 0;
    void Update()
    {
        m_fTime += Time.deltaTime;

        ResourceManager.Instance.Add_Heat(m_Data.m_fHeatPerSecond * Time.deltaTime);
        ResourceManager.Instance.Add_Pollution(m_Data.m_fPollutionPerSecond * Time.deltaTime);

        if (currentPlayingCount < MAX_BUILDING_SOUNDS)
        {
            StartCoroutine(PlaySound());
            currentPlayingCount++;
            Invoke("OnSoundFinished", SoundManager.Instance.m_MachineSound.length);
        }

        if (m_fTime >= m_fProduceCooldown)
        {
            ResourceManager.Instance.Add_Resource(m_itemdata, 1+m_iCount);
            //if(m_ExtratorAdj.m_bActivate)
            //    ResourceManager.Instance.Add_Resource(m_itemdata, m_iCount);

            ResourceManager.Instance.Produce_Effect(m_itemdata, transform.position, 1+m_iCount);

            m_fTime = 0;
        }
    }
    void OnSoundFinished()
    {
        if(currentPlayingCount > 0)
            currentPlayingCount--;
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));

        SoundManager.Instance.PlaySFX(SoundManager.Instance.m_MachineSound, 0.1f);
    }
    public override void OnInteract()
    {
        return;
    }

    public void SetUp_Resource(ItemData data)
    {
         m_itemdata = data;
    }

    public override void RecalculateBonus()
    {
        foreach(UpgradeData upgrade in m_Data.m_upgradeList)
        {
            if (upgrade.m_bActivate)
                UpgradeManager.Instance.Upgrade_Apply(upgrade.Get_EffectType(), this);
        }
    }

    public void Set_Bonus(int iCount)
    {
        m_iCount = iCount;
    }

    public void OnDestroy()
    {
        currentPlayingCount--;
    }
}
