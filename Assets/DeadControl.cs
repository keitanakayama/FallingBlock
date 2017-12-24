using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class DeadControl : MonoBehaviour {

    private int hitcnt = 0;
    private bool bHit = false;

    public GameObject GameOver;
    public GameObject DeadRed;

    private int DEADTIME = 300;
    private bool bEnd = false;
    private int timecnt = 0;


	// Use this for initialization
	void Start () {
        bEnd = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (bHit == false)
        {
            hitcnt = 0;
        }



        if (bHit == true)
        {
            hitcnt++;
        }
        //else
        {
            bHit = false;
        }


        //画面を赤点滅
        if (hitcnt > 120)
        {
            DeadRed.GetComponent<ImageControl>().SetImage();
        }

        if (hitcnt < 120 || bEnd == true)
        {
            DeadRed.GetComponent<ImageControl>().EndImage();
        }



        //ゲームオーバー
        if (hitcnt >= DEADTIME && bEnd == false)
        {
            // イメージの作成
            GameObject block = GameObject.Instantiate(GameOver) as GameObject;
            // イメージの位置設定
            block.transform.position = new Vector3(0,0,0);
            block.transform.SetParent(transform.parent, false);

            bEnd = true;
        }

        if (bEnd == true)
        {
            timecnt++;

            if (timecnt > 120)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetMouseButtonDown(0))
                {
                    Transitioner.Instance.TransitionToScene("title");
                }
            }

            if (timecnt > 300)
            {
                Transitioner.Instance.TransitionToScene("title");
            }

        }
	}

    //当たり判定
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Block")
        {
            bHit = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Block")
        {
            bHit = false;
        }
    }
}
