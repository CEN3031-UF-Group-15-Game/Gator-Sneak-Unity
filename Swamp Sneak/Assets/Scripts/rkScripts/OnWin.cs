using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWin : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void OnTriggerEnter(Collider obj)
    {
        if(obj.tag.CompareTo("Player")==0)
        {

            Debug.Log("YOU WIN");

            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().gameWin = true;
        } else
        {
            Debug.Log("collided with win box " + obj.tag);
        }
    }
    public void OnTriggerExit()
    {

    }
}
