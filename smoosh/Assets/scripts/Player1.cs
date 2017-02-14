using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

    public GameObject hbqueue;
    HBQ hbq;

    hitbox currentHB;

    public Vector3 pos;
    public Rigidbody p1;  //this could be named better
    public Renderer p1rend;
    public Transform temptrans;
    public int thisPlayer;
    public string p1s;
    public float wgt;
    public float walkaccel;
    public float walkspd;
    public float crawlspd;
    public float runaccel;
    public float runspd;
    public float dashl;
    //public float airspd;
    public float airMaxSpeed;
    public float airAccel;
    public float flty;
    public float fallSpdMax;
    public float fallAccel;
    public float roll;
    public float gjumphgt;
    public float ajumphgt;
    public int jumps;
    public int maxjumps;
    public int jumpcd;
    public int jumpmaxcd;


    public int dashwindow;
    public int dashToRun;
    public int dashtimer;
    public int dtrtimer;
    public int standuptimer;
    public int crawlToStand;
    public bool okcrawl;

    public int shieldup;
    public int shieldtimer;
    public bool inshield;
    public int shieldhealth;
    public int maxshield;
    public int shieldstun;

    public bool land;
    public int oneframe; 
    public bool runoff;
    public bool facingr1;

    public bool isHit;
    public int hitstun;
    public int hitstuntimer;
    public int dmg;

    public int lrunofftimer;
    public int rrunofftimer;
    public int maxrunofftimer;
    public bool lrunoffstall;
    public bool rrunoffstall;
    public int landOrRegrabtimer;
    public int maxLandRegrab;

    public float capsuleVOffset;


    public int lag;

    RaycastHit hit;

    // Use this for initialization
    void Start () {
        int dmg = 0;
        thisPlayer = 1;
		p1 = GetComponent<Rigidbody>();
        p1rend= GetComponent<Renderer>();

        p1rend.material.color = Color.yellow;
        p1s = "walk";

        isHit = false;
        walkaccel = 30;
        walkspd = 3;
        runaccel = 60;
        runspd = 8;
        crawlspd = 8;

        airMaxSpeed = 3;
        airAccel = 30;
        fallSpdMax = 3;
        fallAccel = 20;
        gjumphgt = 250;
        ajumphgt = 350;
        jumpmaxcd = 10;
        maxjumps = 2;
        jumps = 2;

        dashwindow = 7;
        dashtimer=0;
        dashl = 350;
        dashToRun = 7;

        crawlToStand = 3;
        standuptimer = 0;
        okcrawl = false;

        shieldup = 3;
        shieldtimer = 0;
        inshield = false;
        maxshield = 100;
        shieldhealth = maxshield;
        shieldstun = 0;

        land = false;
        oneframe = 0;
        runoff = false;
        facingr1 = true;

        lag = 0;

        hitstun = 0;
        hitstuntimer = 0;


        maxrunofftimer = 5;
        //lrunofftimer = maxrunofftimer;
        //rrunofftimer = maxrunofftimer;
        lrunofftimer = 0;
        rrunofftimer = 0;
        lrunoffstall = false;
        rrunoffstall = false;
        landOrRegrabtimer = 0;
        maxLandRegrab = 10;

        capsuleVOffset = .31f;
    }
	
	// Update is called once per frame
	void Update () {
        pos = transform.position;
        
        //player states are colors:
        //walk - yellow
        //dash - black
        //run - grey
        //airborn - cyan
        //attacking - green
        //shield - blue
        //dodge - magenta
        //hitstun - red
        //ledge - black
        //freeze - black
        //crawl - magenta
        //

        if (isHit)
        {
            p1s = "hitstun";
        }

        //for some reason, capsule is not triggering "onTriggerStay". This is a temp solution
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, capsuleVOffset) ) {
            if (hit.collider.tag == "lrunoff" && (p1s=="walk" || p1s == "run" || p1s == "dash" ))
            {
                //Debug.Log("stay");
                if (lrunofftimer > 0)
                {
                    lrunofftimer--;

                }

            }
            if (hit.collider.tag == "rrunoff")
            {
                if (rrunofftimer > 0)
                {
                    rrunofftimer--;
                }
            }
        }
        if (landOrRegrabtimer>0)
        {
            landOrRegrabtimer--;
        }
       

        switch (p1s)
        {
            case "walk":
                //Color Debug State
                p1rend.material.color = Color.yellow;

                //-Ian to Bill -- what is this check doing, it looks like it is looking
                //at wether or not we are dashing to see if we can crouch??? is crouch still in?

                //-Bill to Ian -- yea, that's what it does
                if (dashtimer > 0 )
                {
                    okcrawl = false;
                    dashtimer--;
                    if (dashtimer == 0)
                    {
                        okcrawl = true;
                    }
                }

                if (runoff)
                {
                    //Debug.Log("walk - runoff");
                    p1s = "airborn";
                    jumps--;
                    land = false;
                    runoff = false;
                    dashtimer = 0;
                    //impart maxwalkspeed in direction we are facing
                }

                //input
                if (UnityEngine.Input.GetKey(KeyCode.A))
                {
                    facingr1 = false;
                    if (dashtimer > 0)
                    {
                        p1s = "dash";
                        p1.AddForce(-transform.right * dashl);
                        dtrtimer = dashToRun;
                    }
                    else
                    {
                        if (p1.velocity.x > -walkspd && !(lrunoffstall && lrunofftimer>0))
                        {
                            p1.AddForce(-transform.right * walkaccel);
                        }
                        if (lrunofftimer == 0 && lrunoffstall == true)
                        {
                            runoff = true;
                            landOrRegrabtimer = maxLandRegrab;
                        }
                    }
                }

                if (UnityEngine.Input.GetKey(KeyCode.D))
                {
                    facingr1 = true;
                    if (dashtimer > 0) {
                        p1s = "dash";
                        p1.AddForce(transform.right * dashl);
                        dtrtimer = dashToRun;
                    }
                    else
                    {
                        
                        if (p1.velocity.x < walkspd && !(rrunoffstall && rrunofftimer > 0))
                        {
                            p1.AddForce(transform.right * walkaccel);
                        }
                        if (rrunofftimer == 0 && rrunoffstall == true)
                        {
                            runoff = true;
                            landOrRegrabtimer = maxLandRegrab;
                        }
                    }
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.W) && jumps>0)
                {
                    p1.AddForce(transform.up * gjumphgt);
                    p1s="airborn";
                    jumpcd = jumpmaxcd;
                    jumps--;
                    land = false;
                    //Debug.Log("walk jump");
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.S))
                {
                    dashtimer = dashwindow;
                }

                //doubletap dash
                /*
                if (UnityEngine.Input.GetKeyDown(KeyCode.D))
                {
                    dashtimer = dashwindow;
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.A))
                {
                    dashtimer = dashwindow;
                }
                */
                
                if (UnityEngine.Input.GetKey(KeyCode.S) && dashtimer == 0)
                {
                    standuptimer = crawlToStand;
                    p1s = "crawl";
                } 

                if (UnityEngine.Input.GetKey(KeyCode.F) && shieldhealth >0)
                {
                    p1s = "shield";
                    dashtimer = 0;
                    shieldtimer = 3;
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.Space)) //check cooldown, charges
                {
                    p1s = "attacking";
                    lag = 60;
                    hbq = hbqueue.GetComponent<HBQ>();
                    hbq.DeQ(15, .2f,55,Vector3.right, true, Vector3.zero,
                        1,Vector3.up,5,5,false,0,50,10);
                    //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
                    //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb
                }

                if (UnityEngine.Input.GetMouseButtonDown(1))
                {
                    p1s = "attacking";
                    lag = 10;
                    hbq = hbqueue.GetComponent<HBQ>();
                    hbq.DeQ(15, .5f, 200, Vector3.right, false, Vector3.zero,
                        2, Vector3.up, 10, 0, false, 0, 150, 10);
                    //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
                    //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb
                }


                break;



            case "dash":
                p1rend.material.color = Color.black;
                if (dtrtimer > 0)
                {
                    dtrtimer--;
                }
                else
                {
                    p1s = "walk";
                    if (UnityEngine.Input.GetKey(KeyCode.A)) {
                        p1s = "run";
                    }
                    if (UnityEngine.Input.GetKey(KeyCode.D)) {
                        p1s = "run";
                    }

                    if (runoff)
                    {
                        
                        p1s = "airborn";
                        jumps--;
                        land = false;
                        runoff = false;
                        dashtimer = 0;
                    }

                }
             

                break;



            case "run":
                p1rend.material.color = Color.grey;

                if (UnityEngine.Input.GetKeyDown(KeyCode.W) && jumps > 0)
                {
                    p1.AddForce(transform.up * gjumphgt);
                    p1s = "airborn";
                    jumpcd = jumpmaxcd;
                    jumps--;
                    land = false;
                    dashtimer = 0;
                } else
                if (UnityEngine.Input.GetKey(KeyCode.A)) {
                    facingr1 = false;
                    if (p1.velocity.x > -runspd && !(lrunoffstall && lrunofftimer > 0))
                    {
                        p1.AddForce(-transform.right * runaccel);
                    }

                    if (lrunofftimer == 0 && lrunoffstall == true)
                    {
                        runoff = true;
                        landOrRegrabtimer = maxLandRegrab;
                    }
                }
                else if (UnityEngine.Input.GetKey(KeyCode.D)) {
                    facingr1 = true;
                    if (p1.velocity.x < runspd && !(rrunoffstall && lrunofftimer > 0))
                    {
                        p1.AddForce(transform.right * runaccel);
                    }

                    if (lrunofftimer == 0 && rrunoffstall == true)
                    {
                        runoff = true;
                        landOrRegrabtimer = maxLandRegrab;
                    }
                } 
                else
                {
                    p1s = "walk";
                }

                if (runoff)
                {
                    p1s = "airborn";
                    jumps--;
                    land = false;
                    runoff = false;
                    dashtimer = 0;
                    //impart maxrunspeed in direction we are facing
                }

                break;



            case "airborn":
                p1rend.material.color = Color.cyan;

                if (jumpcd >0)
                {
                    jumpcd--;
                }

                if (UnityEngine.Input.GetKey(KeyCode.A))
                {
                    //p1.AddForce(-transform.right * airspd);
                    if (p1.velocity.x > -airMaxSpeed)
                    {
                        p1.AddForce(-transform.right * airAccel);
                    }
                }

                if (UnityEngine.Input.GetKey(KeyCode.D))
                {
                    if (p1.velocity.x < airMaxSpeed)
                    {
                        p1.AddForce(transform.right * walkaccel);
                    }
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.W) && jumpcd==0 && jumps >0)
                {
                    if (p1.velocity.y < 0)
                    {

                        p1.velocity = new Vector3 (p1.velocity.x,0,0);
                    }
                    p1.AddForce(transform.up * ajumphgt);    
                    
                    jumps--;
                }
                if (UnityEngine.Input.GetKey(KeyCode.S))
                {
                    if (p1.velocity.y > -fallSpdMax)
                    {
                        p1.AddForce(-transform.up * fallAccel);
                    }
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                {
                    p1s = "attacking";
                    lag = 60;
                }


                if (land)
                {
                    //Debug.Log("hi");
                    p1s = "walk";
                    jumps = maxjumps;
                    land = false;
                }
                
                break;



            case "attacking":
                p1rend.material.color = Color.green;

                if (lag >0)
                {
                    lag--;
                    if (Physics.Raycast(transform.position,-Vector3.up,out hit, capsuleVOffset ))
                    {
                        if (hit.collider.tag == "stage")
                        {
                            if (lag==0)
                            {
                                p1s = "walk";
                                land = false;
                                runoff = false;
                                dashtimer = 0;
                                shieldtimer = 0;
                                dtrtimer = 0;
                                okcrawl = false;
                                standuptimer = 0;

                                jumps = maxjumps;
                            }
                        } else
                        {
                           
                            if (UnityEngine.Input.GetKey(KeyCode.A))
                            {
                                if (p1.velocity.x > -airMaxSpeed)
                                {
                                    p1.AddForce(-transform.right * airAccel);
                                };
                            }

                            if (UnityEngine.Input.GetKey(KeyCode.D))
                            {
                                if (p1.velocity.x < airMaxSpeed)
                                {
                                    p1.AddForce(transform.right * walkaccel);
                                }
                            }

                            if (UnityEngine.Input.GetKey(KeyCode.S))
                            {
                                if (p1.velocity.y > -fallSpdMax)
                                {
                                    p1.AddForce(-transform.up * fallAccel);
                                }
                            }


                            if (lag == 0)
                            {
                                p1s = "airborn";

                                //land = false;
                                runoff = false;
                                dashtimer = 0;
                                shieldtimer = 0;
                                lag = 0;
                                dtrtimer = 0;
                                okcrawl = false;
                                standuptimer = 0;
                            }
                        }
                    } else
                    {
                        if (UnityEngine.Input.GetKey(KeyCode.A))
                        {
                            if (p1.velocity.x > -airMaxSpeed)
                            {
                                p1.AddForce(-transform.right * airAccel);
                            }
                        }

                        if (UnityEngine.Input.GetKey(KeyCode.D))
                        {
                            if (p1.velocity.x < airMaxSpeed)
                            {
                                p1.AddForce(transform.right * walkaccel);
                            }
                        }

                        if (UnityEngine.Input.GetKey(KeyCode.S))
                        {
                            if (p1.velocity.y > -fallSpdMax)
                            {
                                p1.AddForce(-transform.up * fallAccel);
                            }
                        }

                        if (lag == 0)
                        {
                            p1s = "airborn";

                            //land = false;
                            runoff = false;
                            dashtimer = 0;
                            shieldtimer = 0;
                            lag = 0;
                            dtrtimer = 0;
                            okcrawl = false;
                            standuptimer = 0;
                        }
                    }
                }
                


                break;



            case "shield":
                p1rend.material.color = Color.blue;

                if (shieldstun > 0)
                {
                    shieldstun--;
                }

                if (shieldtimer > 0)
                {
                    shieldtimer--;
                    if (shieldtimer == 0)
                    {
                        inshield = true;
                        shieldstun = 4;
                    }
                }

                if (UnityEngine.Input.GetKey(KeyCode.F) == false)
                {
                    inshield = false;
                }

                if (UnityEngine.Input.GetKey(KeyCode.F)==false && shieldstun ==0)
                {
                    shieldtimer = 0;
                    p1s = "walk";
                }


                break;



            case "hitstun":
                p1rend.material.color = Color.red;
                if (hitstuntimer > 0)
                {
                    //Debug.Log("ah shit 1", gameObject);
                    if (hitstuntimer == hitstun)
                    {
                        //Debug.Log("ah shit 2", gameObject);
                        isHit = false;
                        p1.velocity = Vector3.zero;
                        //p1.velocity = (currentHB.bkb+currentHB.skb*dmg)*currentHB.angle;
                        p1.AddForce ((currentHB.bkb + currentHB.skb * dmg) * currentHB.angle);
                    }

                    hitstuntimer--;
                    //two frames of hitLAG give increased directional influence, otherwise, DI is low during hitstun

                    if (hitstun - hitstuntimer < 3)
                    {
                        if (UnityEngine.Input.GetKey(KeyCode.A))
                        {
                            //p1.AddForce(-transform.right * airspd *3);
                        }

                        if (UnityEngine.Input.GetKey(KeyCode.D))
                        {
                           // p1.AddForce(transform.right * airspd *3);
                        }
                    } else
                    {
                        if (UnityEngine.Input.GetKey(KeyCode.A))
                        {
                           // p1.AddForce(-transform.right * airspd * .1f);
                        }

                        if (UnityEngine.Input.GetKey(KeyCode.D))
                        {
                            //p1.AddForce(transform.right * airspd * .1f);
                        }
                    }

                }
                
                //return to an acting state !!!! needs a few safety checks!!!!
                if (hitstuntimer == 0)
                {
                    if (Physics.Raycast(transform.position, -Vector3.up, out hit, .01f))
                    {
                        if (hit.collider.tag == "stage")
                        {

                            p1s = "walk";
                            //land = false;
                            runoff = false;
                            dashtimer = 0;
                            shieldtimer = 0;
                            lag = 0;
                            dtrtimer = 0;
                            okcrawl = false;
                            standuptimer = 0;

                            jumps = maxjumps;

                        }
                        else
                        {
                           // Debug.Log("out of hitstun 1", gameObject);
                            p1s = "airborn";
                            land = false;
                            runoff = false;
                            dashtimer = 0;
                            shieldtimer = 0;
                            lag = 0;
                            dtrtimer = 0;
                            okcrawl = false;
                            standuptimer = 0;

                            if (jumps == maxjumps)
                            {
                                jumps--;
                            }
                        }
                    }
                    else
                    {
                        //Debug.Log("out of hitstun 2", gameObject);
                        p1s = "airborn";
                        land = false;
                        runoff = false;
                        dashtimer = 0;
                        shieldtimer = 0;
                        lag = 0;
                        dtrtimer = 0;
                        okcrawl = false;
                        standuptimer = 0;

                        if (jumps == maxjumps)
                        {
                            jumps--;
                        }
                    }
                }
               

                break;



            case "ledge":
                p1rend.material.color = Color.black;
                break;



            case "freeze":
                p1rend.material.color = Color.black;
                break;


            case "crawl":
                p1rend.material.color = Color.magenta;
                if (standuptimer == 0)
                {
                    p1s = "walk";
                }
                if (UnityEngine.Input.GetKey(KeyCode.S)==false)
                {
                    standuptimer--;
                }
                if (UnityEngine.Input.GetKey(KeyCode.A))
                {
                    p1.AddForce(-transform.right * crawlspd);
                }
                if (UnityEngine.Input.GetKey(KeyCode.D))
                {
                    p1.AddForce(transform.right * crawlspd);
                }
                break;


         

        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "stage" && Physics.Raycast(transform.position, -Vector3.up, out hit, capsuleVOffset) && landOrRegrabtimer == 0) {
            Debug.Log("hi");
            land = true;
        }


    }

    /*
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider + "dildos");
        if (collider.tag == "runoff")
        {
            Debug.Log("extra dildos");
            p1rend.material.color = Color.red;
            runoff = true;
        }
    }
    */

    //hitbox interaction
    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log(collider + "dildos");
        if (collider.tag == "hitbox" )
        {
            
            currentHB = collider.GetComponent<hitbox>();
            if ( (currentHB.playerNum != thisPlayer) && currentHB.special =="" && isHit == false)
            {
                //Debug.Log("extra dildos");
                p1rend.material.color = Color.magenta;
                isHit = true;
                hitstun = 2*currentHB.dmg; //!!!! this is a temporary hitstun formula!!!!
                hitstuntimer = hitstun;
                dmg += currentHB.dmg;
                //Debug.Log("hs"+hitstun, gameObject);
                //Debug.Log("hst"+hitstuntimer, gameObject);
            }
            
        }

        if (collider.tag == "lrunoff") 
        {
            if (p1s == "walk" || p1s == "run" || p1s == "dash")
            {
                Debug.Log("enter");
                lrunofftimer = maxrunofftimer;
                p1.velocity = Vector3.zero;
                lrunoffstall = true;
                temptrans = collider.GetComponent<Transform>();
                p1.transform.position = temptrans.position + Vector3.up * capsuleVOffset;
            }
        }

        if (collider.tag == "rrunoff")
        {
            if (p1s == "walk" || p1s == "run" || p1s == "dash")
            {
                rrunofftimer = maxrunofftimer;
                p1.velocity = Vector3.zero;
                rrunoffstall = true;
                temptrans = collider.GetComponent<Transform>();
                p1.transform.position = temptrans.position+Vector3.up*capsuleVOffset;
            }
        }

    }
    /*
    void onTriggerStay (Collider collider)
    {
        Debug.Log("stay");
        if (collider.tag == "lrunoff")
        {
            Debug.Log("stay");
            if (lrunofftimer > 0)
            {
                lrunofftimer--;
                
            }
            
        }
        if (collider.tag == "rrunoff")
        {
            if (rrunofftimer > 0)
            {
                rrunofftimer--;
            }
        }

    }
    */
    
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "lrunoff")
        {
            Debug.Log("exit");
            //lrunofftimer = maxrunofftimer;
            lrunofftimer = 0;
            lrunoffstall = false;
            runoff = false;
        }
        if (collider.tag == "rrunoff")
        {
            //rrunofftimer = maxrunofftimer;
            rrunofftimer = 0;
            rrunoffstall = false;
            runoff = false;
        }
    }

   


}
