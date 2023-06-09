using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string SceneToLoad;
    
    [SerializeField]
    private AudioSource SoundEffect;

    void start()
    {
        SoundEffect.Play();
    }

    public void LoadGame()
    {
        SoundEffect.Stop();
        SceneManager.LoadScene(SceneToLoad);
    }
}
