using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeUI : MonoBehaviour
{
    public GameObject camera;

    public GameObject BlockButtons;

    public GameObject PlayReset;
    public GameObject Pause;

    void Update()
    {
        //activate correct elements depending on mode
        if (Cam.PlayMode)
        {
            BlockButtons.active = false;
            PlayReset.active = false;
            Pause.active = true;
        }
        else
        {
            BlockButtons.active = true;
            PlayReset.active = true;
            Pause.active = false;
        }
    }

    public void ResetButton()
    {
        Cam.ResetBlocks();
    }

    public void Play()
    {
        if (!Cam.PlayMode)
        {
            Cam.Play();
        }
    }

    public void PauseButton()
    {
        if (Cam.PlayMode)
        {
            Cam.Play();
        }
    }

    public void selectBlock(int type)
    {
        camera.GetComponent<Cam>().selectBlockType((BlockTypes)type);
    }
}
