using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MouseCursorPointer m_MouseCusorPointer;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteract += Interact_Building;
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteract -= Interact_Building;
    }
    public void Interact_Building()
    {
        GridDataManager.Instance.Get_PlacedBuilding(m_MouseCusorPointer.m_vecCurrentCell).m_building.OnInteract();
    }
}
