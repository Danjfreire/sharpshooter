using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
        activeWeapon.SwitchWeapon(weaponSO);
        Destroy(this.gameObject);
    }
}
