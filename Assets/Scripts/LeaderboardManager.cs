using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "c4a4caab18900351b6051568bb5cece8965e6dcb2da88cf310af058cf3132762";

    private void Awake(){
        GetLeaderboard();
    }

    public void GetLeaderboard(){
        Debug.Log("GetLeaderboard");
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            int LoopLenght = (msg.Length<names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < LoopLenght; ++i){
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
                Debug.Log("name");
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score){
        Debug.Log("Setting");
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg)=>{
            GetLeaderboard();
        }));
    }

}