using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour
{
    public int score = 0;
    private int enemyValue = 5;
    private int powerupValue = 20;
    [SerializeField] private Text scoreText;
    private float timeBuffer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        timeBuffer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        timeBuffer += Time.deltaTime;
        if(timeBuffer > 1.0){
            score += 1 * (int) ((Time.time / 10)+1);
            timeBuffer -= 1;
        }

        scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.SetInt("score", score);
    }

    public void EnemyKilled(){
        score += enemyValue * (int) ((Time.time / 10)+1);
    }

    public void PowerUp(){
        score += powerupValue * (int) ((Time.time / 20)+1);
    }
}
