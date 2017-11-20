using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockControl : MonoBehaviour {

    private RawImage image;
    private GameObject Gage;

    private GameObject effect;
    private Vector3 move;

	// Use this for initialization
	void Start () {

        move.x = 0;
        move.y = 0;
        move.z = 0;
        image = GetComponent<RawImage>();
        Gage = GameObject.Find("CreateGage").gameObject;
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
            GameObject.Find("CreateEffect").GetComponent<CreateEffect>().SetEffect(new Vector2(transform.position.x , transform.position.y - 0.5f ), COLOR.COLOR_RED);
            DestroyBlock();
        }

        //黄色
        else if (image.color.r > 0.9f && image.color.g > 0.9f && image.color.b < 0.11f)
        {
            //雷とか
            DestroyBlock();
        }


        //青
        else if (image.color.r < 0.11f && image.color.g < 0.11f && image.color.b > 0.9f)
        {
            //氷とか
            GameObject.Find("CreateEffect").GetComponent<CreateEffect>().SetEffect(new Vector2(transform.position.x, transform.position.y - 0.5f), COLOR.COLOR_BLUE);
            DestroyBlock();
        }


        //オレンジ
        else if (image.color.r > 0.6f && image.color.g > 0.39f && image.color.b < 0.2f)
        {
            //爆発とか
            DestroyBlock();
        }


        //緑
        else if (image.color.r < 0.62f && image.color.g > 0.6f && image.color.b < 0.49f)
        {
            //風とか
            GameObject.Find("CreateEffect").GetComponent<CreateEffect>().SetEffect(new Vector2(transform.position.x, transform.position.y - 0.5f), COLOR.COLOR_GREEN);
            DestroyBlock();
        }


        //紫
        else if (image.color.r > 0.4f && image.color.g < 0.2f && image.color.b > 0.4f)
        {
            //毒の雨とか
            DestroyBlock();
        }
		
	}

    //ブロック削除
    public void DestroyBlock()
    {
        Gage.GetComponent<GageControl>().AddGage(0.15f);
        Destroy(gameObject);
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
        Color col = Color.Lerp(blockcolor, playercolor, 0.3f);
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
}
