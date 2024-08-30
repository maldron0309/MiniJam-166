using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    void Start(){
        maxHealth = life;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 dir){
        direction = dir;
        speed = Random.Range(0.8f, 3);
    }

    void Update()
    {
        rb.linearVelocity = speed * direction;

        if (transform.position.x < -10f) 
        {
            Die();
        }
    }

    public void GetDamage(int damage){
        life -= damage;
        float healthPercent = (float)life / maxHealth;
        lifebarFilling.localScale = new Vector2(healthPercent, lifebarFilling.localScale.y);
        if(life <= 0){
            Die();
        }
    }

    private void Die(){
        int isDrop = Random.Range(0, 100);
        if (isDrop <= dropPercentage){
            int randomObjectIndex = Random.Range(0, powerups.Count);
            GameObject objectToDrop = powerups[randomObjectIndex];
            Instantiate(objectToDrop, this.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }


}
