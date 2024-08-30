using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private int life = 3;

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
        if(life <= 0){
            Destroy(gameObject);
        }
    }


}
