using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour {

	[SerializeField] GameObject startingMenu;
	GameObject currentMenu;

	public void LoadMenu(GameObject menu) {
		currentMenu.SetActive(false);
		currentMenu = menu;
		menu.SetActive(true);
	}

	void Start() {
		currentMenu = startingMenu;
		LoadMenu(startingMenu);
	}
}
