using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    private Vector2 movement = new Vector2(1, 0);
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 mov, float s){
        movement = mov;
        speed = s;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetDamage(1);
            Destroy(gameObject);
        }
    }
}
