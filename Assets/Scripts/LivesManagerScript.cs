using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManagerScript : MonoBehaviour
{

    [SerializeField] private int lives = 3;
    [SerializeField] private GameObject one;
    [SerializeField] private GameObject two;
    [SerializeField] private GameObject three;

    private AudioSource source;
    [SerializeField] private AudioClip clip;



    void Start(){
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            lives--;
            other.GetComponent<Enemy>().GetDamage(1000000);
            source.PlayOneShot(clip);
            if(lives < 0){
                SceneManager.LoadScene("GameOverScene");
            }
            if(lives == 2){
                three.SetActive(false);
            }
            if(lives == 1){
                two.SetActive(false);
            }
            if(lives == 0){
                one.SetActive(false);
            }
        }
    }
}
