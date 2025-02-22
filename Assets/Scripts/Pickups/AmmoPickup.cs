using UnityEngine;

public class AmmoPickup : Pickup
{
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        // always give the player 100 ammo so the weapon always has a full magazine
        activeWeapon.AdjustAmmo(100);
    }
}
