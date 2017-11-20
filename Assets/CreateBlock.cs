using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlock : MonoBehaviour {

    private float[] createpos = new float[5];
    private static int CreateCnt = 0;

    public GameObject Canvas;
    public GameObject Block;

    private static int startcnt = 0;

	// Use this for initialization
	void Start () {

        CreateCnt = 0;

        //初期位置設定
        for (int nCntBlock = 0; nCntBlock < 5; nCntBlock++)
        {
            if (nCntBlock == 0)
            {
                createpos[nCntBlock] = -2.3f;
            }
            else
            {
                createpos[nCntBlock] = 3.7f / nCntBlock - 2.5f;
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        CreateCnt++;

        if (startcnt == 0)
        {
            if (CreateCnt % 5 == 0)
            {
                Create();

                if (CreateCnt == 100)
                {
                    startcnt = 1;
                }
            }
        }



        //自動生成
        if (startcnt == 1 && CreateCnt % 120 == 0)
        {
            GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("Block");

            int blockcnt = 0;

            foreach (GameObject obj in tagobjs)
            {
                blockcnt++;
            }


            if (blockcnt < 30)
            {
                Create();
            }
        }


        //プレイヤーブロックがないとき追加生成
        bool bPlayer = false;
        for (int nCntPlayer = 0; nCntPlayer < 4; nCntPlayer++)
        {
            if (CreatePlayer.PlayerUse[nCntPlayer] == true)
            {
                bPlayer = true;
            }
        }

        if (bPlayer == false && CreateCnt % 45 == 0)
        {
            Create();
        }
		
	}

    public static void SetCnt(int cnt)
    {
        startcnt = cnt;
        CreateCnt = 50;
    }


    void Create()
    {
        // ブロックの作成
        GameObject block = GameObject.Instantiate(Block) as GameObject;
        // ブロックの位置設定
        block.transform.position = new Vector3(Random.Range(-2.3f, 1.2f), transform.position.y, 0);
        //block.transform.position = transform.position;
        block.transform.parent = Canvas.transform;
        float scl = Random.Range(0.4f, 1.5f);
        block.transform.localScale = new Vector3(scl, scl, scl);
    }


    public void Create(Vector3 vec3 , float scl)
    {
        // ブロックの作成
        GameObject block = GameObject.Instantiate(Block) as GameObject;
        // ブロックの位置設定
        block.transform.position = vec3;
        block.transform.parent = Canvas.transform;
        block.transform.localScale = new Vector3(scl, scl, scl);
    }

}
