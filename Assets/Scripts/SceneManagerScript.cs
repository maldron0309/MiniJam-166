using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void GoToPlayScene(){
        SceneManager.LoadScene("Ferran Trial");
    }
}
