using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayer : MonoBehaviour {

    public GameObject[] CreatePos = new GameObject[4];
    public GameObject PlayerObj;
    public static bool[] PlayerUse = new bool[4];
    public GameObject PlayerUsePos;
    
    public GameObject Canvas;

    public GameObject[] Weapon = new GameObject[3];
    public GameObject[] WeaponUI = new GameObject[3];

    private bool bCreate = false;

	// Use this for initialization
	void Start () {
        for (int nCntPlayer = 0; nCntPlayer < 4; nCntPlayer++)
        {
            PlayerUse[nCntPlayer] = false;
        }
        CreatePlayerBlock(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (bCreate == true)
        {
            CreatePlayerBlock(false);
        }
	}



    //プレイヤーブロック生成
    void CreatePlayerBlock(bool allcreate)
    {

        for (int nCntPlayer = 0; nCntPlayer < 4; nCntPlayer++)
        {
            if (bCreate == false && allcreate == false)
            {
                break;
            }

            if (PlayerUse[nCntPlayer] == false)
            {
                

                // ブロックの作成
                GameObject block = GameObject.Instantiate(PlayerObj) as GameObject;

                //色設定
                Color col = new Color(0, 0, 0);
                RawImage image = block.GetComponent<RawImage>();
                switch (Random.Range(0, 3))
                {
                    case 0:
                        col.r = 1;
                        break;
                    case 1:
                        col.g = 1;
                        col.r = 1;
                        break;
                    case 2:
                        col.b = 1;
                        break;
                }
                col.a = 1;
                image.color = col;


                // ブロックのタイプ設定
                int type = Random.Range(0,3);


                //武器生成
                block.transform.localPosition = CreatePos[nCntPlayer].transform.localPosition;
                block.transform.SetParent(Canvas.transform, false);
                block.GetComponent<PlayerControl>().SetPlayer(CreatePos[nCntPlayer].transform.position, nCntPlayer, PlayerUsePos.transform.position, type, Weapon[type]);

                //武器のUI生成
                GameObject weaponui = GameObject.Instantiate(WeaponUI[type]) as GameObject;
                weaponui.transform.localPosition = new Vector3(0, 0, 0);
                weaponui.transform.SetParent(block.transform, false);


                PlayerUse[nCntPlayer] = true;
                bCreate = false;

            }
        }
    }


    public void SetCreate()
    {
        bCreate = true;
    }
}
