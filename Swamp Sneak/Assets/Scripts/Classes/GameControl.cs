using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    //Public Constants and Variables
    public const int LOADINGSCENE = 0;

    //Private Variables
    private static bool isAwake = false;
    private bool isCurScnEditable = true;
    private bool loadScn = false;

    private string currentScene, prevScene;

    public string GetCurrentScene()
    {
        return currentScene;
    }

    public void SetCurrentScene(string value)
    {
        if(isCurScnEditable)
            currentScene = value;
        loadScn = true;
    }


    //Awake is call 0
    private void Awake()
    {
#pragma warning disable CS0665 // Assignment in conditional expression is always constant
        if (isAwake = true)
#pragma warning restore CS0665 // Assignment in conditional expression is always constant
        {
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("The " + this.gameObject.ToString() + " is Awake and Will Not Be Destroyed");
        }
    }

    //OnEnable is Call 1
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

    //OnSceneChanged is Call 3
    void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        prevScene = currentScene;
        currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("New Scene Loaded:" + scene.name);

        //Things that happen once at the beginning of scene load go here
        switch (currentScene)
        {
            case "LoadingScreen": //Scene 0 Should Be Mapped to Loading Scene
                Debug.Log("GameControl is in Loading Screen\n");
                break;
            case "DEMO SCENE":
                Debug.Log("GameControl is in DemoScene 0\n");
                /*
                 * Put SetUp Code Here
                 */
                break;
            default:
                //Do Nothing.
                break;
        }
    }

    //Start is call 2
    // Use this for initialization
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;

        //Goto Start
        SceneManager.LoadScene("start");
    }

    /*FixedUpdate is call 4
    void FixedUpdate()
    {

    }
    */

    // Update is call 5
    void Update () {
        manageScene();
	}

    // Scene Management Goes Here
    private void manageScene()
    {
        switch (currentScene)
        {
            case "LoadingScene":
                Debug.Log("GameControl Loaded");
                break;
            case "start":
                Debug.Log("In Start");
                break;
            default:
                Debug.Log("Unknown Scene");
                break;
        }
    }
}
