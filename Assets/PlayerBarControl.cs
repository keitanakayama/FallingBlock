using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarControl : MonoBehaviour {

    private CreatePlayer playercontrol;
    private bool bPlayer = false;

    private Vector2 purpos;
    private float move;

    private bool bTach = false;

	// Use this for initialization
	void Start () {

        playercontrol = GameObject.Find("CreatePlayer").gameObject.GetComponent<CreatePlayer>();
        purpos = transform.position;
        move = 0;

	}
	
	// Update is called once per frame
	void Update () {

        bPlayer = playercontrol.GetbPlayer();

        if (bPlayer == true)
        {
            Vector2 mousepos = transform.position;
            purpos = transform.position;
            //タッチ判定
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                bTach = true;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                bTach = false;

            }


            //カーソルの座標取得
            if (bTach == true)
            {
                mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                purpos.x = mousepos.x;
            }



            Vector2 pos;
            pos = transform.position;
            move = (purpos.x - pos.x) * 0.05f;
            pos.x += move;

            if (pos.x < 1.4f && pos.x > -2.4f)
            {
                
            }
            else
            {
                pos.x = transform.position.x;
            }
            
            transform.position = pos;



        }





	}


    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.collider.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(move,0);
        }

    }
    
}
