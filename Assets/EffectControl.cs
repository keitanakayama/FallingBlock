using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum COLOR {

    COLOR_RED = 0,
    COLOR_YELLOW,
    COLOR_BLUE,
    COLOR_GREEN,
    COLOR_ORANGE,
    COLOR_PUPLE,
    COLOR_MAX,
}

public class EffectControl : MonoBehaviour {

    public float DeadTime;
    private float time;
    public COLOR Type;
    private Vector3 move;
    private int nCnt = 0;
    private List<GameObject> hitblock = new List<GameObject>();


    void Awake()
    {
        int random = Random.Range(0, 3);
        DeadTime *= 60;

        switch (Type)
        {

            case COLOR.COLOR_GREEN:
                if (random == 0)
                {
                    move.x = 0.00025f;
                }
                else
                {
                    move.x = -0.00025f;
                }
                break;

        }
    }

	// Use this for initialization
	void Start () {

        switch (Type)
        {

            case COLOR.COLOR_GREEN:
                if (Random.Range(0, 2) == 0)
                {
                    move.x = 0.00025f;
                }
                else
                {
                    move.x = -0.00025f;
                }
                break;

        }
	}
	
	// Update is called once per frame
	void Update () {



        time++;
        if (time > DeadTime)
        {

            //貫いたブロックを削除
            for (int nCntBlock = 0; nCntBlock < hitblock.Count; nCntBlock++)
            {
                Destroy(hitblock[nCntBlock]);
            }
            
            //貫いた数分大きいブロック追加
            if (Type == COLOR.COLOR_BLUE)
            {
                GameObject.Find("CreateBlock").GetComponent<CreateBlock>().Create(transform.position, 1 + 0.1f * nCnt);
            }
            
            Destroy(gameObject);
        }
        

        transform.position = transform.position + move;

        move += move;


        //位置チェック
        if (transform.position.y <= -800)
        {
            Destroy(gameObject);
        }
        if (transform.position.x <= -600 || transform.position.x >= 600)
        {
            Destroy(gameObject);
        }

	}

    //当たり判定
    void OnTriggerStay2D(Collider2D col)
    {
        if (Type == COLOR.COLOR_RED)
        {
            col.transform.localScale = new Vector3(col.transform.localScale.x - 0.01f, col.transform.localScale.y - 0.01f, col.transform.localScale.z - 0.01f);
        }
    }


    //当たり判定
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Block")
        {
            switch (Type)
            {

                case COLOR.COLOR_GREEN:
                    col.GetComponent<BlockControl>().SetMove(move);

                    break;

                case COLOR.COLOR_BLUE:
                    nCnt++;
                    hitblock.Add(col.gameObject);
                    break;

            }
        }
    }
}
