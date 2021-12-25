using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CheckViewBounds))]
[RequireComponent(typeof(Despawn))]
public class AsteroidsMover : Comment, IGenericCallback
{
    [SerializeField] private float _moveSpeed = 10;
    private Rigidbody _body;
    private CheckViewBounds _checkViewBounds;
    private WaitForSeconds _waitForSeconds;
    private Despawn _despawn;
    private AsteroidType _myType;
    private AsteroidsSpawner _spawner;
    private bool isInitialized = false, canDivide = false;
    private Vector3 _direction;

    public Vector3 Direction => _direction;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(0.25f);
        _body = GetComponent<Rigidbody>();
        _checkViewBounds = GetComponent<CheckViewBounds>();
        _despawn = GetComponent<Despawn>();
        PoolManager.Instance.OnDespawned += OnDespawned;
    }

    /// <summary>
    /// Called when item is despawned
    /// </summary>
    /// <param name="value">object despaned</param>
    private void OnDespawned(GameObject value)
    {
        // if despawned object is same as mine and I can divide then do logic of division
        if (value == gameObject && canDivide)
        {
            // if I am lowest division return
            if ((int)_myType == 0)
                return;

            // Reduce next division size
            int toSpawnType = ((int) _myType) - 1;
            // force not to reduce less than zero otherwise error thrown
            toSpawnType = toSpawnType <= 0 ? 0 : toSpawnType;
            // spawn the division
            _spawner.SpawnDivided((AsteroidType) toSpawnType, this);
        }
    }

    /// <summary>
    /// Initialize Asteroid
    /// Cache some information and won't run again it already initialized
    /// </summary>
    /// <param name="type"></param>
    /// <param name="spawner"></param>
    public void Init(AsteroidType type, AsteroidsSpawner spawner)
    {
        if (isInitialized)
            return;

        _myType = type;
        _spawner = spawner;

        isInitialized = true;
    }

    /// <summary>
    /// Move Asteroids by given direction
    /// </summary>
    /// <param name="direction">direction to move</param>
    public void MoveAsteroid(Vector3 direction)
    {
        canDivide = true;
        _direction = direction;
        // make downward move faster
        direction.z *= 4;
        // make side movement slower
        direction.x /= 2;
        // apply force
        _body.AddForce(direction * _moveSpeed, ForceMode.Impulse);

        DoCheckBounds();
    }

    /// <summary>
    /// Stop Asteroids Movement
    /// So when it spawn again it won't move
    /// </summary>
    void StopAsteroid()
    {
        _body.velocity = Vector3.zero;
    }

    private void OnDisable()
    {
        StopAsteroid();
    }

    /// <summary>
    /// Check if object is out of bound
    /// </summary>
    void DoCheckBounds()
    {
        // using coroutine to optimise the call of Update
        StartCoroutine(CheckBoundsRoutine());
    }

    /// <summary>
    /// Check Bound Coroutine
    /// if out of bound Event Raises
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckBoundsRoutine()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            yield return _waitForSeconds;
            _checkViewBounds.CheckPosition();
        }
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        GameState _state = (GameState) param[0];
        switch (_state)
        {
            case GameState.LevelFail:
                StopAsteroid();
                break;
            case GameState.Reset:
                canDivide = false;
                _despawn.DespawnMe();
                break;
        }
    }

    private void OnDestroy()
    {
        PoolManager.Instance.OnDespawned -= OnDespawned;
    }
}