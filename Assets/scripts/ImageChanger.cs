using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    public Sprite newSprite;
    public AudioClip Clip;

    private Image img;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        audio = GetComponent<AudioSource>();
        audio.Play();
    }
    public void AudioChangePlay()
    {
        if (audio.isPlaying)
        {
            audio.Pause();
        }
        else audio.Play();
    }
    public void ChangeSound()
    {
        audio.clip = Clip;
        audio.Play();
    }
    public void ChangeSprite()
    {

        img.sprite = newSprite;
    }
    public void ChangeColour()
    {
        img.color = Color.green;
        //img.color = new Color(0.4f, 0.2f, 0.7f, 0.5f);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
