using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    private float move = 0.01f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector2(transform.position.x + move, transform.position.y);

	}



    //当たり判定
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "UI")
        {
            move *= -1;
        }


        if (col.transform.tag == "Player")
        {
            //ブロック追加
            CreateBlock.SetCnt(0);
            //攻撃を受けた
            CreateBlock.AddWhite(0.1f);
            AttackControl.AddAttack(10);
            //GameObject.Find("CreateGage").gameObject.GetComponent<GageControl>().AddGage(-0.35f);
        }
    }
}
