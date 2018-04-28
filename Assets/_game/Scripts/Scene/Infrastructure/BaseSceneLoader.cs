using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneLoader : MonoBehaviour {

	string baseSceneName = "Base";

	void Awake() {
		Scene baseScene = SceneManager.GetSceneByName(baseSceneName);
		if (!baseScene.IsValid())
			SceneManager.LoadScene("Base", LoadSceneMode.Additive);
	}
}
