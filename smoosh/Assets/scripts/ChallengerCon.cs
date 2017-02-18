using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengerCon : MonoBehaviour
{

    public GameObject hbqueue;
    HBQ hbq;

    hitbox currentHB;

    public Vector3 pos;
    public Rigidbody p1;  //this could be named better
    public GameObject animObject;
    public GameObject animMesh;
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
    public int skidframes;
    public int skidtimer;
    //public float airspd;
    public float airMaxSpeed;
    public float airAccel;
    public float flty;
    public float fallSpdMax;
    public float fallAccel;
    public float roll;

    //input
    //private bool jumpPressed; 

    // public bool isJumping;

    public float gSHjumphgt;
    public float gFHjumphgt;
    public float ajumphgt;
    //public int jumpTime;
    public int hoptimer;
    public int maxhoptimer;
    public int squattimer;
    public int maxsquattimer;
    public int jumps;
    public int maxjumps;
    public int jumpcd;
    public int jumpmaxcd;
    public int jumpSquatFrames;
    //public int JumpFullFrames; //frames after short hop frames for full jump
    //public int jumpShortHopFrames;
    public bool fullhop;



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


    public bool intangible;
    public float rollLength;
    public int rollTime;
    public int rollStart;
    public int rolltimer;
    public int rollIntStart;
    public int rollIntEnd;
    public int rollDir;

    public int dodgeTime;
    public int dodgeStart;
    public int dodgetimer;
    public int dodgeIntStart;
    public int dodgeIntEnd;


    public bool land;
    public int oneframe;
    public bool runoff;
    public bool facingr1;

    public bool isHit;
    public int hitstun;
    public int hitstuntimer;
    public int dmg;

    public bool isDead;
    public int respawnTime;
    public int respawntimer;
    public int placePlayer;

    public int lrunofftimer;
    public int rrunofftimer;
    public int maxrunofftimer;
    public bool lrunoffstall;
    public bool rrunoffstall;
    public int landOrRegrabtimer;
    public int maxLandRegrab;

    public float capsuleVOffset;
    public float capsuleHOffset;

    public int ledgetimer;
    public int ledgetimer2;
    public int ledgelag;
    public int ledgeIntang;
    public int lag;
    public bool nGetup;
    public bool jGetup;
    public bool rGetup;
    public bool dGetup;
    public int nGetupFrames;
    public int jGetupFrames;
    public int rGetupFrames;
    public Vector3 ledgePos;
    public int currentDir;

    RaycastHit hit;

    public bool ledgeGrab;

    // Use this for initialization
    void Start()
    {
        int dmg = 0;
        thisPlayer = 1;
        p1 = GetComponent<Rigidbody>();
        p1rend = animMesh.GetComponent<Renderer>();

        p1rend.material.color = Color.yellow;
        p1s = "walk";

        isHit = false;
        walkaccel = 30;
        walkspd = 3;
        runaccel = 55;
        runspd = 8;
        crawlspd = 8;

        airMaxSpeed = 3;
        airAccel = 20;
        fallSpdMax = 3;
        fallAccel = 20;
        gFHjumphgt = 350;
        gSHjumphgt = 250;
        ajumphgt = 350;
        jumpmaxcd = 1;
        maxjumps = 2;
        jumps = 2;
        maxsquattimer = 6;
        maxhoptimer = 5; //cannot be more than maxsquattimer
        hoptimer = 0;
        squattimer = 0;

        dashwindow = 10;
        dashtimer = 0;
        dashl = 400;
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

        isDead = false;
        placePlayer = 40;
        respawnTime = 80;

        maxrunofftimer = 5;
        //lrunofftimer = maxrunofftimer;
        //rrunofftimer = maxrunofftimer;
        lrunofftimer = 0;
        rrunofftimer = 0;
        lrunoffstall = false;
        rrunoffstall = false;
        landOrRegrabtimer = 0;
        maxLandRegrab = 1;//might need different timers for runoff/ledge


        intangible = false;
        dodgeTime = 30;
        dodgeIntStart = 7;
        dodgeIntEnd = 22;

        rollTime = 30;
        rollLength = 400;
        rollStart = 7;
        rollIntStart = 7;
        rollIntEnd = 22;

        capsuleVOffset = .31f;
        capsuleHOffset = .2f;
        ledgeGrab = false;
        ledgelag = 15;
        ledgeIntang = 5;
        nGetupFrames = 10;
        jGetupFrames = 20;
        rGetupFrames = 30;

        skidframes = 13;


    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;


        //input buffer



























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

        if (ledgeGrab)
        {
            p1s = "ledge";
            ledgeGrab = false;
        }
        if (isHit)
        {
            p1s = "hitstun";
        }

        if (isDead)
        {
            p1s = "dead";
        }


        //for some reason, capsule is not triggering "onTriggerStay". This is a temp solution
        //Ian to bill - what is the capsul trying to trigger on trigger stay on?

        //Bill to Ian - capsule should register 'stay' in the runoff zone when standing on the edge, the commented out version below looks a lot like this.
        if (Physics.Raycast(transform.position, Vector3.down, out hit, capsuleVOffset))
        {
            if (hit.collider.tag == "lrunoff" && (p1s == "walk" || p1s == "run" || p1s == "dash" || p1s=="skid"))
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
        if (landOrRegrabtimer > 0)
        {
            landOrRegrabtimer--;
        }


        //begin state processing:

        switch (p1s)
        {
            case "walk":
                //Color Debug State
                p1rend.material.color = Color.yellow;

                //-Ian to Bill -- what is this check doing, it looks like it is looking
                //at wether or not we are dashing to see if we can crouch??? is crouch still in?

                //-Bill to Ian -- yea, that's what it does

                //check to see if we are dashing, if not we can crouch
                if (dashtimer > 0)
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
                    if (facingr1)
                    {
                        currentDir = 1;
                    }
                    else
                    {
                        currentDir = -1;
                    }
                    p1.position = p1.position + Vector3.right * currentDir*capsuleHOffset;
                    p1.AddForce(currentDir*Vector3.right*walkspd);
                }

                //input
                if (UnityEngine.Input.GetKey(KeyCode.A) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                {
                    facingr1 = false;
                    if (dashtimer > 0 && UnityEngine.Input.GetKeyDown(KeyCode.A))
                    {
                        p1.velocity = Vector3.zero;
                        p1s = "dash";
                        p1.AddForce(-transform.right * dashl);
                        dtrtimer = dashToRun;
                    }
                    else
                    {
                        if (p1.velocity.x > -walkspd && !(lrunoffstall && lrunofftimer > 0))
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

                if (UnityEngine.Input.GetKey(KeyCode.D) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                {
                    facingr1 = true;
                    if (dashtimer > 0 && UnityEngine.Input.GetKeyDown(KeyCode.D))
                    {
                        p1.velocity = Vector3.zero;
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



                if (UnityEngine.Input.GetKeyDown(KeyCode.W) && jumps > 0)
                {
                    //p1.AddForce(transform.up * gjumphgt);
                    p1s = "jumpsquat";
                    jumpcd = jumpmaxcd;
                    jumps--;
                    land = false;
                    squattimer = maxsquattimer;
                    hoptimer = maxhoptimer;
                    //Debug.Log("walk jump");
                }

                //crouch to dash

                if (UnityEngine.Input.GetKeyDown(KeyCode.S))
                {
                    dashtimer = dashwindow;
                }




                if (UnityEngine.Input.GetKey(KeyCode.S) && dashtimer == 0)
                {
                    standuptimer = crawlToStand;
                    p1s = "crawl";
                }

                if (UnityEngine.Input.GetKey(KeyCode.F) && (UnityEngine.Input.GetMouseButton(0)) && shieldhealth > 0)
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
                    hbq.DeQ(15, .2f, 55, Vector3.right, true, Vector3.zero,
                        1, Vector3.up, 5, 5, false, 0, 50, 10);
                    //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
                    //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb
                }

                if (UnityEngine.Input.GetMouseButtonDown(1) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                {
                    p1s = "attacking";
                    lag = 10;
                    hbq = hbqueue.GetComponent<HBQ>();
                    hbq.DeQ(15, .5f, 200, Vector3.right, false, Vector3.zero,
                        2, Vector3.up, 10, 0, false, 0, 150, 10);
                    //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
                    //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb
                }


                if (UnityEngine.Input.GetKeyDown(KeyCode.A) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    rolltimer = 0;
                    rollDir = -1;
                    p1s = "roll";
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.D) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    rolltimer = 0;
                    rollDir = 1;
                    p1s = "roll";
                }

                if (UnityEngine.Input.GetKey(KeyCode.S) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    dodgetimer = 0;
                    p1s = "dodge";
                }

                //alternate input
                if (UnityEngine.Input.GetMouseButton(1) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    dodgetimer = 0;
                    p1s = "dodge";
                }




                //doubletap dash

                if (UnityEngine.Input.GetKeyDown(KeyCode.D) && dashtimer == 0)
                {
                    dashtimer = dashwindow;
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.A) && dashtimer == 0)
                {
                    dashtimer = dashwindow;
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
                    if (facingr1)
                    {
                        currentDir = 1;
                    }
                    else
                    {
                        currentDir = -1;
                    }
                    if (UnityEngine.Input.GetKey(KeyCode.A))
                    {
                        p1s = "run";
                        p1.AddForce(-Vector3.right*runspd);
                    }
                    if (UnityEngine.Input.GetKey(KeyCode.D))
                    {
                        p1s = "run";
                    }

                    if (runoff)
                    {

                        p1s = "airborn";
                        jumps--;
                        land = false;
                        runoff = false;
                        dashtimer = 0;
                        if (facingr1)
                        {
                            currentDir = 1;
                        }
                        else
                        {
                            currentDir = -1;
                        }
                        p1.position = p1.position + Vector3.right * currentDir * capsuleHOffset;
                        p1.AddForce(currentDir * Vector3.right * runspd);
                    }

                }


                break;



            case "run":
                p1rend.material.color = Color.grey;

                if (UnityEngine.Input.GetKeyDown(KeyCode.W) && jumps > 0)
                {
                    p1s = "jumpsquat";
                    jumpcd = jumpmaxcd;
                    jumps--;
                    land = false;
                    squattimer = maxsquattimer;
                    hoptimer = maxhoptimer;
                }
                else
                if (UnityEngine.Input.GetKey(KeyCode.A) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                {
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
                else if (UnityEngine.Input.GetKey(KeyCode.D) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                {
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
                    if (p1.velocity.x>walkspd || p1.velocity.x < -runspd)
                    {
                        p1s = "skid";
                        skidtimer = skidframes;
                    }
                    else
                    {
                        p1s = "walk";
                    }
                }

                if (runoff)
                {
                    p1s = "airborn";
                    jumps--;
                    land = false;
                    runoff = false;
                    dashtimer = 0;
                    if (facingr1)
                    {
                        currentDir = 1;
                    }
                    else
                    {
                        currentDir = -1;
                    }
                    p1.position = p1.position + Vector3.right * currentDir * capsuleHOffset;
                    p1.AddForce(currentDir * Vector3.right * runspd);
                    //impart maxrunspeed in direction we are facing
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.A) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    rolltimer = 0;
                    rollDir = -1;
                    p1s = "roll";
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.D) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    rolltimer = 0;
                    rollDir = 1;
                    p1s = "roll";
                }

                if (UnityEngine.Input.GetKey(KeyCode.S) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    dodgetimer = 0;
                    p1s = "dodge";
                }

                //alternate input
                if (UnityEngine.Input.GetMouseButton(1) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    dodgetimer = 0;
                    p1s = "dodge";
                }

                break;


            case "jumpsquat":


                p1rend.material.color = Color.magenta;

                if (hoptimer > 0)
                {
                    hoptimer--;
                    if (hoptimer == 0)
                    {
                        if (UnityEngine.Input.GetKey(KeyCode.W))
                        {
                            fullhop = true;
                        }
                        else
                        {
                            fullhop = false;
                        }
                    }
                }
                if (squattimer > 0)
                {
                    squattimer--;
                    if (squattimer == 0)
                    {
                        if (fullhop)
                        {
                            p1.AddForce(transform.up * gFHjumphgt);
                        }
                        else
                        {
                            p1.AddForce(transform.up * gSHjumphgt);
                        }
                        p1s = "airborn";
                    }
                }




                break;


            case "airborn":
                p1rend.material.color = Color.cyan;

                if (jumpcd > 0)
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


                if (UnityEngine.Input.GetKeyDown(KeyCode.W) && jumpcd == 0 && jumps > 0 && !(land))
                {
                    p1.velocity = new Vector3(p1.velocity.x, 0, 0);
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
                    if (p1.velocity.x > walkspd)
                    {
                        p1s = "run";
                    }
                    else
                    {
                        p1s = "walk";
                    }

                    jumps = maxjumps;
                    land = false;
                }

                break;



            case "attacking":
                p1rend.material.color = Color.green;

                if (lag > 0)
                {
                    lag--;
                    if (Physics.Raycast(transform.position, -Vector3.up, out hit, capsuleVOffset))
                    {
                        if (hit.collider.tag == "stage")
                        {
                            if (lag == 0)
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
                        }
                        else
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
                    }
                    else
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

                if (!(UnityEngine.Input.GetKey(KeyCode.F)) || !(UnityEngine.Input.GetMouseButton(0)))
                {
                    inshield = false;
                }

                if (UnityEngine.Input.GetKey(KeyCode.F) == false && shieldstun == 0)
                {
                    shieldtimer = 0;
                    p1s = "walk";
                }



                break;

            case "roll":
                p1rend.material.color = Color.white;
                if (rolltimer < rollTime)
                {
                    rolltimer++;
                    if (rolltimer == 1)
                    {
                        p1.velocity = Vector3.zero;
                    }
                    if (rolltimer == rollStart)
                    {
                        p1.AddForce(rollDir * transform.right * rollLength);
                    }
                    if (rolltimer == rollIntStart)
                    {
                        intangible = true;
                    }
                    if (rolltimer == rollIntEnd)
                    {
                        intangible = false;

                        if (rollDir == 1)
                        {
                            facingr1 = false;
                        }
                        else
                        {
                            facingr1 = true;
                        }
                    }
                }
                else
                {
                    p1s = "walk";
                }


                break;

            case "dodge":
                p1rend.material.color = Color.white;
                if (dodgetimer < dodgeTime)
                {
                    dodgetimer++;
                    if (dodgetimer == 1)
                    {
                        p1.velocity = Vector3.zero;
                    }
                    if (dodgetimer == dodgeIntStart)
                    {
                        intangible = true;
                    }
                    if (dodgetimer == dodgeIntEnd)
                    {
                        intangible = false;
                    }
                }
                else
                {
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
                        p1.AddForce((currentHB.bkb + currentHB.skb * dmg) * currentHB.angle);
                    }

                    hitstuntimer--;
                    //two frames of hitLAG give increased directional influence, otherwise, DI is low during hitstun

                    if (hitstun - hitstuntimer < 3)
                    {
                        if (UnityEngine.Input.GetKey(KeyCode.A))
                        {
                            p1.AddForce(-transform.right * airAccel * 3);
                        }

                        if (UnityEngine.Input.GetKey(KeyCode.D))
                        {
                            p1.AddForce(transform.right * airAccel * 3);
                        }
                    }
                    else
                    {
                        if (UnityEngine.Input.GetKey(KeyCode.A))
                        {
                            p1.AddForce(-transform.right * airAccel * .1f);
                        }

                        if (UnityEngine.Input.GetKey(KeyCode.D))
                        {
                            p1.AddForce(transform.right * airAccel * .1f);
                        }
                        if (Physics.Raycast(transform.position, -Vector3.up, out hit, .01f))
                        {
                            if (hit.collider.tag != "stage")
                            {
                                //flty
                            }
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

            case "dead":
                p1rend.material.color = Color.black;
                isDead = false;
                p1.velocity = Vector3.zero;
                if (respawntimer < placePlayer)
                {
                    p1.position = Vector3.zero;
                }
                if (respawntimer > 0)
                {
                    respawntimer--;
                }
                else
                {
                    p1s = "airborn";
                    jumps = maxjumps - 1;
                    land = false;
                    runoff = false;
                    dashtimer = 0;
                    shieldtimer = 0;
                    lag = 0;
                    dtrtimer = 0;
                    okcrawl = false;
                    standuptimer = 0;

                }
                break;

            case "ledge":
                p1rend.material.color = Color.black;
                //Debug.Log("ledge");
                if (ledgetimer > 0)
                {
                    intangible = true;
                    ledgetimer--;
                    p1.velocity = Vector3.zero;
                    p1.position = ledgePos;
                }
                else
                {

                    intangible = false;

                    //choose getup option
                    if (!nGetup && !jGetup && !rGetup && !dGetup)
                    {
                        p1.velocity = Vector3.zero;
                        p1.position = ledgePos;
                        if (UnityEngine.Input.GetKey(KeyCode.A) && facingr1)
                        {
                            rGetup = true;
                            ledgetimer2 = rGetupFrames;
                        }
                        if (UnityEngine.Input.GetKey(KeyCode.D) && facingr1)
                        {
                            nGetup = true;
                            ledgetimer2 = nGetupFrames;
                        }
                        if (UnityEngine.Input.GetKey(KeyCode.A) && !facingr1)
                        {
                            nGetup = true;
                            ledgetimer2 = nGetupFrames;
                        }
                        if (UnityEngine.Input.GetKey(KeyCode.D) && !facingr1)
                        {
                            rGetup = true;
                            ledgetimer2 = rGetupFrames;
                        }

                        if (UnityEngine.Input.GetKey(KeyCode.S))
                        {
                            dGetup = true;
                        }
                        if (UnityEngine.Input.GetKey(KeyCode.W))
                        {
                            jGetup = true;
                            ledgetimer2 = jGetupFrames;
                        }
                    }


                    //getup

                    if (nGetup)
                    {
                        if (ledgetimer2 > 0)
                        {
                            if (ledgetimer2 == nGetupFrames)
                            {
                                p1.position = ledgePos + (capsuleVOffset+.5f) * Vector3.up + 2*currentDir*capsuleHOffset*Vector3.right;
                            }
                            ledgetimer2--;
                        } else
                        {
                            p1s = "walk";
                        }
                    }
                    if (jGetup)
                    {
                        if (ledgetimer2 >0)
                        {
                            if (ledgetimer2 == jGetupFrames)
                            {
                                p1.position = ledgePos + (capsuleVOffset + .5f) * Vector3.up; //+ 2 * currentDir * capsuleHOffset * Vector3.right
                                p1.AddForce(Vector3.up * gFHjumphgt + Vector3.right * 50*currentDir*airMaxSpeed);
                            }
                            ledgetimer2--;
                        }
                        else
                        {
                            jumps--;
                            p1s = "airborn";
                        }
                    }
                    if (rGetup)
                    {
                        if (ledgetimer2 > 0)
                        {
                            if (ledgetimer2 == rGetupFrames)
                            {
                                p1.position = ledgePos + (capsuleVOffset + .5f) * Vector3.up + 2 * currentDir * capsuleHOffset * Vector3.right;
                                p1.AddForce(Vector3.right * currentDir * rollLength);
                            }
                            ledgetimer2--;
                        }
                        else
                        {
                            p1s = "walk";
                        }
                    }
                    if (dGetup)
                    {
                        landOrRegrabtimer = maxLandRegrab;
                        p1s = "airborn";
                        jumps--;
                    }
                }
                break;

            case "skid":
                p1rend.material.color = Color.blue;
                if (skidtimer>0)
                {
                    skidtimer--;
                    if (skidframes-skidtimer < 6)
                    {
                        if (UnityEngine.Input.GetKey(KeyCode.A) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                        {
                            facingr1 = false;
                            if (p1.velocity.x > -runspd && !(lrunoffstall && lrunofftimer > 0))
                            {
                                p1.AddForce(-transform.right * runaccel);
                            }

                            p1s = "run";
                        }
                        if (UnityEngine.Input.GetKey(KeyCode.D) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                        {
                            facingr1 = true;
                            if (p1.velocity.x < runspd && !(rrunoffstall && lrunofftimer > 0))
                            {
                                p1.AddForce(transform.right * runaccel);
                            }

                            p1s = "run";
                        }
                        if (UnityEngine.Input.GetKeyDown(KeyCode.A) && UnityEngine.Input.GetKey(KeyCode.F))
                        {
                            rolltimer = 0;
                            rollDir = -1;
                            p1s = "roll";
                        }

                        if (UnityEngine.Input.GetKeyDown(KeyCode.D) && UnityEngine.Input.GetKey(KeyCode.F))
                        {
                            rolltimer = 0;
                            rollDir = 1;
                            p1s = "roll";
                        }

                        if (UnityEngine.Input.GetKey(KeyCode.S) && UnityEngine.Input.GetKey(KeyCode.F))
                        {
                            dodgetimer = 0;
                            p1s = "dodge";
                        }

                        //alternate input
                        if (UnityEngine.Input.GetMouseButton(1) && UnityEngine.Input.GetKey(KeyCode.F))
                        {
                            dodgetimer = 0;
                            p1s = "dodge";
                        }
                    }
                }
                else
                {
                    p1s="walk";
                }
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
                if (UnityEngine.Input.GetKey(KeyCode.S) == false)
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
        if (col.gameObject.tag == "stage" && Physics.Raycast(transform.position, -Vector3.up, out hit, capsuleVOffset) && landOrRegrabtimer == 0)
        {
            //Debug.Log("landed");
            if (p1.velocity.y < 0)
            {

            }
            land = true;

            //Working

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
        if (collider.tag == "hitbox")
        {

            currentHB = collider.GetComponent<hitbox>();
            if ((currentHB.playerNum != thisPlayer) && currentHB.special == "" && isHit == false && inshield == false && intangible == false)
            {
                //Debug.Log("extra dildos");
                p1rend.material.color = Color.magenta;
                isHit = true;
                hitstun = 2 * currentHB.dmg; //!!!! this is a temporary hitstun formula!!!!
                hitstuntimer = hitstun;
                dmg += currentHB.dmg;
                //Debug.Log("hs"+hitstun, gameObject);
                //Debug.Log("hst"+hitstuntimer, gameObject);
            }

        }

        if (collider.tag == "lrunoff" && (p1s == "walk" || p1s == "run" || p1s == "dash" || p1s == "skid"))
        {
            lrunofftimer = maxrunofftimer;
            p1.velocity = Vector3.zero;
            lrunoffstall = true;
            temptrans = collider.GetComponent<Transform>();
            p1.transform.position = temptrans.position + Vector3.up * capsuleVOffset;
        }

        if (collider.tag == "rrunoff" && (p1s == "walk" || p1s == "run" || p1s == "dash" || p1s == "skid"))
        {
            rrunofftimer = maxrunofftimer;
            p1.velocity = Vector3.zero;
            rrunoffstall = true;
            temptrans = collider.GetComponent<Transform>();
            p1.transform.position = temptrans.position + Vector3.up * capsuleVOffset;
        }

        if (collider.tag == "blastline")
        {
            isDead = true;
            respawntimer = respawnTime;
        }

        if (collider.tag == "lledge" && landOrRegrabtimer == 0 && !(UnityEngine.Input.GetKey(KeyCode.S)) && !(p1s == "attacking"))
        {
            ledgeGrab = true;
            facingr1 = true;
            jumps = maxjumps;
            ledgetimer = ledgelag;
            nGetup = false;
            jGetup = false;
            rGetup = false;
            dGetup = false;
            p1.velocity = Vector3.zero;
            temptrans = collider.GetComponent<Transform>();
            p1.transform.position = temptrans.position - Vector3.right * capsuleHOffset;
            ledgePos = temptrans.position - Vector3.right * capsuleHOffset;
            currentDir = 1;
            ledgetimer2 = 0;
        }
        if (collider.tag == "rledge" && landOrRegrabtimer == 0 && !(UnityEngine.Input.GetKey(KeyCode.W)) && !(p1s == "attacking"))
        {
            ledgeGrab = true;
            facingr1 = false;
            jumps = maxjumps;
            ledgetimer = ledgelag;
            nGetup = false;
            jGetup = false;
            rGetup = false;
            dGetup = false;
            p1.velocity = Vector3.zero;
            temptrans = collider.GetComponent<Transform>();
            p1.transform.position = temptrans.position + Vector3.right * capsuleHOffset;
            ledgePos = temptrans.position + Vector3.right * capsuleHOffset;
            currentDir = -1;
            ledgetimer2 = 0;
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
            //Debug.Log("exit");
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

    void upSpecial()
    {

    }

    void sideSpecial()
    {

    }

    void downSpecial()
    {

    }

    void upNormal()
    {

    }

    void sideNormal()
    {

    }

    void downNormal()
    {

    }

    void jab()
    {

    }

    void grab() 
    {

    }


}
