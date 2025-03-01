using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private CinemachineVirtualCamera gameOverCam;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Image[] shieldBars;

    private int currentHealth;
    private int gameOverCameraPriority = 20;

    private void Awake()
    {
        currentHealth = maxHealth;
        AdjustShieldBars();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage. Current health: " + currentHealth);
        AdjustShieldBars();

        if (currentHealth <= 0)
        {
            // When the player dies we need to remove the parent of the weapon camera, because it is a child of the player
            // We also need to set the game over camera to a higher priority, so it automatically transitions to the game over camera
            weaponCamera.parent = null;
            gameOverCam.Priority = gameOverCameraPriority;
            Destroy(gameObject);
        }
    }

    private void AdjustShieldBars()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < currentHealth)
            {
                shieldBars[i].enabled = true;
            }
            else
            {
                shieldBars[i].enabled = false;
            }
        }
    }
}
