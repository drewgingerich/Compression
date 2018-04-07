using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	[SerializeField] string startingSceneName;
	string currentSceneName;
	string baseSceneName = "Base";

	void Awake() {
		if (instance == null)
			instance = this;
		else
			Debug.LogError("Multiple GameManager instances detected.");
	}

	// void Start() {
	// 	LoadScene(startingSceneName);
	// }

	public void SetCurrentSceneName(string name) {
		currentSceneName = name;
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
	}
}
