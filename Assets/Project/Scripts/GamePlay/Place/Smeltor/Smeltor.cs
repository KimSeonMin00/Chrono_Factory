using Mono.Cecil;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Smeltor : Producer
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
