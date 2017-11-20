using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageControl : MonoBehaviour {


    private bool bUse = false;
    private Color col;
    public float alphacnt = 0;
    private float time = 0;

	// Use this for initialization
	void Start () {
        col = gameObject.GetComponent<Image>().color;
	}
	
	// Update is called once per frame
	void Update () {
        time += 0.02f;

        if (bUse == true)
        {
           alphacnt = Mathf.PingPong(time, 0.7f);
        }



        col = new Color(255, 0, 0, alphacnt);
        gameObject.GetComponent<Image>().color = col;
	}

    public void SetImage()
    {
        bUse = true;
    }

    public void EndImage()
    {
        bUse = false;
        alphacnt = 0;
        time = 0;
    }
}
