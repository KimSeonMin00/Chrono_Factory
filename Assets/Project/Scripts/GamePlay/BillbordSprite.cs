using UnityEngine;

public class BillbordSprite : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate()
    {
        Set_Billboard();
    }

    public void Set_Billboard()
    {
        if(Camera.main != null)
        {
            Vector3 vecCameraforward = Camera.main.transform.forward;

            vecCameraforward.Normalize();

            transform.forward = vecCameraforward;
        }
    }
}
