using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponControl : MonoBehaviour {

    private Color color;
    private bool bAttack = false;
    private int nType;
    private float move = 0.0f;
    private Vector2 basepos;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        switch (nType)
        {


                //筆
            case 0:
                {

                    move += 50f;

                    //プレイヤーを中心に回る
                    Vector3 axis = transform.TransformDirection(Vector3.forward);
                    transform.RotateAround(basepos, axis, move * Time.deltaTime);

                    if (move > 1000)
                    {
                        Destroy(gameObject);
                    }
                }
                break;

                //バケツ
            case 1:
                {

                    move += 50f;

                    //プレイヤーを中心に回る
                    Vector3 axis = transform.TransformDirection(Vector3.forward);
                    transform.RotateAround(basepos, axis, move * Time.deltaTime);

                    if (move > 1700)
                    {
                        Destroy(gameObject);
                    }
                }
                break;


                //注射
            case 2:
                {
                    Vector2 pos = transform.localPosition;

                    move -= 5f;
                    pos.y += move;
                    //プレイヤーから発射
                    transform.localPosition = pos;

                    if (move < -45)
                    {
                        Destroy(gameObject);
                    }
                }
                break;
        }
		
	}

    //当たり判定
    void OnTriggerEnter2D(Collider2D col)
    {


        //色をブロックに渡す
        if (col.tag == "Block")
        {
            col.GetComponent<BlockControl>().SetColor(color);
        }



    }

    public void SetAttack(Vector2 pos,Color col ,int i)
    {
        basepos = pos;
        nType = i;
        color = col;
    }
}
