using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject canvas;

    public void GoToPlayScene(){
        SceneManager.LoadScene("Ferran Trial");
    }

    public void GoToMainMenu(){

    }

    public void RestartGame(){
        SceneManager.LoadScene("Ferran Trial");
    }

    public void ContinueGame(){
        OnPause();
    }

    private void OnPause(){
        if(canvas.activeInHierarchy){
            canvas.SetActive(false);
            Time.timeScale = 1;
        }else{
            canvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
