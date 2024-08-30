using UnityEngine;
using UnityEngine.UI;

public class DisplayScoreScript : MonoBehaviour
{

    private Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int score = PlayerPrefs.GetInt("score");
        text = GetComponent<Text>();
        if(text != null){
            text.text = "Score: " + score;
        }
    }
}
