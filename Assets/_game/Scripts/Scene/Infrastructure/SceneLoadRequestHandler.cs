using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadRequestHandler : MonoBehaviour {

	GameManager gameManager;

	public void RequestSceneLoad(string sceneName) {
		if (gameManager == null)
			gameManager = GameManager.instance;
		gameManager.LoadScene(sceneName);
	}
}
