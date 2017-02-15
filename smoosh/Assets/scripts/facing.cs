using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facing : MonoBehaviour {

    //test object only - tells if player is facing right or left

    public bool facingr;
    public GameObject player1;
    public Transform pos;
    ChallengerCon p1;

    // Use this for initialization
    void Start () {
        facingr = true;
        pos = GetComponent<Transform>();

        p1 = player1.GetComponent<ChallengerCon>();
        facingr = p1.facingr1;
    }
	
	// Update is called once per frame
	void Update () {
       
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
