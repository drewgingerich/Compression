using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneLoader : MonoBehaviour {

	public event System.Action OnBaseSceneLoaded = delegate { };
	string baseSceneName = "Base";

	void Awake() {
		Scene baseScene = SceneManager.GetSceneByName(baseSceneName);
		if (baseScene.IsValid())
			StartCoroutine(LoadBaseScene());
		else
			OnBaseSceneLoaded();
	}

	IEnumerator LoadBaseScene() {
		yield return SceneManager.LoadSceneAsync("Base", LoadSceneMode.Additive);
		OnBaseSceneLoaded();
	}
}
