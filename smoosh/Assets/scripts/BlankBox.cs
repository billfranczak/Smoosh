using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlankBox : MonoBehaviour {

    ChallengerCon player;

    public Action<int> act;

    public BlankBox (ChallengerCon chal)
    {
        player = chal;
    }

	// Use this for initialization
	void Start () {
        act = blank;
	}
	
	// Update is called once per frame
	void Update () {
        act(0);
	}

    void blank (int i)
    {

    }

}
