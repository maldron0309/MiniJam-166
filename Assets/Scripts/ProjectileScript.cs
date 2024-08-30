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

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
