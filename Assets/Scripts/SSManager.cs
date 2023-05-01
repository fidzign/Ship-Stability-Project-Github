using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SSManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ExitApp()
    {
		Application.Quit();
    }

	public void ReloadApp()
    {
		SceneManager.LoadScene(0);
    }
}
