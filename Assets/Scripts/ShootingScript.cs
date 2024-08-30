using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private int projectileStrength = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAttack(InputValue value){
        if(firePoint != null && projectile != null){
            GameObject newProjectileObject = Instantiate(projectile, firePoint.position, firePoint.rotation);
            ProjectileScript newProjectile = newProjectileObject.GetComponent<ProjectileScript>();
            newProjectile.Initialize(firePoint.right, projectileSpeed, projectileStrength);
        }
    }

    public void IncreaseStrength(int strength){
        projectileStrength = strength;
        if(strength < 0){
            projectileStrength = 1;
        }
    }
}
