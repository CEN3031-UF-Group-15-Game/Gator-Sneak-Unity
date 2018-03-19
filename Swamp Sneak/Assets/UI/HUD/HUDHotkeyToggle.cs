using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHotkeyToggle : MonoBehaviour {

    public KeyCode Hotkey;

    private Canvas CanvasObject;

    private void Start()
    {
        CanvasObject = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(Hotkey))
        {
            CanvasObject.enabled = !CanvasObject.enabled;
        }
	}
}
