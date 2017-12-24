using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class ResultControl : MonoBehaviour {

    public GameObject[] ScoreRank = new GameObject[5];
    private int nTime = 0;

	// Use this for initialization
	void Start () {

        nTime = 0;

        ScoreControl.LoadScore.Sort();


        //スコア表示
        for (int nCntScore = 5; nCntScore > 0; nCntScore--)
        {
            ScoreRank[5 - nCntScore].GetComponent<Text>().text = "" + ScoreControl.LoadScore[nCntScore];

            //セーブ
            PlayerPrefs.SetInt("Score" + nCntScore, ScoreControl.LoadScore[nCntScore]);
        }
        

	}
	
	// Update is called once per frame
	void Update () {

        nTime++;

        if (nTime > 120)
        {
             if (Input.GetKeyDown(KeyCode.Mouse0))
             {
                 //SceneManager.LoadScene("title");
                 Transitioner.Instance.TransitionToScene("title");
             }
        }

	}
}
