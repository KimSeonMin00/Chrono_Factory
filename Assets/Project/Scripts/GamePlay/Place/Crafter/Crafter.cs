using UnityEngine;

public class Crafter : Producer
{
    public void Update()
    {
        Update_Produce();
    }
    public override void OnInteract()
    {
        return;
    }

    public override void RecalculateBonus()
    {

    }
}
