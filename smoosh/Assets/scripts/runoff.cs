using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runoff : MonoBehaviour {

    public Renderer ro;

    // Use this for initialization
    void Start () {

        ro= GetComponent<Renderer>();
        ro.material.color = Color.black;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
