using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingScript : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
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
            GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
            //From newProjectile we can acces the projectile to edit some of its properties like the velocity or the rotation
        }
    }
}
