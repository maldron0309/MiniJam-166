using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{
    public GameObject pause;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private int projectileStrength = 1;
    private AudioSource source;
    [SerializeField] private AudioClip shootClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnAttack(InputValue value){
        if(firePoint != null && projectile != null && !pause.activeInHierarchy){
            GameObject newProjectileObject = Instantiate(projectile, firePoint.position, firePoint.rotation);
            ProjectileScript newProjectile = newProjectileObject.GetComponent<ProjectileScript>();
            newProjectile.Initialize(firePoint.right, projectileSpeed, projectileStrength);
            source.PlayOneShot(shootClip);
        }
    }

    public void IncreaseStrength(int strength){
        projectileStrength = strength;
        if(strength < 0){
            projectileStrength = 1;
        }
    }
}
