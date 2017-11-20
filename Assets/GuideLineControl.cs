using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideLineControl : MonoBehaviour {

    private static bool bUse = false;
    private static Vector2 pos;
    private Color linecolor;

	// Use this for initialization
	void Start () {

        linecolor = GameObject.Find("line").GetComponent<SpriteRenderer>().color;


	}
	
	// Update is called once per frame
	void Update () {
        transform.position = pos;
        if (bUse == false)
        {
            GameObject.Find("line").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
        }
        else
        {
            GameObject.Find("line").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
	}


    public static void SetGuideLine(Vector2 vec2)
    {
        bUse = true;
        pos = vec2;
    }

    public static void EndGuideLine()
    {
        bUse = false;
    }
}
