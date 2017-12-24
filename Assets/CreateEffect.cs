using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEffect : MonoBehaviour {

    public GameObject[] Effect = new GameObject[(int)COLOR.COLOR_MAX];
    public GameObject[] AuthorEffect = new GameObject[1];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEffect(Vector2 pos , COLOR type)
    {
        // ブロックの作成
        GameObject effect = GameObject.Instantiate(Effect[(int)type]) as GameObject;
        // ブロックの位置設定
        effect.transform.position = pos;
        effect.transform.parent = GameObject.Find("Canvas").transform;

    }

    public void SetEffect(Vector2 pos)
    {
        // ブロックの作成
        GameObject effect = GameObject.Instantiate(AuthorEffect[0]) as GameObject;
        // ブロックの位置設定
        effect.transform.SetParent(GameObject.Find("Canvas").transform, false);
        effect.transform.position = pos;
        
        //effect.transform.parent = GameObject.Find("Canvas").transform;

    }
}
