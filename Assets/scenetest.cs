using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenetest : MonoBehaviour {

    public bool btest = false;

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {

        if (btest == true)
        {
            Transitioner.Instance.TransitionToScene("Scene");
        }

	}
}
