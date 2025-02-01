using UnityEngine;

public class Weapon : MonoBehaviour
{

    private void Update()
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
    }
}
