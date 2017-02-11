using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HBQ : MonoBehaviour {

    public GameObject[] gethitboxes;
    public List<hitbox> hitboxes;
    hitbox currentHB;

    // Hitbox Manager/Controller keeps a list of hitboxes
    void Start () {
        //-Ian instantiate hitbox list
        gethitboxes = GameObject.FindGameObjectsWithTag("hitbox");
        hitboxes.Clear();
        foreach (GameObject h in gethitboxes)
        {
            hitboxes.Add(h.GetComponent<hitbox>());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnQ (hitbox h) //once a move is copmleted, a hitbox is added to the unused hitbox list
    {
        hitboxes.Add(h);
    }

    public void DeQ () //give the first entry on the hitbox list the desired properties and remove it from the list of unused hitboxes
    {
        currentHB = hitboxes[1]; //refactor potential
        hitboxes.Remove(hitboxes[1]);
        //assign paramters
    }

}
