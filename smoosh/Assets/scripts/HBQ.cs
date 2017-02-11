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
            //Debug.Log("Yo it's in!", gameObject);
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
   
    public void DeQ (int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction, 
        int playerNum, Vector3 angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb)
    {
        //Debug.Log("1", gameObject);
        currentHB = hitboxes[0]; //refactor potential
        //Debug.Log("2", gameObject);
        hitboxes.Remove(hitboxes[0]);
        //Debug.Log("3", gameObject);
        currentHB.activeOn = activeOn;
        currentHB.size = size;
        currentHB.duration = duration;
        currentHB.location = location;
        currentHB.tethered = tethered;
        currentHB.direction = direction;

        currentHB.playerNum = playerNum;
        currentHB.angle = angle;
        currentHB.dmg = dmg;
        currentHB.sdmg = sdmg;
        currentHB.grab = grab;
        currentHB.priority = priority;
        currentHB.bkb = bkb;
        currentHB.skb = skb;


        //assign paramters
    }
    //DeQ parameter list:
    //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
    //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb

}
