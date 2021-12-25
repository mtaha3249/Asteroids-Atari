using UnityEngine;

public class UMReset : MonoBehaviour, IGenericCallback
{
    public Gameplay _gameplay;

    public void OnEventRaisedCallback(params object[] param)
    {
        GameState state = (GameState) param[0];
        if (state == GameState.Reset)
        {
            _gameplay.Panel.SetActive(true);
        }
    }
}