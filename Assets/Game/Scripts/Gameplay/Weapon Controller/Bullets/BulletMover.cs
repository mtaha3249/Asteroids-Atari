using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CheckViewBounds))]
[RequireComponent(typeof(Despawn))]
public class BulletMover : Comment, IGenericCallback
{
    [SerializeField] private float moveSpeed = 10;

    private CheckViewBounds _checkViewBounds;
    private Rigidbody _body;
    private Despawn _despawn;
    private WaitForSeconds _waitForSeconds;

    void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _waitForSeconds = new WaitForSeconds(0.25f);
        _checkViewBounds = GetComponent<CheckViewBounds>();
        _despawn = GetComponent<Despawn>();
    }

    private void OnEnable()
    {
        PoolManager.Instance.OnSpawned += OnSpawned;
        MoveBullet();
        DoCheckBounds();
    }

    private void OnSpawned(GameObject value)
    {
        if (value == gameObject)
            // the spawned bullet and this object is same so do it.
            _body.velocity = Vector3.zero;
    }

    void MoveBullet()
    {
        _body.AddRelativeForce(Vector3.forward * moveSpeed * _body.mass, ForceMode.Impulse);
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
        while (true)
        {
            yield return _waitForSeconds;
            _checkViewBounds.CheckPosition();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void OnEventRaisedCallback(params object[] param)
    {
        GameState _state = (GameState) param[0];
        switch (_state)
        {
            case GameState.LevelFail:
                _despawn.DespawnMe();
                break;
            case GameState.Reset:
                _despawn.DespawnMe();
                break;
        }
    }
}