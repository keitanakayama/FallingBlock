using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class CreatePlayer : MonoBehaviour {

    public GameObject[] CreatePos = new GameObject[4];
    public GameObject PlayerObj;
    public static bool[] PlayerUse = new bool[4];
    public GameObject PlayerUsePos;
    
    public GameObject Canvas;

    public GameObject[] Weapon = new GameObject[3];
    public GameObject[] WeaponUI = new GameObject[3];

    public Sprite[] WeaponSprite = new Sprite[3];

    private bool bCreate = false;

    private List<GameObject> playerblock = new List<GameObject>();


    public GameObject[] ExplosionEffect = new GameObject[3];

	// Use this for initialization
	void Start () {
        for (int nCntPlayer = 0; nCntPlayer < 4; nCntPlayer++)
        {
            PlayerUse[nCntPlayer] = false;
        }
        CreatePlayerBlock(true);
        playerblock[0].GetComponent<PlayerControl>().SetRot();
	}
	
	// Update is called once per frame
	void Update () {
        if (ScoreControl.GetScore() > 2)
        {
            if (playerblock.Count < 4)
            {
                bCreate = true;
            }
            if (bCreate == true)
            {
                CreatePlayerBullet();
            }
        }

        if (ScoreControl.GetScore() <= 0)
        {
            if (playerblock.Count <= 0)
            {
                Transitioner.Instance.TransitionToScene("result");
            }
        }


        //タップで弾発射
        if (Input.GetKeyDown(KeyCode.Mouse0) && ScoreControl.GetScore() > 0)
        {
            UsePlayer();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && ScoreControl.GetbLast() == true)
        {
            UsePlayer();
        }

	}


    public void UsePlayer()
    {
        if (playerblock.Count > 0)
        {
            playerblock[0].GetComponent<PlayerControl>().SetTach(true);
            
        }
    }

    public bool GetbPlayer()
    {
        if (playerblock.Count > 0)
        {
            return playerblock[0].GetComponent<PlayerControl>().GetbPlayer();
        }

        return false;
    }

    void CreatePlayerBullet()
    {
        // ブロックの作成
        GameObject block = GameObject.Instantiate(PlayerObj) as GameObject;

        //色設定
        Color col = new Color(0, 0, 0);
        RawImage image = block.GetComponent<RawImage>();

        int colortype = Random.Range(0, 3);
        switch (colortype)
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
        int type = Random.Range(0, 3);

        //武器生成
        block.transform.localPosition = CreatePos[3].transform.localPosition;
        block.transform.SetParent(Canvas.transform, false);
        block.GetComponent<PlayerControl>().SetPlayer(CreatePos[3].transform.position, 3, PlayerUsePos.transform.position, type, Weapon[type], WeaponSprite[type], ExplosionEffect[colortype]);

        //武器のUI生成
        GameObject weaponui = GameObject.Instantiate(WeaponUI[type]) as GameObject;
        weaponui.transform.localPosition = new Vector3(0, 0, 0);
        weaponui.transform.SetParent(block.transform, false);

        //プレイヤーデータを確保
        playerblock.Insert(playerblock.Count, block.gameObject);

        PlayerUse[3] = true;
        bCreate = false;
        for (int nCntBlock = 0; nCntBlock < playerblock.Count; nCntBlock++)
        {
            if (playerblock[nCntBlock] != null)
            {
                playerblock[nCntBlock].GetComponent<PlayerControl>().SetBasePos(CreatePos[nCntBlock].transform.position);
            }
            /*else
            {
                break;
            }*/
        }

    }


    public void DestroyPlayer()
    {
        

        playerblock.RemoveAt(0);
        for (int nCntBlock = 0; nCntBlock < playerblock.Count; nCntBlock++)
        {
            if (playerblock[nCntBlock] != null)
            {
                playerblock[nCntBlock].GetComponent<PlayerControl>().SetBasePos(CreatePos[nCntBlock].transform.position);
            }
            /*else
            {
                break;
            }*/
        }

        if (playerblock.Count > 0)
        {
            playerblock[0].GetComponent<PlayerControl>().SetRot();
        }
    }

    //プレイヤーブロック生成
    void CreatePlayerBlock(bool allcreate)
    {

        if (playerblock.Count < 3)
        {

            for (int nCntPlayer = 0; nCntPlayer < 4; nCntPlayer++)
            {

                if (allcreate == false)
                {
                    nCntPlayer = 3;
                }


                // ブロックの作成
                GameObject block = GameObject.Instantiate(PlayerObj) as GameObject;

                //色設定
                Color col = new Color(0, 0, 0);
                RawImage image = block.GetComponent<RawImage>();
                int colortype = Random.Range(0, 3);
                switch (colortype)
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
                int type = Random.Range(0, 3);


                //武器生成
                block.transform.localPosition = CreatePos[nCntPlayer].transform.localPosition;
                block.transform.SetParent(Canvas.transform, false);
                block.GetComponent<PlayerControl>().SetPlayer(CreatePos[nCntPlayer].transform.position, nCntPlayer, PlayerUsePos.transform.position, type, Weapon[type], WeaponSprite[type], ExplosionEffect[colortype]);

                //武器のUI生成
                GameObject weaponui = GameObject.Instantiate(WeaponUI[type]) as GameObject;
                weaponui.transform.localPosition = new Vector3(0, 0, 0);
                weaponui.transform.SetParent(block.transform, false);


                //プレイヤーデータを確保
                playerblock.Insert(playerblock.Count, block.gameObject);

                PlayerUse[nCntPlayer] = true;
                bCreate = false;



            }
        }


 
    }


    public void SetCreate()
    {
        bCreate = true;
    }

    public int GetPlayerNum()
    {
        return playerblock.Count;
    }

    public void SetLastPlayer()
    {
        playerblock[0].transform.localScale = new Vector3(3, 3, 1);
        playerblock[0].transform.GetComponent<CircleCollider2D>().isTrigger = true;
        playerblock[0].transform.GetComponent<CircleCollider2D>().radius = 125;
        playerblock[0].transform.tag = "LastPlayer";
    }
}
