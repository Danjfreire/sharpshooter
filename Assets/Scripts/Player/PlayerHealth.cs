using Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private CinemachineVirtualCamera gameOverCam;
    [SerializeField] Transform weaponCamera;

    private int currentHealth;
    private int gameOverCameraPriority = 20;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Took " + damage + " damage!");

        if (currentHealth <= 0)
        {
            // When the player dies we need to remove the parent of the weapon camera, because it is a child of the player
            // We also need to set the game over camera to a higher priority, so it automatically transitions to the game over camera
            weaponCamera.parent = null;
            gameOverCam.Priority = gameOverCameraPriority;
            Destroy(gameObject);
        }
    }
}
