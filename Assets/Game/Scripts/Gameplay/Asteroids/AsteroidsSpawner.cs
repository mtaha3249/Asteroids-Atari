using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AsteroidType
{
    Small = 0,
    Medium = 1,
    Big = 2
}

[Serializable]
public struct Asteroid
{
    public GameObject _asteroidPrefab;
    public AsteroidType _type;
}

[Serializable]
public struct AsteroidSpawn
{
    public Vector3 _spawnPosition;
    public Vector3 _moveDirection;
}

public class AsteroidsSpawner : Comment, IGenericCallback
{
    [SerializeField, Range(1, 5)] private int _initialAmount = 3;
    [SerializeField] private Asteroid[] _asteroids;

    [Header("Wave Information"), SerializeField]
    private bool canIncreaseAmountAfterWave = true;

    [SerializeField, Range(1, 5)]
    private int waveAmount = 3;

    [SerializeField]
    private float _waveTime = 2;

    private Dictionary<AsteroidType, List<GameObject>> _sorted = new Dictionary<AsteroidType, List<GameObject>>();
    private List<GameObject> _items = new List<GameObject>();
    private AsteroidSpawn _asteroidSpawn;
    private Camera _camera;
    private GameObject _spawnedItem, _prefabToSpawn;
    private float _currentTime = 0;
    private bool canSpawn = false;
    private int maxAmount = 0, waveIndex = 0;

    private void Start()
    {
        _camera = Camera.main;
        maxAmount = _initialAmount;
        // find max number in the enum
        var maxNumber = Enum.GetValues(typeof(AsteroidType)).Cast<AsteroidType>().Last();
        // iterate till max number
        for (int i = 0; i <= (int) maxNumber; i++)
        {
            List<GameObject> _objects = new List<GameObject>();
            // iterate each element in asteroids list
            for (int j = 0; j < _asteroids.Length; j++)
            {
                // if iterated type matched to the type of the asteroids then add
                // for example i == 0 means Small Asteroid, then add to the list
                if (_asteroids[j]._type == (AsteroidType) i)
                {
                    _objects.Add(_asteroids[j]._asteroidPrefab);
                }
            }

            // sorted dictionary contains all prefabs in the list and save w.r.t type
            _sorted.Add((AsteroidType) i, _objects);
        }
    }

    /// <summary>
    /// Spawn Big Asteroids
    /// </summary>
    /// <param name="_spawnAmount">Amount of asteroids to spawn</param>
    void Spawn(int _spawnAmount)
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            _asteroidSpawn = CalculateSpawn();
            // get item of all big stones
            _items = _sorted[AsteroidType.Big];
            // get asteroid to spawn
            _prefabToSpawn = _items[Random.Range(0, _items.Count)];
            // spawn and store
            _spawnedItem = PoolManager.Instance.Spawn(_prefabToSpawn,
                _asteroidSpawn._spawnPosition, _prefabToSpawn.transform.rotation);

            IAsteroidsInit _init = _spawnedItem.GetComponent<IAsteroidsInit>();
            IAsteroidsMover _mover = _spawnedItem.GetComponent<IAsteroidsMover>();
            _init?.Init(AsteroidType.Big, this);
            _mover?.MoveAsteroid(_asteroidSpawn._moveDirection);
        }
    }

    private void Update()
    {
        if (!canSpawn)
            return;

        // Wave counter
        _currentTime += Time.deltaTime;
        if (_currentTime > _waveTime)
        {
            // when new wave timer start spawn random amount
            Spawn(Random.Range(0, maxAmount));
            _currentTime = 0;
            waveIndex++;
            // if wave index reaches than increase spawn amount index
            if (canIncreaseAmountAfterWave && waveIndex >= waveAmount)
            {
                waveIndex = 0;
                maxAmount += 1;
            }
        }
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        GameState _state = (GameState) param[0];
        switch (_state)
        {
            case GameState.Gameplay:
                Spawn(_initialAmount);
                canSpawn = true;
                break;
            case GameState.LevelFail:
                canSpawn = false;
                break;
            case GameState.Reset:
                canSpawn = false;
                break;
        }
    }

    /// <summary>
    /// Calculate Spawn position and direction to apply force
    /// </summary>
    /// <returns>Asteroid Position and force direction</returns>
    AsteroidSpawn CalculateSpawn()
    {
        AsteroidSpawn _spawn = new AsteroidSpawn();
        Vector3 _screenPosition = _camera.WorldToScreenPoint(Vector3.zero);
        // spawn from top and randomize the x position
        _spawn._spawnPosition =
            _camera.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height + 100,
                _screenPosition.z));
        // move downward and randomize the right or left movement
        _spawn._moveDirection = Vector3.back + (Random.Range(0, 2) == 0 ? Vector3.right : Vector3.left);
        return _spawn;
    }

    /// <summary>
    /// Spawn divided asteroids
    /// It will spawn 2 asteroids
    /// </summary>
    /// <param name="_typeToSpawn">Next Type To Spawn</param>
    /// <param name="_spawnPosition">Current Spawn Position</param>
    public void SpawnDivided(AsteroidType _typeToSpawn, AsteroidsMover _spawnPosition)
    {
        List<GameObject> _items = _sorted[_typeToSpawn];
        // get asteroid to spawn
        GameObject _prefabToSpawn = _items[Random.Range(0, _items.Count)];
        // spawn and store
        for (int i = 0; i < 2; i++)
        {
            GameObject _spawnedItem = PoolManager.Instance.Spawn(_prefabToSpawn,
                _spawnPosition.transform.position + (Vector3.right * i * 2), _prefabToSpawn.transform.rotation);

            IAsteroidsInit _init = _spawnedItem.GetComponent<IAsteroidsInit>();
            IAsteroidsMover _mover = _spawnedItem.GetComponent<IAsteroidsMover>();
            _init?.Init(_typeToSpawn, this);
            _mover?.MoveAsteroid(_spawnPosition.Direction);
        }
    }
}