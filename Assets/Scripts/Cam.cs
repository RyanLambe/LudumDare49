using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cam : MonoBehaviour
{
    public static bool PlayMode = false;
    Camera cam;

    public static Cam thisScript;
    public GameObject selected;

    public GameObject[] BlockPrefabs;

    public int[] counts = { 3, 4, 5, 2, 1};

    public GameObject Player;
    public GameObject blocks;

    float pos;
    public Vector2 LevelBounds;
    float height;

    public AudioSource place;
    public AudioSource Reset;

    void Start()
    {
        //setup
        cam = GetComponent<Camera>();
        pos = transform.position.x;
        height = transform.position.y;
        thisScript = this;
    }

    void Update()
    {
        if (PlayMode)
        {
            //adjust camera component settings
            cam.orthographic = false;
            cam.fieldOfView = 30;
            transform.position = new Vector3(transform.position.x, height + 2, -20);
        }
        else
        {
            //adjust camera component settings
            cam.orthographic = true;
            transform.position = new Vector3(transform.position.x, height, -20);

            //place blocks in scene
            if (selected == null)
                return;

            int count = counts[(int)selected.GetComponent<Block>().type];
            if (count <= 0)
            {
                return;
            }

            //move selected to under the mouse
            Plane plane = new Plane(Vector3.forward, transform.position + new Vector3(0, 0, 1));
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            float hit;
            if (plane.Raycast(ray, out hit))
            {
                Vector3 temp = ray.GetPoint(hit);
                selected.transform.position = temp;
            }

            if (Input.GetMouseButtonDown(0) && !mouseOverUI())
            {
                //place object
                place.Play();
                GameObject obj = Instantiate(selected, blocks.transform);
                Vector3 temp = selected.transform.position;
                temp.z = 0;
                obj.transform.position = temp;
                obj.GetComponent<Block>().Prefab = false;

                //remove from count
                count = --counts[(int)selected.GetComponent<Block>().type];
                Debug.Log((int)selected.GetComponent<Block>().type);
                if (count == 0)
                {
                    Destroy(selected);
                }
            }
        }
    }

    public bool mouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void selectBlockType(BlockTypes type)
    {
        Destroy(selected);

        if(counts[(int)type] > 0)
        {
            selected = Instantiate(BlockPrefabs[(int)type], transform);
            selected.transform.Translate(0, 0, 0);
            selected.GetComponent<Block>().Prefab = true;
        }
    }

    public static void Play()
    {
        PlayMode = !PlayMode;
        if (PlayMode)
        {
            Destroy(thisScript.selected);
        }
    }

    public static void ResetBlocks()
    {
        thisScript.Reset.Play();
        Transform blockParent = thisScript.blocks.transform;
        for (int i = 0; i < blockParent.childCount; i++)
        {
            thisScript.counts[(int)blockParent.GetChild(i).GetComponent<Block>().type]++;
            Destroy(blockParent.GetChild(i).gameObject);
        }
    }
}

public enum BlockTypes
{
    Cube = 0,
    Cylinder = 1,
    Line = 2,
    Beam = 3,
    TShape = 4
}
