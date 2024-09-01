using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreLeaderboardManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;

    void Awake(){
        inputScore.text = PlayerPrefs.GetInt("score").ToString();
    }

    public void SubmitScore(){
        submitScoreEvent.Invoke(inputName.text, PlayerPrefs.GetInt("score"));
    }
}
