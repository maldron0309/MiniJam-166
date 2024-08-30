using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 2f;
   
    private int life = 3;
    [SerializeField] private Transform lifebarFilling;
    private Vector2 direction;
    [SerializeField] private int dropPercentage = 40;
     [SerializeField] private List<GameObject> powerups;
    private Rigidbody2D rb;
    private int maxHealth;
    private ScoreManagerScript scoreManager;

    void Start(){
        maxHealth = life;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 dir){
        direction = dir;
        speed = Random.Range(0.8f + (Time.time /10), 3 + (Time.time /10));
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManagerScript>();
    }

    void Update()
    {
        rb.linearVelocity = speed * direction;
    }

    public void GetDamage(int damage){
        life -= damage;
        float healthPercent = (float)life / maxHealth;
        lifebarFilling.localScale = new Vector2(healthPercent, lifebarFilling.localScale.y);
        bool drop = true;
        if(damage == 1000000){
            drop = false;
        }
        if(life <= 0){
            Die(drop);
        }
    }

    private void Die(bool drop){
        int isDrop = Random.Range(0, 100);
        if (isDrop <= dropPercentage && drop){
            int randomObjectIndex = Random.Range(0, powerups.Count);
            GameObject objectToDrop = powerups[randomObjectIndex];
            Instantiate(objectToDrop, this.transform.position, Quaternion.identity);
            scoreManager.EnemyKilled();
        }
        Destroy(gameObject);
    }

    public void IncreaseSpeed(float s) {
        this.speed *= s;
    }

}
