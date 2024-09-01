using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{

    public AudioClip menuMusic;
    public AudioClip gameMusic;
    private static MusicScript instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to handle scene change
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Ferran Trial"){
            PlayMusic(gameMusic);
        }
        else{
            PlayMusic(menuMusic);
        }
    }

    // Play the music associated with the given track index
    private void PlayMusic(AudioClip clip)
    {
        if ( audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
