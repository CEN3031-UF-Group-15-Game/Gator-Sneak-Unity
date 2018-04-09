using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour {
	
	public KeyCode HideHotkey;
	
    private Canvas CanvasObject;

	// Hide the canvas
	public void hide() {
		CanvasObject.enabled = false;
	}
	// Show the canvas
	public void show() {
		CanvasObject.enabled = true;
	}
	// Toggle hud view
	public void toggleView() {
		CanvasObject.enabled = !CanvasObject.enabled;
	}

	void Awake() {
        CanvasObject = GetComponent<Canvas>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(HideHotkey))
        {
            toggleView();
        }
	}

}
