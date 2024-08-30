using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    private Vector2 movement = new Vector2(1, 0);
    private Rigidbody2D rb;
    private int damage = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 mov, float s, int str){
        movement = mov;
        speed = s;
        damage = str;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
