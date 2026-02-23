using UnityEngine;

public class Portal : Building
{
    private float m_fTime = 0f;

    private void Update()
    {
        m_fTime += Time.deltaTime;

        if (m_fTime >= 1f)
            SceneLoader.Instance.Load_Scene("Clear", GameState.Clear);
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
