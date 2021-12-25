using UnityEngine;

public class MachineGun : BaseWeapon
{
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private float fireRate = 0.5f;

    private float _currentFireRate;
    private int spawnPoint;
    private GameObject _spawnedMuzzle, _spawnedBullet;

    private void Start()
    {
        _currentFireRate = 0;
        spawnPoint = 0;
    }

    public override void Shoot()
    {
        _currentFireRate += Time.deltaTime;
        if (_currentFireRate >= fireRate)
        {
            SpawnBullet();
            _currentFireRate = 0;
        }
    }

    protected override void SpawnBullet()
    {
        // Spawn bullet here
        _spawnedMuzzle = PoolManager.Instance.Spawn(_muzzle, _spawnPoints[spawnPoint].position, _spawnPoints[spawnPoint].rotation);
        _spawnedBullet = PoolManager.Instance.Spawn(_bulletPrefab, _spawnPoints[spawnPoint].position, _spawnPoints[spawnPoint].rotation);

        spawnPoint++;
        spawnPoint = spawnPoint % _spawnPoints.Length;
    }
}
