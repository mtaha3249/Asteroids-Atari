using System.Collections;
using UnityEngine;

public class Despawn : Comment
{
    [SerializeField] private bool _despawnOnEnable;
    [SerializeField, Range(0, 10)] private float _despawnTime = 4;

    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_despawnTime);
    }

    private void OnEnable()
    {
        if (_despawnOnEnable)
            DespawnMe();
    }

    /// <summary>
    /// External call for despawn
    /// </summary>
    public void DespawnMe()
    {
        StartCoroutine(DespawnDelay());
    }

    /// <summary>
    /// Despawn item after given delay
    /// </summary>
    /// <returns></returns>
    IEnumerator DespawnDelay()
    {
        yield return _waitForSeconds;
        PoolManager.Instance.Despawn(gameObject, true);
    }

    /// <summary>
    /// Stops all coroutine if object disabled
    /// Otherwise error thrown
    /// </summary>
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}