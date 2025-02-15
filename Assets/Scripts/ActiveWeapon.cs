using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    const string SHOOT_ANIM = "Shoot";

    [SerializeField] private WeaponSO weaponSO;

    private Animator animator;
    private Weapon currentWeapon;
    private StarterAssets.StarterAssetsInputs inputs;
    private float currentWeaponCd = 0f;

    private void Start()
    {
        inputs = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        ShootWeapon();

        if (currentWeaponCd > 0)
        {
            currentWeaponCd -= Time.deltaTime;
        }
    }

    private void ShootWeapon()
    {
        if (!inputs.shoot) return;
        if (currentWeaponCd > 0) return;

        animator.Play(SHOOT_ANIM, 0, 0f);
        currentWeapon.Shoot(weaponSO);
        currentWeaponCd = weaponSO.FireRate;

        if (!weaponSO.IsAutomatic)
        {
            inputs.ShootInput(false);
        }
    }
}
