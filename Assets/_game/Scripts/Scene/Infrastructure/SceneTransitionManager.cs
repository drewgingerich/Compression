using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour {

	public static SceneTransitionManager instance;
	[SerializeField] string startingSceneName;
	string baseSceneName = "Base";

	void Awake() {
		if (instance == null)
			instance = this;
		else
			Debug.LogError("Multiple GameManager instances detected.");
	}

	public void LoadScene(string sceneName) {
		if (string.IsNullOrEmpty(sceneName)) {
			Debug.LogError("No scene name given.");
			return;
		}
		for (int i = 0; i < SceneManager.sceneCount; i++) {
			Scene scene = SceneManager.GetSceneAt(i);
			if (scene.name != baseSceneName)
				StartCoroutine(UnloadSceneRoutine(scene.name));
		}
		StartCoroutine(LoadSceneRoutine(sceneName));
	}

	IEnumerator UnloadSceneRoutine(string sceneName) {
		yield return SceneManager.UnloadSceneAsync(sceneName);
	}

	IEnumerator LoadSceneRoutine(string sceneName) {
		yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		Scene loadedScene = SceneManager.GetSceneByName(sceneName);
		if (loadedScene.IsValid())
			SceneManager.SetActiveScene(loadedScene);
		else
			Debug.LogError(string.Format("Loaded scene {0} is not valid.", sceneName));
	}
}
