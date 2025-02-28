using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObjects/WeaponSO", order = 1)]
public class WeaponSO : ScriptableObject
{
    public GameObject WeaponPrefab;
    public int Damage = 1;
    public float FireRate = 0.1f;
    public ParticleSystem HitVfx;
    public bool IsAutomatic = false;
    public bool CanZoom = false;
    public float ZoomFov = 10f;
    public int MagazineSize = 10;
}
