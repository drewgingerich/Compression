using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	void Start () {
		StartCoroutine (LoadMenu ());
	}

	IEnumerator LoadMenu () {
		yield return SceneManager.LoadSceneAsync ("Menu", LoadSceneMode.Additive);
	}

	IEnumerator UnloadMenu () {
		yield return SceneManager.UnloadSceneAsync ("Menu");
	}
	
	IEnumerator LoadPlay () {
		yield return SceneManager.LoadSceneAsync ("Play", LoadSceneMode.Additive);
	}

	IEnumerator UnloadPlay () {
		yield return SceneManager.UnloadSceneAsync ("Menu");
	}
}
