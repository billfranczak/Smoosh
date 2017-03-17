using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class hitboxNet : NetworkBehaviour
{

    
    public GameObject player;
    public GameObject queue;
    public ChallengerConNet p;
    [SyncVar]
    public Transform pos;
    public Renderer hbrend;

    //properties of a hitbox used imminently
    [SyncVar]
    public bool active; //turns on hitbox
    [SyncVar]
    public int activeOn; //first frame hitbox becomes active -- !!!!move activeOn to player, to prevent attacks from happening if a player is interupted!!!!
    [SyncVar]
    public float size;
    [SyncVar]
    public int duration;
    [SyncVar]
    public Vector3 location;
    [SyncVar]
    public bool tethered; //punch vs missile
    [SyncVar]
    public Vector3 direction; // hitbox listens for updates to this

    //may just use direct manipulation of 'direction' to do this
    public bool grav; // is the projectile affected by gravity
    public float floaty; //how gravity affects it

    //properties that are only important on contact
    [SyncVar]
    public int playerNum; //who spawned the hitbox
    [SyncVar]
    public Vector3 angle; //angle the victom is sent off at
    [SyncVar]
    public int dmg;
    [SyncVar]
    public int sdmg; //sheild dmg
    [SyncVar]
    public bool grab; //grabs >> shields
    [SyncVar]
    public int priority;  //The raiting at wich a move will overide another simulataneously cast and overlapping move
    [SyncVar]
    public float bkb; //base knockback
    [SyncVar]
    public float skb; //scaling knockback
    public int atkDir;

    public bool clanked; //tells if another move clanked the current hitbox 

    public string special; //used for weird moves, hitbox listens for updates to this

    public int offset; //offsets hitboxes in space

    //pass-on traits - spawn a new hitbox when the current one finishes

    // Use this for initialization
    void Start()
    {
        hbrend = GetComponent<Renderer>();

        special = "";
        clanked = false;
        active = false;
        duration = 0;
        pos = GetComponent<Transform>();
        size = 0.3f;

        hbrend.material.color = Color.yellow;

    }

    // Update is called once per frame
    void Update()
    {

        if (duration > 0)
        {
            duration--;
            //Debug.Log("Yo it's in!", gameObject);
            if (activeOn > 0)
            {
                activeOn--;
                if (activeOn == 0)
                {
                    active = true;
                    pos.position = p.transform.position + location;
                    hbrend.material.color = Color.red;
                }
            }

            //pos.position = size; rescale the hitbox
            pos.transform.localScale = new Vector3(size, size, size);
            if (tethered)
            {
                pos.position = p.transform.position + location;

            }
            else
            {
                pos.position = pos.position + direction;
            }

            //return to offscreen position
            if (duration == 0 || clanked)
            {
                pos.position = new Vector3(-10, 5 + 5 * offset, 3);
                hbrend.material.color = Color.yellow;
                clanked = false;
                active = false;
                //push into p's queue
                p.EnQ(this);
            }

        }



    }
}
