using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private ScoreManagerScript scoreManager;
    [SerializeField] private GameObject enemies;
    [SerializeField] private float shieldTime = 5f;
    [SerializeField] private float speedIncrease = 10f;
    [SerializeField] private float speedTime = 5f;
    [SerializeField] private int strengthIncrease = 3;
    [SerializeField] private float  strengthTime = 5f;
    [SerializeField] private float enemySpeedIncrease = 1.5f;
    [SerializeField] private float enemySpeedTime = 5f;
    private PlayerMovement playerMovement;
    private ShootingScript projectileScript;
    private Coroutine shieldCoroutine = null;
    private Coroutine speedCoroutine = null;
    private Coroutine strengthCoroutine = null;
    private Coroutine enemySpeedCoroutine = null;

    void Start(){
        playerMovement = GetComponent<PlayerMovement>();
        projectileScript = GetComponent<ShootingScript>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManagerScript>();
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
                case "StrengthPowerUp":
                    IncreaseStrength();
                    break;
                case "EnemiesSpeed":
                    EnemySpeed();
                    break;
                case "ScorePowerUp":
                    scoreManager.PowerUp();
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

    private void IncreaseStrength(){
        if(projectileScript != null){
            if(strengthCoroutine != null){
                StopCoroutine(strengthCoroutine);                
            }
            else{
                   projectileScript.IncreaseStrength(strengthIncrease);
            }

            strengthCoroutine = StartCoroutine(Deactivate(strengthTime, "strength"));            
        }
    }
    private void DeactivateStrength(){
        if(projectileScript!= null){
            projectileScript.IncreaseStrength(-1);
        }
    }

    private void EnemySpeed(){
        if(enemies!= null){
            if(enemySpeedCoroutine!= null){
                StopCoroutine(enemySpeedCoroutine);                
            }
            else{
                foreach(Transform enemy in enemies.transform){
                    enemy.GetComponent<Enemy>().IncreaseSpeed(enemySpeedIncrease);
                }
            }
            enemySpeedCoroutine = StartCoroutine(Deactivate(enemySpeedTime, "enemiesSpeed"));
        }
    }
    private void DeactivateEnemySpeed(){
        if(enemies!= null){
            foreach(Transform enemy in enemies.transform){
                enemy.GetComponent<Enemy>().IncreaseSpeed(1/enemySpeedIncrease);
            }
        }
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
            case "strength":
                DeactivateStrength();
                strengthCoroutine = null;
                break;
            case "enemiesSpeed":
                DeactivateEnemySpeed();
                enemySpeedCoroutine = null;
                break;
            default:
                Debug.Log("Unknown powerup deactivation: " + whatToCall);
                break;
        }
    }
}
