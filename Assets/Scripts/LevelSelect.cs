using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLevel(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
