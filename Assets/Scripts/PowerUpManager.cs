using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private float shieldTime = 5f;
    [SerializeField] private float speedIncrease = 10f;
    [SerializeField] private float speedTime = 5f;
    private PlayerMovement playerMovement;
    private Coroutine shieldCoroutine = null;
    private Coroutine speedCoroutine = null;

    void Start(){
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("PowerUp")){
            string powerup = other.name.Split("(")[0];
            switch(powerup) {
                case "ShieldPowerUp":
                    Shield();
                    break;
                case "SpeedPowerUp":
                    IncreaseSpeed();
                    break;
                case "HealthPowerUp":
                    GainHealth();
                    break;
                default:
                    Debug.Log("Unknown powerup: " + powerup);
                    break;
    
            }
            Destroy(other.gameObject);
        }
    }

    private void IncreaseSpeed()
    {
        if(playerMovement != null){
            if(speedCoroutine != null){
                StopCoroutine(speedCoroutine);
            }
            else{
                playerMovement.SetSpeed(speedIncrease);
            }
            speedCoroutine = StartCoroutine(Deactivate(speedTime, "speed"));
        }
    }
    private void DeactivateSpeed(){
        if(playerMovement!= null){
            playerMovement.SetSpeed(-1);
        }
    }

    private void Shield(){
        if(playerMovement != null){
            if(shieldCoroutine != null){
                StopCoroutine(shieldCoroutine);               
            }
            else{
                playerMovement.ToggleShield();   
            }

            shieldCoroutine = StartCoroutine(Deactivate(shieldTime, "shield"));            
        }
    }
    private void DeactivateShield(){
        if(playerMovement != null){
            playerMovement.ToggleShield();
        }
    }

    private void GainHealth(){
        
    }

    private IEnumerator Deactivate(float delay, string whatToCall){
        yield return new WaitForSeconds(delay);
        switch (whatToCall){
            case "shield":
                DeactivateShield();
                shieldCoroutine = null;
                break;
                
            case "speed":
                DeactivateSpeed();
                speedCoroutine = null;
                break;
            default:
                Debug.Log("Unknown powerup deactivation: " + whatToCall);
                break;
        }
    }
}
