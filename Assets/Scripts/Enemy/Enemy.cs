using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private int life = 3;
    [SerializeField] private Transform lifebarFilling;
    private int maxHealth;

    void Start(){
        maxHealth = life;
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

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
