using UnityEngine;

public class PlayerWeaponController : BaseWeaponController
{
    /// <summary>
    /// Shoot Weapon
    /// Use the weapon in the variable currentWeapon
    /// </summary>
    protected override void ShootWeapon()
    {
        ShootWeapon(currentWeapon);
    }

    /// <summary>
    /// Shoot Weapon
    /// Use the weapon given in the weaponIndex
    /// </summary>
    /// <param name="weaponIndex">weapon to shoot</param>
    protected override void ShootWeapon(int weaponIndex)
    {
        if (weaponIndex < _weapons.Length)
        {
            _weapons[weaponIndex].Shoot();
        }
        else
        {
            Debug.LogError(
                $"Weapon Index given in the shoot is wrong. Total Weapons {_weapons.Length} but you give {weaponIndex}");
        }
    }
}