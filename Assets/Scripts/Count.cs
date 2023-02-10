using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    Text text;
    public BlockTypes type;

    void Start()
    {
        //get Text to update
        text = GetComponent<Text>();
    }

    void Update()
    {
        //update text with count from cam
        text.text = Cam.thisScript.counts[(int)type].ToString();
    }
}
