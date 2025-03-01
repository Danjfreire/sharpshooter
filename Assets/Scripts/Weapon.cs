using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private LayerMask layerMask;

    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }


    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play();
        RaycastHit hit;
        impulseSource.GenerateImpulse();

        // Check if the raycast has collided with an object, but ignore triggers
        bool hasCollided = Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit,
            Mathf.Infinity,
            layerMask,
            QueryTriggerInteraction.Ignore
        );

        if (hasCollided)
        {
            // Check if the the object can be damaged
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);

            // Show the weapon hit effect
            Instantiate(weaponSO.HitVfx, hit.point, Quaternion.identity);
        }
    }
}
