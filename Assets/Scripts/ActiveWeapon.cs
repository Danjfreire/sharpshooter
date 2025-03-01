using System;
using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    const string SHOOT_ANIM = "Shoot";

    [SerializeField] private WeaponSO defaultWeaponSO;
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] private Camera weaponCamera;
    [SerializeField] private GameObject zoomVignette;
    [SerializeField] private TMP_Text ammoCounterText;

    // weapon
    private WeaponSO currentWeaponSO;
    private Weapon currentWeapon;
    private float currentWeaponCd = 0f;
    private int currentAmmo = 0;
    private Animator animator;

    // player
    private float defaultFov;
    private float defaultRotationSpeed;
    private StarterAssets.StarterAssetsInputs inputs;
    private FirstPersonController firstPersonController;

    private void Start()
    {
        inputs = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        currentWeapon = GetComponentInChildren<Weapon>();
        defaultFov = playerFollowCamera.m_Lens.FieldOfView;
        firstPersonController = GetComponentInParent<FirstPersonController>();
        defaultRotationSpeed = firstPersonController.RotationSpeed;

        SwitchWeapon(defaultWeaponSO);
    }

    private void Update()
    {
        ShootWeapon();
        ZoomWeapon();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo = Math.Clamp(currentAmmo + amount, 0, currentWeaponSO.MagazineSize);
        ammoCounterText.text = currentAmmo.ToString("D2");
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
        currentWeaponSO = weaponSO;
        currentWeapon = newWeapon;
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    private void ShootWeapon()
    {
        // Update weapon cd
        if (currentWeaponCd > 0)
        {
            currentWeaponCd -= Time.deltaTime;
        }

        if (currentWeapon == null) return;
        if (!inputs.shoot) return;
        if (currentWeaponCd > 0) return;
        if (currentAmmo == 0) return;

        animator.Play(SHOOT_ANIM, 0, 0f);
        currentWeapon.Shoot(currentWeaponSO);
        currentWeaponCd = currentWeaponSO.FireRate;

        if (!currentWeaponSO.IsAutomatic)
        {
            inputs.ShootInput(false);
        }

        AdjustAmmo(-1);
    }

    private void ZoomWeapon()
    {
        if (!currentWeaponSO.CanZoom) return;

        if (inputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomFov;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomFov;
            zoomVignette.SetActive(true);
            firstPersonController.SetRotationSpeed(currentWeaponSO.ZoomFov / defaultFov);
        }
        else
        {
            playerFollowCamera.m_Lens.FieldOfView = defaultFov;
            weaponCamera.fieldOfView = defaultFov;
            zoomVignette.SetActive(false);
            firstPersonController.SetRotationSpeed(defaultRotationSpeed);
        }

    }
}
