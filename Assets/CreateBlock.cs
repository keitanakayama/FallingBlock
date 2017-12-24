using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlock : MonoBehaviour {

    private float[] createpos = new float[5];
    private static int CreateCnt = 0;

    public GameObject Canvas;
    public GameObject Block;

    private static int startcnt = 0;
    private static float addwhite = 1;

    


    private List<GameObject> EnemyBlock = new List<GameObject>();

	// Use this for initialization
	void Start () {

        startcnt = 0;
        addwhite = 1;
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


        for (int nCntBlock = 0; nCntBlock < 100; nCntBlock++)
        {
            GameObject block = GameObject.Instantiate(Block) as GameObject;
            EnemyBlock.Insert(EnemyBlock.Count, block);
            EnemyBlock[nCntBlock].transform.parent = Canvas.transform;
            EnemyBlock[nCntBlock].SetActive(false);

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
        /*
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
        }*/


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


    public void Create()
    {
        float white = 1;
        if (Random.Range(0, 10) > 8)
        {
            white = Random.Range(addwhite, 1);
        }

        // ブロックの作成

        for (int nCntBlock = 0; nCntBlock < 100; nCntBlock++)
        {
            if (EnemyBlock[nCntBlock].activeInHierarchy == false)
            {
                EnemyBlock[nCntBlock].SetActive(true);

                // ブロックの位置設定
                EnemyBlock[nCntBlock].transform.position = new Vector3(Random.Range(-2.3f, 1.2f), transform.position.y - 2f, 0);
                EnemyBlock[nCntBlock].transform.parent = Canvas.transform;
                float scl = Random.Range(0.4f, 1.5f);
                EnemyBlock[nCntBlock].transform.localScale = new Vector3(scl, scl, scl);
                EnemyBlock[nCntBlock].GetComponent<BlockControl>().SetWhite(white);

                break;

            }
        }

    }


    public void Create(Vector3 vec3 , float scl)
    {
        for (int nCntBlock = 0; nCntBlock < 100; nCntBlock++)
        {
            if (EnemyBlock[nCntBlock].activeInHierarchy == false)
            {
                // ブロックの作成
                EnemyBlock[nCntBlock].SetActive(true);
                // ブロックの位置設定
                EnemyBlock[nCntBlock].transform.position = vec3;
                EnemyBlock[nCntBlock].transform.parent = Canvas.transform;
                EnemyBlock[nCntBlock].transform.localScale = new Vector3(scl, scl, scl);
                EnemyBlock[nCntBlock].GetComponent<BlockControl>().SetWhite(addwhite);

                break;
            }
        }
    }
    



    public static void AddWhite(float add)
    {
        addwhite -= add;
        if (addwhite < 0.1f)
        {
            addwhite = 0.1f;
        }
    }

    public void DestroyBlock()
    {
        for (int nCntBlock = 0; nCntBlock < 100; nCntBlock++)
        {
            if (EnemyBlock[nCntBlock].activeInHierarchy == true)
            {
                EnemyBlock[nCntBlock].GetComponent<BlockControl>().DestroyBlock();
                break;
            }
        }
    }


    public int GetBlockCnt()
    {
        return EnemyBlock.Count;
    }

}
