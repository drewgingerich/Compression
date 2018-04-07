using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	[SerializeField] string startingSceneName;
	string currentSceneName;

	void Awake() {
		if (instance == null)
			instance = this;
		else
			Debug.LogError("Multiple GameManager instances detected.");
	}

	void Start() {
		LoadScene(startingSceneName);
	}

	public void LoadScene(string sceneName) {
		if (!string.IsNullOrEmpty(currentSceneName))
			StartCoroutine(UnloadCurrentSceneRoutine());
		StartCoroutine(LoadSceneRoutine(sceneName));
		currentSceneName = sceneName;
	}

	IEnumerator LoadSceneRoutine(string sceneName) {
		yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
	}

	IEnumerator UnloadCurrentSceneRoutine() {
		yield return SceneManager.UnloadSceneAsync(currentSceneName);
	}
}
