using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmControl : MonoBehaviour {

    private float move = 0.0f;
    private GameObject player;
    private Vector2 basepos;
    private bool bAttack;
    private GameObject weapon;

	// Use this for initialization
	void Start () {

        player = transform.parent.gameObject;
        basepos = transform.localPosition;

        //武器取得
        foreach (Transform child in transform)
        {
            weapon = child.gameObject;
            break;
        }

        //色設定
        transform.GetComponent<Image>().color = transform.parent.GetComponent<RawImage>().color;
	}
	
	// Update is called once per frame
    void Update()
    {

        /*
        //筆
        {
            move += 50f;

            //プレイヤーを中心に回る
            Vector3 axis = transform.TransformDirection(Vector3.forward);
            transform.RotateAround(player.transform.position, axis, move * Time.deltaTime);

            if (move > 1000)
            {
                transform.localPosition = basepos;
                move = 0;
                DisableAttack();
            }
        }*/



        //バケツ
        if (bAttack == true)
        {
            move += 75;


            //float angle = 450;
            //move += (angle - move) * 0.1f;

            //プレイヤーを中心に回る
            Vector3 axis = transform.TransformDirection(Vector3.forward);
            transform.RotateAround(player.transform.position, axis, move * Time.deltaTime);

            if (move > 2000)
            {
                transform.localPosition = basepos;
                move = 0;
                DisableAttack();
            }
        }
        
    }

    void DisableAttack()
    {
        bAttack = false;
        //weapon.GetComponent<WeaponControl>().SetAttack(bAttack);
    }

    public void SetAttack()
    {
        bAttack = true;
        //weapon.GetComponent<WeaponControl>().SetAttack(bAttack);
    }
}
