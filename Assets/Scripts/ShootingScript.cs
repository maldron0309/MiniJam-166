using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;
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
            newProjectile.Initialize(firePoint.right, projectileSpeed);
        }
    }
}
