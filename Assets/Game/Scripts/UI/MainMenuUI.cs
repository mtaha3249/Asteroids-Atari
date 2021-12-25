using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameEvent _gameStateEvent;

    private void Awake()
    {
        _gameStateEvent.Raise(GameState.MainMenu);
    }

    public void OnTapToPlay()
    {
        _gameStateEvent.Raise(GameState.Gameplay);
    }
}