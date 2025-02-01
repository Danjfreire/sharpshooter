using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    StarterAssets.StarterAssetsInputs inputs;

    private void Start()
    {
        inputs = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
    }

    private void Update()
    {
        if (inputs.shoot)
        {
            RaycastHit hit;

            bool hasCollided = Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hit,
                Mathf.Infinity
            );

            if (hasCollided)
            {
                Debug.Log(hit.collider.name);
            }

            inputs.ShootInput(false);
        }

    }
}
