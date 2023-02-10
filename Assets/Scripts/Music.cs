using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    AudioSource music;
    public Slider vol;

    void Start()
    {
        //setup music
        music = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateVolume()
    {
        SetVolume((float)vol.value);
    }

    public void SetVolume(float vol)
    {
        music.volume = vol;
    }
}
