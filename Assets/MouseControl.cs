using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour {

    private GameObject obj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        /* プレイヤーをタッチ移動
        //タッチ判定
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetMouseButtonDown(0))
        {
            Vector3 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Physics2D.Raycast(ray, -Vector2.up))
            {
                obj = Physics2D.Raycast(ray, -Vector2.up).transform.gameObject;

                if (obj.CompareTag("Player"))
                {

                    //当たった対象を座標移動
                    obj.GetComponent<PlayerControl>().SetTach(true);
                }

            }

        }
        */


        //タップで弾発射
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

        }
	}
}
