using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackControl : MonoBehaviour {

    private static int nAttack = 0;
    public GameObject DamageText;

	// Use this for initialization
	void Start () {

        nAttack = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDamageText()
    {
        DamageText.SetActive(true);
        DamageText.GetComponent<Text>().text = "" + nAttack * 10;
        GameObject.Find("UI").GetComponent<ScoreControl>().SaveScore(nAttack * 10);
    }

    public static void AddAttack(int i)
    {
        nAttack += i;
    }

    public static int GetAttack()
    {
        return nAttack;
    }
}
