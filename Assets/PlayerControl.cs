using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    private float move = 0;
    private int nLife = 3;

    private bool bPlayer = false;
    private Vector2 basepos;
    private bool bTach = false;

    private Vector2 Playerusepos;
    private int playernum = 0;

    private int hitcnt = 0;
    private bool bHit = false;

    private bool bStart = false;

    private GameObject arm;

    public GameObject weapon;
    private int nType;



	// Use this for initialization
	void Start () {


        foreach (Transform child in transform)
        {
            arm = child.gameObject;
            break;
        }

        
	}
	
	// Update is called once per frame
	void Update () {


        if (bTach == false || bStart == true)
        {
            if (bPlayer == true)
            {
                bStart = true;
                hitcnt++;
                if (hitcnt % 5 == 0)
                {
                    bHit = false;
                    hitcnt = 0;
                }

                //移動
                move -= 0.01f;
                transform.position = new Vector3(transform.position.x, transform.position.y + move, 0);


                //攻撃
                

                //削除

                if (transform.position.y < -100)
                {
                    //ブロック追加
                    CreateBlock.SetCnt(0);
                    GameObject.Find("CreateGage").gameObject.GetComponent<GageControl>().AddGage(0.3f);
                    nLife = 0;
                }
                if (nLife <= 0)
                {
                    CreatePlayer.PlayerUse[playernum] = false;
                    Destroy(gameObject);
                }
            }


            //初期位置に戻る
            else
            {

                Vector2 pos;
                pos = transform.localPosition;
                pos = transform.position;

                pos += (basepos - pos) * 0.5f;

                transform.position = pos;

            }
        }


        //プレイヤーをタッチしている
        else if (bTach == true && bStart == false)
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vec.z = 0;
            if (vec.y < 2)
            {
                vec.y = 2;
            }
            transform.position = vec;


            //落下位置設定
            if (Playerusepos.x > transform.position.x && -Playerusepos.x - 1f < transform.position.x)
            {
                bPlayer = true;
            }
            else
            {
                bPlayer = false;
            }



            //ガイドライン表示
            GuideLineControl.SetGuideLine(transform.position);

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                GuideLineControl.EndGuideLine();
                SetTach(false);  
            }

        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

	}

    public void SetTach(bool b)
    {
        bTach = b;
    }


    //プレイヤーの初期設定
    public void SetPlayer(Vector2 vec2, int num, Vector2 usepos , int type , GameObject obj)
    {
        Playerusepos = usepos;
        basepos = vec2;
        playernum = num;
        nType = type;
        weapon = obj;
    }


    
    
    //当たり判定
    void OnTriggerEnter2D(Collider2D col)
    {


        if ((bTach == false && bPlayer == true) || bStart == true)
        {
            //色をブロックに渡す
            if (col.tag == "Block")
            {
                if (bHit == false)
                {
                    nLife--;
                    bHit = true;
                
                    move *= -1 * 0.75f;
                }
                CreateWeapon();
                //arm.GetComponent<ArmControl>().SetAttack();
                col.GetComponent<BlockControl>().SetColor(transform.GetComponent<RawImage>().color);
            }

        }
    }

    void CreateWeapon()
    {
        GameObject block = GameObject.Instantiate(weapon) as GameObject;
        block.transform.SetParent(GameObject.Find("Canvas").transform,false);
        switch (nType)
        {
            case 0:
                {
                    block.transform.position = new Vector3(transform.position.x - 0.9f, transform.position.y, transform.position.z);
                }
                break;

            case 1:
                {
                    block.transform.position = new Vector3(transform.position.x, transform.position.y + 1.55f, transform.position.z);
                }
                break;

            default:
                {
                    block.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                }
                break;
            
        }
        block.GetComponent<WeaponControl>().SetAttack(new Vector3(transform.position.x , transform.position.y + 0.5f , transform.position.z) , transform.GetComponent<RawImage>().color, nType);
    }

}
