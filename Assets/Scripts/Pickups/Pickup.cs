using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
        OnPickup(activeWeapon);
        Destroy(gameObject);
    }

    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
