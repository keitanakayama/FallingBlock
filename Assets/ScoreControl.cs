using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour {

    private static int score = 0;
    private static int addscore = 0;

    public GameObject scoretext;
    private Text text;
    private CreatePlayer player;
    private static bool bLast;

    public static List<int> LoadScore = new List<int>();
    private int timecnt = 0;

	// Use this for initialization
	void Start () {

        LoadScore.Add(PlayerPrefs.GetInt("Score0", 0));
        LoadScore.Add(PlayerPrefs.GetInt("Score1", 0));
        LoadScore.Add(PlayerPrefs.GetInt("Score2", 0));
        LoadScore.Add(PlayerPrefs.GetInt("Score3", 0));
        LoadScore.Add(PlayerPrefs.GetInt("Score4", 0));

        text = scoretext.GetComponent<Text>();
        player = GameObject.Find("CreatePlayer").GetComponent<CreatePlayer>();
        score = 30;
        addscore = 0;
        bLast = false;
        timecnt = 0;
	}
	
	// Update is called once per frame
	void Update () {
 
        /*
        if (addscore > 0)
        {
            score += 10;
            addscore -= 10;
        }*/

        text.text = "残り" + score;

        if (score <= 0)
        {
            timecnt++;
            player.SetLastPlayer();
        }

        if (timecnt > 120)
        {
            bLast = true;
        }

	}

    public void SaveScore(int i)
    {
        LoadScore.Add(i);
    }

    /*
    public static void AddScore(int getscore)
    {
        addscore += getscore;
    }*/

    public static void SubScore()
    {
        score--;
    }

    public static int GetScore()
    {
        return score;
    }


    public static bool GetbLast()
    {
        return bLast;
    }
}
