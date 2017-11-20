using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ButtonControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(450, 800, Screen.fullScreen);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnClick(int button)
    {
        switch (button)
        {

            case 0:
                SceneManager.LoadScene("game");
                break;
        }
    }

}
