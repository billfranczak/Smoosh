using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShieldScriptNet : NetworkBehaviour {

    public ChallengerConNet p;
    public bool shieldIsUp;
    public int poffset;
    public float sx;
    public float sy;
    public Renderer shRend;
    // Use this for initialization
    void Start () {
        shieldIsUp = false;
        shRend = GetComponent<Renderer>();
        shRend.material.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
		if (shieldIsUp)
        {
            transform.position = new Vector3(sx,sy,0);
        }
        else
        {
            transform.position = new Vector3(-20, -10, 5 * poffset);
        }
	}
}
