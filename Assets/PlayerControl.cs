using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    private float move = 0;
    private int nLife = 10;

    private bool bPlayer = false;
    private Vector2 basepos;
    private bool bTach = false;

    private Vector2 Playerusepos;
    private int playernum = 0;

    private int hitcnt = 0;
    private bool bHit = false;

    private bool bStart = false;
    private bool bMove = true;


    private GameObject weapon;
    private int nType;

    private Sprite weaponsprite;

    private bool bRot = false;
    private Vector3 oldposition;

    private CreatePlayer createplayer;

    private GameObject effect;
    private GageControl gagecontrol;
    private Color playercolor;
    private CreateBlock createblock;

    private Rigidbody2D playerrb;

    public GameObject KappaImage;
    private RawImage kappabody;

	// Use this for initialization
	void Start () {

        kappabody = KappaImage.GetComponent<RawImage>();
        createplayer = GameObject.Find("CreatePlayer").GetComponent<CreatePlayer>();
        gagecontrol = GameObject.Find("CreateGage").gameObject.GetComponent<GageControl>();
        GetComponent<CircleCollider2D>().enabled = false;
        playercolor = transform.GetComponent<RawImage>().color;
        createblock = GameObject.Find("CreateBlock").GetComponent<CreateBlock>();
        playerrb = GetComponent<Rigidbody2D>();

        kappabody.color = transform.GetComponent<RawImage>().color;
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
                move = -500f;

                //進行方向に向きを合わせる

                Vector3 vec3 = transform.position;
                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                vec3.x += vel.x;
                vec3.y += vel.y;
                var vec = (vec3 - oldposition).normalized;
                transform.rotation = Quaternion.FromToRotation(Vector3.left, vec);


                //移動量チェック
                if (playerrb.velocity.x * playerrb.velocity.x < 1)
                {
                    vel = playerrb.velocity;
                    vel.x *= 2;
                    playerrb.velocity = vel;
                }

                //重力
                if (transform.position.y > 2.5f)
                {
                    playerrb.gravityScale = 0.5f;
                }
                else
                {
                    playerrb.gravityScale = 0;
                    if (playerrb.velocity.y * playerrb.velocity.y < 1)
                    {
                        vel = playerrb.velocity;
                        vel.y *= 2;
                        playerrb.velocity = vel;
                    }
                }



                //攻撃
                

                //削除

                //画面外チェック
                if (transform.position.y < -100)
                {
                    //ブロック追加
                    //CreateBlock.SetCnt(0);
                    gagecontrol.AddGage(0.3f);
                    nLife = 0;
                }
                if (transform.position.y > 20)
                {
                    nLife = 0;
                }





                if (nLife <= 0)
                {
                    CreatePlayer.PlayerUse[playernum] = false;
                    DestroyPlayer();
                }

            }


            //初期位置に戻る
            else
            {

                Vector2 pos;
                pos = transform.position;

                pos += (basepos - pos) * 0.5f;

                transform.position = pos;

                if ((basepos.x - pos.x) < 0.01f)
                {
                    bMove = false;
                }

            }
        }

        if (bRot == true && bPlayer == false)
        {
            if (transform.tag == "LastPlayer")
            {
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.7025f, transform.rotation.w);
            }
            else
            {
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, Mathf.PingPong(0.1f + Time.time, 0.9f), transform.rotation.w);
            }
        }


            /*
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
        */
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        oldposition = transform.position;
	}






    void DestroyPlayer()
    {
        //爆発エフェクト
        GameObject explosion = GameObject.Instantiate(effect) as GameObject;
        explosion.transform.position = transform.position;
        explosion.GetComponent<ExplosionControl>().SetColor(playercolor);

        
        createplayer.DestroyPlayer();
        Destroy(gameObject);

        createblock.Create();

        //残機を減らす
        ScoreControl.SubScore();
    }





    public void SetTach(bool b)
    {
        if (bMove == false && bPlayer == false)
        {
            bPlayer = b;

            GetComponent<CircleCollider2D>().enabled = true;


            //角度決定
            float angle = transform.eulerAngles.z * (Mathf.PI / 180.0f);
            Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);

            // 自身の向きに移動
            move = 500 - 500 / (createblock.GetBlockCnt() + 1);
            playerrb.velocity = dir * -move * Time.deltaTime;


        }
    }

    public bool GetbPlayer()
    {
        return bPlayer;
    }


    //プレイヤーの初期設定
    public void SetPlayer(Vector2 vec2, int num, Vector2 usepos , int type , GameObject obj , Sprite sprite , GameObject effectobj)
    {
        Playerusepos = usepos;
        basepos = vec2;
        playernum = num;
        nType = type;
        weapon = obj;
        weaponsprite = sprite;
        effect = effectobj;
    }

    public void SetBasePos(Vector2 vec2)
    {
        basepos = vec2;
    }


    //一番上のプレイヤー
    public void SetRot()
    {
        bRot = true;
    }

    
    
    //当たり判定
    void OnTriggerEnter2D(Collider2D col)
    {


        if (transform.tag == "LastPlayer")
        {
            if (col.tag == "Block")
            {
                col.GetComponent<BlockControl>().DestroyBlock();
            }

            if (col.tag == "Enemy")
            {
                GameObject.Find("AttackControl").GetComponent<AttackControl>().SetDamageText();
            }
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if ((bTach == false && bPlayer == true) || bStart == true && bHit == false)
        {
            //色をブロックに渡す
            if (col.collider.tag == "Block")
            {
                if (bHit == false)
                {
                    nLife--;
                    bHit = true;
                
                    move *= -1 * 0.75f;
                }
                //CreateWeapon();
                //arm.GetComponent<ArmControl>().SetAttack();
                col.collider.GetComponent<BlockControl>().SetColor(playercolor);
            }

            //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z * -0.5f, transform.rotation.w);

        }


        if (col.collider.tag == "Destroy")
        {
            DestroyPlayer();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if ((bTach == false && bPlayer == true) || bStart == true && bHit == false)
        {

            if (col.collider.tag == "Block" || col.collider.tag == "UI")
            {
                // 移動量が少なかったら加速
                if (playerrb.velocity.x * playerrb.velocity.x < 3)
                {
                    Vector2 vel = playerrb.velocity;
                    vel.x *= 2;
                    playerrb.velocity = vel;
                }
                if (playerrb.velocity.y * playerrb.velocity.y < 3)
                {
                    Vector2 vel = playerrb.velocity;
                    vel.y *= 2;
                    playerrb.velocity = vel;
                }
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
