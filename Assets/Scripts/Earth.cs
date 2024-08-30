using UnityEngine;

public class Earth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("SO BAD");
        }
    }
}
