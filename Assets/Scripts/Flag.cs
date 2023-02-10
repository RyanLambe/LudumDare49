using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    public bool LastLevel = false;

    private void OnTriggerEnter(Collider other)
    {
        //if player runs into flag go to next scene
        if (other.transform.tag == "Player")
        {
            Debug.Log("win");
            Cam.PlayMode = false;
            if (LastLevel)
            {
                SceneManager.LoadScene(0);
                return;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
