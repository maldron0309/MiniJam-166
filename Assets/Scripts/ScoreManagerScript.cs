using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour
{
    public Text xpText;
    private Coroutine coroutine;
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

    public void EnemyKilled(Transform enemy){
        score += enemyValue * (int) ((Time.time / 10)+1);
        xpText.text =  (enemyValue * (int) ((Time.time / 10)+1)).ToString();
        xpText.transform.position = Camera.main.WorldToScreenPoint(enemy.position);
        xpText.gameObject.SetActive(true);
        if(coroutine == null){
            coroutine = StartCoroutine(TextRoutine());
        }
        else{
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(TextRoutine());
        }
    }

    public void PowerUp(Transform power){
        score += powerupValue * (int) ((Time.time / 20)+1);
        xpText.text =  (enemyValue * (int) ((Time.time / 10)+1)).ToString();
        xpText.transform.position = Camera.main.WorldToScreenPoint(power.position);
        xpText.gameObject.SetActive(true);
        if(coroutine == null){
            coroutine = StartCoroutine(TextRoutine());
        }
        else{
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(TextRoutine());
        }
    }

    private IEnumerator TextRoutine(){
        yield return new WaitForSeconds(2);
        xpText.gameObject.SetActive(false);
        coroutine = null;
    }
}
