using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Controller : MonoBehaviour
{
    // Universal Controller Variable Declarations
    protected static bool isAwake = false;
 
    // Use this initialing the controller
    //  NOTE: controllers should be initialized in a Scene that only occurs once unless the 
    //         previous controller has been destroyed.
    //      Only Managers can destroy other Managers
    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if(isAwake = true) {
            Debug.Log("The " + this.gameObject + "is Awake.");

            DontDestroyOnLoad(this.gameObject);
            Debug.Log("I Will Not Be Destroyed.");
        }
    }

    // Use Classes Override this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    // Interprocess Communication Functions
    public static bool GetisAwake()
    {
        return isAwake;
    }
}
