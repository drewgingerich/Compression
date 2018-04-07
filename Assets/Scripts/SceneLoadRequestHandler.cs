using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadRequestHandler : MonoBehaviour {

	GameManager gameManager;

	void Awake() {
		gameManager = GameManager.instance;
		if (gameManager == null)
			StartCoroutine(LoadBaseScene());
	}

	IEnumerator LoadBaseScene () {
		yield return SceneManager.LoadSceneAsync("Base", LoadSceneMode.Additive);
		gameManager = GameManager.instance;
	}

	public void RequestSceneLoad(string sceneName) {
		if (gameManager != null)
			gameManager.LoadScene(sceneName);
	}
}
