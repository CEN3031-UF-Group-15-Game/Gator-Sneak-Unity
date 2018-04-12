using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

	protected GameControl control;

    protected virtual void Awake() {
        Init();
    }

    protected virtual void Start() {
        
    }

    protected virtual void Init() {

    }
}
