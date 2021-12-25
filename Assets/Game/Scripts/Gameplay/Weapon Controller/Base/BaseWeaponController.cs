using UnityEngine;

public abstract class BaseWeaponController : MonoBehaviour
{
    [SerializeReference] protected BaseWeapon[] _weapons;

    [SerializeField] protected int currentWeapon = 0;

    /// <summary>
    /// Shoot Weapon
    /// Use the weapon in the variable currentWeapon
    /// </summary>
    protected abstract void ShootWeapon();
    
    /// <summary>
    /// Shoot Weapon
    /// Use the weapon given in the weaponIndex
    /// </summary>
    /// <param name="weaponIndex">weapon to shoot</param>
    protected abstract void ShootWeapon(int weaponIndex);

    /// <summary>
    /// External Shoot call
    /// This function is written because you can customize the shoot behaviour base on controller type.
    /// </summary>
    public virtual void Shoot() => ShootWeapon();
}
