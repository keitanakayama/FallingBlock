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

	// Use this for initialization
	void Start () {

        basepos = transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () {

        gage += 0.0005f;

        GetComponent<Slider>().value = gage;


        if (gage >= 1)
        {
            gage = 0;
            //プレイヤー生成
            Player.GetComponent<CreatePlayer>().SetCreate();
        }



        //プレイヤーがいないとき連打でゲージ上昇
        bool bPlayer = false;
        for (int nCntPlayer = 0; nCntPlayer < 4; nCntPlayer++)
        {
            if (CreatePlayer.PlayerUse[nCntPlayer] == true)
            {
                bPlayer = true;
            }
        }

        if (bPlayer == false)
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

            


        }
        
	}


    public void AddGage(float i)
    {
        gage += i;
    }
}
