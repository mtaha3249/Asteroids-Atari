using UnityEngine;

public class UMGameplay : MonoBehaviour, IGenericCallback
{
    public Gameplay _gameplay;

    public void OnEventRaisedCallback(params object[] param)
    {
        GameState state = (GameState) param[0];
        if (state == GameState.Gameplay)
        {
            _gameplay.Panel.SetActive(true);
        }
        else
            _gameplay.Panel.SetActive(false);
    }
}