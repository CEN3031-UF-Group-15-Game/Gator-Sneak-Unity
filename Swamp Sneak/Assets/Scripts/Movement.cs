using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float movementSpeed = 1000;

    void Update()
    {

        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

    }

}
