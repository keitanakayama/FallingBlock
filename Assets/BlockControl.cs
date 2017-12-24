using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockControl : MonoBehaviour {

    private RawImage image;
    private GageControl Gage;

    private GameObject effect;
    private Vector3 move;

    private float addwhite = 1;
    private CreateEffect Effect;

	// Use this for initialization
	void Start () {

        move.x = 0;
        move.y = 0;
        move.z = 0;
        image = GetComponent<RawImage>();
        Gage = GameObject.Find("CreateGage").gameObject.GetComponent<GageControl>();
        Effect = GameObject.Find("CreateEffect").GetComponent<CreateEffect>();

        Color col;
        col.r = addwhite;
        col.g = addwhite;
        col.b = addwhite;
        col.a = 255;

        image.color = col;
	}
	
	// Update is called once per frame
	void Update () {


        //移動        基本は移動なし
        if (move.x != 0 || move.y != 0)
        {
            Vector3 pos = transform.position;
            transform.position = pos + move;
            move += move;
        }

        //位置チェック
        if (transform.position.y <= -800)
        {
            DestroyBlock();
        }

        if (transform.position.y > 800)
        {
            DestroyBlock();
        }
        if (transform.position.x <= -600 || transform.position.x >= 600)
        {
            DestroyBlock();
        }


        //サイズチェック
        if (transform.localScale.x <= 0)
        {
            DestroyBlock();
        }


        //色チェック
        //赤
        if (image.color.r > 0.9f && image.color.g < 0.11f && image.color.b < 0.11f)
        {
            //炎とか
            Effect.SetEffect(new Vector2(transform.position.x, transform.position.y - 0.5f), COLOR.COLOR_RED);
            DestroyBlock();
        }

        //黄色
        else if (image.color.r > 0.9f && image.color.g > 0.9f && image.color.b < 0.11f)
        {
            //雷とか
            Effect.SetEffect(new Vector2(transform.position.x, transform.position.y - 0.5f), COLOR.COLOR_YELLOW);
            DestroyBlock();
        }


        //青
        else if (image.color.r < 0.11f && image.color.g < 0.11f && image.color.b > 0.9f)
        {
            //氷とか
            Effect.SetEffect(new Vector2(transform.position.x, transform.position.y - 0.5f), COLOR.COLOR_BLUE);
            DestroyBlock();
        }


        //オレンジ
        else if (image.color.r > 0.6f && image.color.g > 0.39f && image.color.b < 0.2f && (image.color.r - image.color.g * image.color.r - image.color.g) > 0.0015f )
        {
            //爆発とか
            Effect.SetEffect(new Vector2(transform.position.x, transform.position.y), COLOR.COLOR_ORANGE);
            DestroyBlock();
        }


        //緑
        else if (image.color.r < 0.62f && image.color.g > 0.6f && image.color.b < 0.49f)
        {
            //風とか
            Effect.SetEffect(new Vector2(transform.position.x, transform.position.y - 0.5f), COLOR.COLOR_GREEN);
            DestroyBlock();
        }


        //紫
        else if (image.color.r > 0.4f && image.color.g < 0.2f && image.color.b > 0.4f)
        {
            //ブラックホールとか
            Effect.SetEffect(new Vector2(transform.position.x, transform.position.y), COLOR.COLOR_PUPLE);
            DestroyBlock();
        }
		
	}


    //どのくらい白いかの設定
    public void SetWhite(float white)
    {
        addwhite = white;
    }

    //ブロック削除
    public void DestroyBlock()
    {
        Gage.AddGage(0.015f);
        //爆発
        Effect.SetEffect(new Vector2(transform.position.x, transform.position.y));
        //ScoreControl.AddScore((int)(100 + ((1 - addwhite) * 100)));

        Color col = new Color(1,1,1,1);
        image.color = col;

        CancelMove();
        gameObject.SetActive(false);
        AttackControl.AddAttack(1);
    }

    public void SetColor(Color playercolor)
    {
        //ブロックの色取得
        Color blockcolor;
        blockcolor = image.color;

        //ブロックが黄色かどうかを判定
        float yellowcheck = blockcolor.r - blockcolor.g;
        if (yellowcheck < 0)
        {
            yellowcheck *= -1;
        }

        //黄色と青を混ぜるときだけ例外
        if (playercolor.r == playercolor.g && blockcolor.r < blockcolor.b && blockcolor.g < blockcolor.b)
        {
            playercolor.r = 0;
        }
        if (yellowcheck < 0.1f && blockcolor.r > blockcolor.b && blockcolor.g > blockcolor.b && playercolor.r < playercolor.b && playercolor.g < playercolor.b)
        {
            playercolor.g = 1;
        }


        //ブロックと色を混ぜる
        Color col = Color.Lerp(blockcolor, playercolor, 0.3f * addwhite);
        col.a = 255;
        image.color = col;


    }




    //当たり判定
    void OnTriggerEnter2D(Collider2D col)
    {

        //水中に入ったような感じに重力をいじる
        if (col.tag == "DeadLine")
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.001f);
            transform.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
        }
    }


    //ふっとび
    public void SetMove(Vector3 vec3)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        move = vec3;
    }

    public void CancelMove()
    {
        move = new Vector3(0, 0, 0);
        GetComponent<BoxCollider2D>().enabled = true;
    }

}
