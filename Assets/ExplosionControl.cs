using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControl : MonoBehaviour {

    private int nLife = 30;
    private Color color;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        nLife--;

        if (nLife < 0)
        {
            Destroy(gameObject);
        }



	}


    public void SetColor(Color col)
    {
        color = col;
    }

    //当たり判定
    void OnTriggerEnter2D(Collider2D col)
    {

        //色をブロックに渡す
        if (col.tag == "Block")
        {
            col.GetComponent<BlockControl>().SetColor(color);
        }
    }

}
