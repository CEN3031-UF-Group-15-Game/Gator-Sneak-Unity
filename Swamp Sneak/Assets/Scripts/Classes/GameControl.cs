using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : Controller {
    //Constants and Public Variables
    public const int LOADINGSCENE = 0;
    public const int STARTSCENE = 1;
    public const int DEMOSCENE = 2;

    //Protected and Private Variables
    private int currentScene, lastScene, nextScene;

    // Use this for initialization
    protected override void Start ()
    {
        currentScene = 0;
        SceneManager.LoadScene(STARTSCENE);
        nextScene = -1;
    }

    private void OnEnable()
    {
        Debug.Log("GameControl onEnable Called");
        SceneManager.sceneLoaded += OnSceneChanged;
    }
    private void OnDisable()
    {
        Debug.Log("GameControl onDisable Called");
        SceneManager.sceneLoaded -= OnSceneChanged;
    }

    void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        lastScene = currentScene;
        Debug.Log("New Scene Loaded:" + scene.name);
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    protected override void Update()
    {
        switch (currentScene)
        {
            case 0: //Scene 0 Should Be Mapped to Loading Scene
                Debug.Log("Stuck at LoadingScene?");
                break;
            case 1: //Scene 1 Should Be Mapped to Start Scene

                break;
            case DEMOSCENE:
                Debug.Log("GameControl is in StartScene\n");
                break;
            default:
                //Do Nothing.
                break;
        }
    }
}
