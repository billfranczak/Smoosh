using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HBQ : MonoBehaviour {

    public int hbNum;
    public GameObject pcObj;
    public List<GameObject> hbObjList;
    public List<hitbox> hbCompList;
    hitbox currentHB;

    // Hitbox Manager/Controller keeps a list of hitboxes
    void Start()
    {

        for (int i = 0; i < hbNum; i++)
        {
            //instantiate a sphere
            hbObjList.Add (GameObject.CreatePrimitive(PrimitiveType.Sphere));
            //add hitbox Component to that sphere and to our hitbox component list
            hbCompList.Add (hbObjList[i].AddComponent<hitbox>());
            hbCompList[i].player = pcObj;
            hbCompList[i].queue = gameObject;
            hbObjList[i].GetComponent<SphereCollider>().isTrigger = true;
            hbObjList[i].tag = "hitbox";
            hbObjList[i].transform.position = transform.position;
        }

        //DEPROCATED\\
        //    gethitboxes = GameObject.FindGameObjectsWithTag("hitbox");
        //    hitboxes.Clear();
        //    foreach (GameObject h in gethitboxes)
        //    {
        //        //Debug.Log("Yo it's in!", gameObject);
        //        hitboxes.Add(h.GetComponent<hitbox>());
        //    }
        //}
    }
            // Update is called once per frame
            void Update () {
		
	}
    //is this a utility script called by our player to add the hitbox back or called by the hitbox??? - Ian
   
    public void EnQ (hitbox h) //once a move is copmleted, a hitbox is added to the unused hitbox list
    {
        hbCompList.Add(h);
    }


    //down the road hitbox que will likely need to function on a few different levels
    //if we are pasing our hitbox around durring the trigger there is less of a reason for this kind of top down manager
    //at that point hbq would be seperated into two parts,
    //  --a per move interface that gives us the max number of hitboxes at once for that move IAttackWithHitboxes
    //  --a per fighter hbq that determines the max number of hitboxes that fighter could need at any given moment, (based on information from the IAttackWithHitboxes interface)
    //    instantiates, and keep track of those hitboxes

    //who is calling this? currently the player? more speciically the attack is calling this
    //eventually this will need to take into account animation, most likely parenting the hitbox under a specific joint (even if that joint is only to animate attacks)
    //persistent hitboxes?
    public void DeQ (int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction, 
        int playerNum, Vector3 angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb)
    {
        //Debug.Log("1", gameObject);
        currentHB = hbCompList[0]; //refactor potential

        //Question??? if we take the 0 index out of a list does that make the 1 index roll down to 0???? O>o

        //Debug.Log("2", gameObject);
        hbCompList.Remove(hbCompList[0]);
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
