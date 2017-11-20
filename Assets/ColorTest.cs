using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTest : MonoBehaviour {

    private RawImage image;
    public Color color;
    public float R = 0;
    public float G = 0;
    public float B = 1;


    public GameObject block;

	// Use this for initialization
	void Start () {
        /*
        color.r = R;
        color.g = G;
        color.b = B;

        //ブロックの色取得
        Color blockcolor;

        image = block.GetComponent<RawImage>();
        blockcolor = image.color;


        //ブロックと色を混ぜる
        Color col = Color.Lerp(blockcolor, color, 0.5f);
        col.a = 255;
        image.color = col;

		*/
	}
	
	// Update is called once per frame
	void Update () {
        /*
        //設定したい色の更新
        color.r = R;
        color.g = G;
        color.b = B;


        //タッチ判定
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            //ブロックの色取得
            Color blockcolor;

            image = block.GetComponent<RawImage>();
            blockcolor = image.color;

            //ブロックが黄色かどうかを判定
            float yellowcheck = blockcolor.r - blockcolor.g;
            if (yellowcheck < 0)
            {
                yellowcheck *= -1;
            }

            //黄色と青を混ぜるときだけ例外
            if (color.r == color.g && blockcolor.r < blockcolor.b && blockcolor.g < blockcolor.b)
            {
                color.r = 0;
            }
            if (yellowcheck < 0.1f && blockcolor.r > blockcolor.b && blockcolor.g > blockcolor.b && color.r < color.b && color.g < color.b)
            {
                color.g = 1;
            }


            //ブロックと色を混ぜる
            Color col = Color.Lerp(blockcolor, color, 0.25f);
            col.a = 255;
            image.color = col;



        }

        */
        
	}
}
