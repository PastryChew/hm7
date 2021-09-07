using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool paused;
    public GameObject PauseScreen;
    public AudioSource clicksound;
    public AudioSource music;

    private void Start()
    {
        clicksound.GetComponent<AudioSource>();
        music.GetComponent<AudioSource>();
    }
    public void PauseGame()
    {
        clicksound.Play();
        if (paused)
        {
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
            music.Play();
        }
        else
        {
            Time.timeScale = 0;
            PauseScreen.SetActive(true);
            music.Pause();
        }

        paused = !paused;
    }

}
