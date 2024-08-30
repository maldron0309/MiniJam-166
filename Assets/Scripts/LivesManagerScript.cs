using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManagerScript : MonoBehaviour
{

    [SerializeField] private int lives = 3;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            lives--;
            other.GetComponent<Enemy>().GetDamage(1000000);
            if(lives <= 0){
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }
}
