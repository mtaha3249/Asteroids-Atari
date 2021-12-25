using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour, IGenericCallback
{
    [SerializeField] private Transform Camera, Player, SpawnPoint;

    [SerializeField] private GameEvent _gameState;

    /// <summary>
    /// Reset Player Position
    /// </summary>
    public void ResetPlayerPosition() =>
        Player.transform.position = SpawnPoint.transform.position;
    /// <summary>
    /// Reset Player Rotation
    /// </summary>
    public void ResetPlayerRotation() =>
        Player.transform.rotation = SpawnPoint.transform.rotation;

    public void OnEventRaisedCallback(params object[] param)
    {
        GameState _state = (GameState) param[0];
        switch (_state)
        {
            case GameState.Gameplay:
                ResetPlayerPosition();
                ResetPlayerRotation();
                break;
            case GameState.LevelFail:
                StopAllCoroutines();
                break;
            case GameState.Reset:
                StartCoroutine(ResetPlayerRoutine(2));
                break;
        }
    }

    /// <summary>
    /// Reset Player with delay
    /// </summary>
    /// <param name="resetTime">reset time</param>
    /// <returns></returns>
    IEnumerator ResetPlayerRoutine(float resetTime = 2)
    {
        yield return new WaitForSeconds(resetTime);
        ResetPlayerPosition();
        ResetPlayerRotation();
        _gameState.Raise(GameState.Gameplay);
    }
}