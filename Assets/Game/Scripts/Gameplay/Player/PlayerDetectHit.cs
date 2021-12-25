using UnityEngine;

public class PlayerDetectHit : TriggerEvents
{
    [SerializeField] private GenericReference<int> _maxLives;
    [SerializeField] private GameEvent _onPlayerGetHit;
    [SerializeField] private GameEvent _gameState;
    private int _currentLives = 0;

    protected override void Start()
    {
        base.Start();
        _currentLives = _maxLives.Value;
    }

    public override void TriggerEnter(GameObject triggeredObject)
    {
        // reduce live cound
        _currentLives--;
        // raise event I am hit
        _onPlayerGetHit.Raise(_currentLives);
        // reset the game state if it is not last life
        if (_currentLives > 0)
            _gameState.Raise(GameState.Reset);
    }

    public override void TriggerExit(GameObject triggeredObject)
    {
    }
}