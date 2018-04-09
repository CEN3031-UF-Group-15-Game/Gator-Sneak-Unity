using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
	
	public KeyCode HideHotkey;
	public bool DebugMode;
	
    private Canvas CanvasObject;
	private Text ScoreText;
	private Text TimerText;
	private Text GamemodeText;

	public void updateScoreValue(int score) {
		ScoreText.text = score.ToString();
	}
	public void updateTimerSecondsRemaining(int seconds) {
		int min = (int)(seconds / 60);
		int sec = seconds % 60;
		updateTimerValue(min.ToString() + ":" + sec.ToString("00"));
	}
	public void updateGamemodeString(string gamemodeStr) {
		GamemodeText.text = gamemodeStr;
	}

	// Will set the timer value to (time) exactly
	public void updateTimerValue(string time) {
		TimerText.text = time;
	}
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
		ScoreText = GameObject.Find("Score Value").GetComponent<Text>();
		TimerText = GameObject.Find("Timer Value").GetComponent<Text>();
		GamemodeText = GameObject.Find("Gamemode").GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(HideHotkey)) {
            toggleView();
        }
		if (DebugMode) {
			HUDDebug();
		}
	}

	/**
	Hold F to increase score
	Press G to set score to number of seconds remaining in the timer
	 */
	private void HUDDebug() {
		if (Input.GetKey(KeyCode.F)) {
			updateScoreValue(int.Parse(ScoreText.text) + 1);
		}
		if (Input.GetKeyDown(KeyCode.G)) {
			updateTimerSecondsRemaining(int.Parse(ScoreText.text) + 1);
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			if (int.Parse(ScoreText.text) % 2 == 1) {
				updateGamemodeString("ODD");
			} else {
				updateGamemodeString("EVEN");
			}

		}
	}

}
