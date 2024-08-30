using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private int life = 3;
    [SerializeField] private Transform lifebarFilling;
    [SerializeField] private Vector2 direction;
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
            Destroy(gameObject);
        }
    }

    public void GetDamage(int damage){
        life -= damage;
        float healthPercent = (float)life / maxHealth;
        lifebarFilling.localScale = new Vector2(healthPercent, lifebarFilling.localScale.y);
        if(life <= 0){
            Destroy(gameObject);
        }
    }


}
