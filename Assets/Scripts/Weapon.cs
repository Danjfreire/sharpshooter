using StarterAssets;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private ParticleSystem weaponHitFx;
    [SerializeField] private ParticleSystem muzzleFlash;

    const string SHOOT_ANIM = "Shoot";

    StarterAssets.StarterAssetsInputs inputs;

    private void Start()
    {
        inputs = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
    }

    private void Update()
    {
        ShootWeapon();
    }

    private void ShootWeapon()
    {
        if (!inputs.shoot) return;

        muzzleFlash.Play();
        animator.Play(SHOOT_ANIM, 0, 0f);
        RaycastHit hit;

        bool hasCollided = Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit,
            Mathf.Infinity
        );

        if (hasCollided)
        {
            // Check if the the object can be damaged
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(damageAmount);

            // Show the weapon hit effect
            Instantiate(weaponHitFx, hit.point, quaternion.identity);
        }

        inputs.ShootInput(false);
    }
}
