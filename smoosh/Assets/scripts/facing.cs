using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facing : MonoBehaviour {

    //test object only - tells if player is facing right or left

    public bool facingr;
    public GameObject player1;
    public Transform pos;
    Player1 p1;

    // Use this for initialization
    void Start () {
        facingr = true;
        pos = GetComponent<Transform>();
        
        facingr = p1.facingr1;
    }
	
	// Update is called once per frame
	void Update () {
        p1 = player1.GetComponent<Player1>();
        facingr = p1.facingr1;

        if (facingr)
        {
            pos.position = player1.transform.position + Vector3.right;
        } else
        {
            pos.position = player1.transform.position - Vector3.right;
        }
	}
}
