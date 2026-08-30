using UnityEngine;

public class SkipUI : MonoBehaviour
{
    public void Skip()
    {
        Fade.Instance.FadeTo("Result", GameState.GameOver, Color.red);
    }
}
