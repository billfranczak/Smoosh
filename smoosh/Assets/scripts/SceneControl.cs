using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {
    public int frameRate = 60;
	// Use this for initialization
	void Awake () {
        Application.targetFrameRate = frameRate;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
