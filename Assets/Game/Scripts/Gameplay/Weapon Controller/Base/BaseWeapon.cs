using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected Transform[] _spawnPoints;
    [SerializeField] protected GameObject _bulletPrefab;

    /// <summary>
    /// Shoot the weapon
    /// </summary>
    public abstract void Shoot();

    /// <summary>
    /// Spawn Bullet
    /// </summary>
    protected abstract void SpawnBullet();
}