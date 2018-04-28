using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadRequestHandler : MonoBehaviour {

	GameManager gameManager;
	float timeout = 1f;

	public void RequestSceneLoad(string sceneName) {
		if (gameManager == null)
			gameManager = GameManager.instance;
		gameManager.LoadScene(sceneName);
	}
}
