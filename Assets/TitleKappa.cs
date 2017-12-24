using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleKappa : MonoBehaviour {

    public GameObject PurPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.localScale.x < 5)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.1f,transform.localScale.y + 0.1f,transform.localScale.z);
            transform.position += (PurPos.transform.position - transform.position) * 0.05f;

        }
		
	}
}
