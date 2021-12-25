using UnityEngine;

public class UpdateGameStateVariable : MonoBehaviour, IGenericCallback
{
    public GenericReference<GameState> _gameState;
    public void OnEventRaisedCallback(params object[] param)
    {
        _gameState.Value = (GameState) param[0];
    }
}
