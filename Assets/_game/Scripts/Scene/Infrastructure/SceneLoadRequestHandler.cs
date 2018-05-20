using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadRequestHandler : MonoBehaviour {

	SceneTransitionManager sceneTransitionManager;

	public void RequestSceneLoad(string sceneName) {
		if (sceneTransitionManager == null)
			sceneTransitionManager = SceneTransitionManager.instance;
		sceneTransitionManager.LoadScene(sceneName);
	}
}
