using UnityEngine;

public class UMReset : MonoBehaviour, IGenericCallback
{
    public GenericReference<int> _startLevel;
    public Gameplay _gameplay;

    public void OnEventRaisedCallback(params object[] param)
    {
        GameState state = (GameState) param[0];
        if (state == GameState.Reset)
        {
            _gameplay.levelNumber.text = string.Format("{0}{1}", _gameplay.LevelText, _startLevel.Value);
            _gameplay.Panel.SetActive(true);
        }
    }
}