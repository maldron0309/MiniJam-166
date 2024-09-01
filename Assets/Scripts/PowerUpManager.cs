using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public Text shieldText;
    public Text speedText;
    public Text strenghtText;
    public Text enemiesText;
    private Coroutine shieldTCoroutine = null;
    private Coroutine speedTCoroutine = null;
    private Coroutine strengthTCoroutine = null;
    private Coroutine enemySpeedTCoroutine = null;
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
    private AudioSource source;
    [SerializeField] private AudioClip shieldClip;
    [SerializeField] private AudioClip shieldDownClip;
    [SerializeField] private AudioClip speedUpClip;
    [SerializeField] private AudioClip speedDownClip;
    [SerializeField] private AudioClip strengthClip;
    [SerializeField] private AudioClip strenghDownClip;
    [SerializeField] private AudioClip enemySpeedClip;
    [SerializeField] private AudioClip xpClip;


    void Start(){
        playerMovement = GetComponent<PlayerMovement>();
        projectileScript = GetComponent<ShootingScript>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManagerScript>();
        source = GetComponent<AudioSource>();
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
                    source.PlayOneShot(xpClip);
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
                if(speedText!=null && speedTCoroutine == null){
                    speedText.gameObject.SetActive(true);
                    speedTCoroutine = StartCoroutine(DeactivateText(speedText, "speed"));
                }
            }
            if(speedUpClip!=null){
                source.PlayOneShot(speedUpClip);
            }
            
            speedCoroutine = StartCoroutine(Deactivate(speedTime, "speed"));
        }
    }
    private void DeactivateSpeed(){
        if(playerMovement!= null){
            playerMovement.SetSpeed(-1);
        }
        if(speedDownClip!=null){
            source.PlayOneShot(speedDownClip);
        }
    }

    private void Shield(){
        if(playerMovement != null){
            if(shieldCoroutine != null){
                StopCoroutine(shieldCoroutine);               
            }
            else{
                playerMovement.ToggleShield();  
                if(shieldText!=null && shieldTCoroutine == null){
                    shieldText.gameObject.SetActive(true);
                    shieldTCoroutine = StartCoroutine(DeactivateText(shieldText, "shield"));
                }
            }
            
            if(shieldClip!=null){
                source.PlayOneShot(shieldClip);
            }
            shieldCoroutine = StartCoroutine(Deactivate(shieldTime, "shield"));            
        }
    }
    private void DeactivateShield(){
        if(playerMovement != null){
            playerMovement.ToggleShield();
        }
        if(shieldDownClip!=null){
            source.PlayOneShot(shieldDownClip);
        }
    }

    private void IncreaseStrength(){
        if(projectileScript != null){
            if(strengthCoroutine != null){
                StopCoroutine(strengthCoroutine);                
            }
            else{
                projectileScript.IncreaseStrength(strengthIncrease);
                if(strenghtText!=null && strengthTCoroutine == null){
                strenghtText.gameObject.SetActive(true);
                strengthTCoroutine = StartCoroutine(DeactivateText(strenghtText, "strength"));
                }
            }

            if(strengthClip!=null){
                source.PlayOneShot(strengthClip);
            }

            strengthCoroutine = StartCoroutine(Deactivate(strengthTime, "strength"));            
        }
    }
    private void DeactivateStrength(){
        if(projectileScript!= null){
            projectileScript.IncreaseStrength(-1);
        }
        if(strenghDownClip!=null){
            source.PlayOneShot(strenghDownClip);
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
                if(enemiesText!=null && enemySpeedTCoroutine == null){
                    enemiesText.gameObject.SetActive(true);
                    enemySpeedTCoroutine = StartCoroutine(DeactivateText(enemiesText, "enemies"));
                }
            }

            if(enemySpeedClip!=null){
                source.PlayOneShot(enemySpeedClip);
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

    private IEnumerator DeactivateText(Text text, string whatToCall){
        yield return new WaitForSeconds(2);
        text.gameObject.SetActive(false);
        switch (whatToCall){
            case "speed":
                speedTCoroutine = null;
                break;
            case "shield":
                shieldTCoroutine = null;
                break;
            case "strength":
                strengthTCoroutine = null;
                break;
            case "enemies":
                enemySpeedTCoroutine = null;
                break;
            default:
                Debug.Log("Unknown text: " + text);
                break;
        }
    }
}
