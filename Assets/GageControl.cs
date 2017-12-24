using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GageControl : MonoBehaviour {

    private float gage = 0;
    public GameObject Player;

    private Vector2 basepos;
    private bool bTap = false;
    private int typecnt = 0;
    private int timecnt = 0;

    private CreatePlayer createplayer;
    private CreateBlock createblock;

    private bool bNext = false;
    private int createcnt = 0;

	// Use this for initialization
	void Start () {

        createcnt = 0;
        bNext = false;
        gage = 0;
        createblock = GameObject.Find("CreateBlock").GetComponent<CreateBlock>();
        /*
        basepos = transform.localPosition;
        createplayer = GameObject.Find("CreatePlayer").GetComponent<CreatePlayer>();*/
	}
	
	// Update is called once per frame
	void Update () {


        GetComponent<Slider>().value = gage;
        if (gage > 1)
        {
            gage = 1;
        }
        /*
        if (gage <= 0 && bNext == false)
        {
            //攻撃を受けた
            CreateBlock.AddWhite(0.2f);
            //やられた演出
            bNext = true;
        }*/


        //今あるブロックを少し削除
        if (bNext == true)
        {
            if (createcnt % 5 == 0)
            {
                //createblock.DestroyBlock();
            }

            createcnt++;

            if (createcnt > 100)
            {
                createcnt = 0;
                gage = 1;
                bNext = false;
                CreateBlock.SetCnt(0);
            }
        }
        /*
        gage += 0.0005f;

        GetComponent<Slider>().value = gage;


        if (gage >= 1)
        {
            gage = 0;
            //プレイヤー生成
            createplayer.SetCreate();
        }



        //プレイヤーがいないとき連打でゲージ上昇
        if (createplayer.GetPlayerNum() <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetMouseButtonDown(0))
            {
                gage += 0.005f;
                if (bTap == false)
                {
                    bTap = true;
                }
            }
        }


        if (bTap == true)
        {
            if (timecnt % 3 == 0)
            {
                typecnt++;
            }

            timecnt++;
            switch (typecnt)
            {
                case 0:
                    transform.localPosition = new Vector2(basepos.x + 3, basepos.y);
                    break;
                case 1:
                    transform.localPosition = new Vector2(basepos.x - 6, basepos.y);
                    break;
                default:
                    transform.localPosition = basepos;
                    typecnt = 0;
                    timecnt = 0;
                    bTap = false;
                    break;
            }

            


        }*/
        
	}


    public void AddGage(float i)
    {
        gage += i;
    }
}
