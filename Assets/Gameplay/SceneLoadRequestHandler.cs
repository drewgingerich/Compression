using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadRequestHandler : MonoBehaviour {

	GameManager gameManager;

	void Awake() {
		gameManager = GameManager.instance;
	}

	public void RequestSceneLoad(string sceneName) {
		gameManager.LoadScene(sceneName);
	}
}
