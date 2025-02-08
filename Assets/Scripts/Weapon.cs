using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private ParticleSystem muzzleFlash;

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
        }

        inputs.ShootInput(false);
    }
}
