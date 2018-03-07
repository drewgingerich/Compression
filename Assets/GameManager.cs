// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class GameManager : MonoBehaviour {

// 	MenuManager menuManager;
// 	PlayManager playManager;

// 	void Start () {
// 		StartCoroutine (LoadMenu ());
// 	}

// 	void StartPlay () {
// 		StartCoroutine (UnloadMenu ());
// 		StartCoroutine (LoadPlay ());
// 	}

// 	void EndPlay () {
// 		StartCoroutine (UnloadPlay ());
// 		StartCoroutine (LoadMenu ());
// 	}

// 	IEnumerator LoadMenu () {
// 		yield return SceneManager.LoadSceneAsync ("Menu", LoadSceneMode.Additive);
// 		menuManager = GameObject.FindGameObjectWithTag ("menuManager").GetComponent<MenuManager> ();
// 		menuManager.OnStart += StartPlay;
// 	}

// 	IEnumerator UnloadMenu () {
// 		yield return SceneManager.UnloadSceneAsync ("Menu");
// 	}
	
// 	IEnumerator LoadPlay () {
// 		yield return SceneManager.LoadSceneAsync ("Play", LoadSceneMode.Additive);
// 		playManager = GameObject.FindGameObjectWithTag ("playManager").GetComponent<PlayManager> ();
// 		playManager.OnEndPlay += EndPlay;
// 	}

// 	IEnumerator UnloadPlay () {
// 		yield return SceneManager.UnloadSceneAsync ("Play");
// 	}
// }
