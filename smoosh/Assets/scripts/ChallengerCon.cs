using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChallengerCon : MonoBehaviour
{

    
    public FrameData frameData;
    public MoveList moveList;

    //public delegate void move();
    //public move jab;
    public Action<int> jab;
    public Action<int> grab;
    public Action<int> fWeak;
    public Action<int> uWeak;
    public Action<int> dWeak;
    public Action<int> fStrong;
    public Action<int> uStrong;
    public Action<int> dStrong;
    public Action<int> fSpecial;
    public Action<int> uSpecial;
    public Action<int> dSpecial;
    public Action<int> nAir;
    public Action<int> fAir;
    public Action<int> uAir;
    public Action<int> dAir;
    public Action<int> dashAttack;
    /*
    public GameObject hbqueue;
    public HBQ hbq;
    hitbox currentHB;
    */

    public int hbMax;
    public GameObject pcObj;
    public List<GameObject> hbObjList;
    public List<hitbox> hbCompList;
    hitbox currentHB;

    RaycastHit hit;

    public Vector3 pos;
    public Rigidbody p1;  //this could be named better
    public GameObject animObject;
    public GameObject animMesh;
    public Renderer p1rend;
    public Transform temptrans;
    public int thisPlayer;
    public string state;
    public float weight;

    public float walkAccel;
    public float walkSpeed;
    public float crawlspd;
    public float runAccel;
    public float runSpeed;
    public float dashl;
    public int skidFrames;
    public int skidTimer;
    public float airMaxSpeed;
    public float airAccel;
    public float flty;
    public float fallSpdMax;
    public float fallAccel;
    public float roll;


    public float shJumpHeight;
    public float fhJumpHeight;
    public float aJumpHeight;
    public int hopTimer;
    public int hopTimerMax;
    public int squatTimer;
    public int squatTimerMax;
    public int jumps;
    public int jumpsMax;
    public int jumpcd;
    public int jumpCdMax;
    public int jumpSquatFrames;
    public bool fullhop;


    public int dashWindow;
    public int dashToRun;
    public int dashTimer;
    public int dtrtimer;
    public int standupTimer;
    public int crawlToStand;
    public bool okCrawl;


    public int shieldUp;
    public int shieldTimer;
    public bool inShield;
    public int shieldHealth;
    public int shieldHealthMax;
    public int shieldStun;


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
    public int dodgeTimer;
    public int dodgeIntStart;
    public int dodgeIntEnd;


    public bool land;
    public int oneframe;
    public bool runoff;
    public bool facingr1;


    public bool isHit;
    public int hitstun;
    public int hitstunTimer;
    public int dmg;


    public bool isDead;
    public int respawnTime;
    public int respawntimer;
    public int placePlayer;


    public int lrunoffTimer;
    public int rrunoffTimer;
    public int runoffTimerMax;
    public bool lrunoffStall;
    public bool rrunoffStall;
    public int landOrRegrabTimer;
    public int landOrRegrabTimerMax;


    public float capsuleVOffset;
    public float capsuleHOffset;


    public int ledgeTimer;
    public int ledgeTimer2;
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
    public bool ledgeGrab;



    // Use this for initialization
    void Start()
    {


        frameData = new FrameData("TMan"); //move to constructor
        moveList = new MoveList(this,"TMan");
        dmg = 0;
        thisPlayer = 1;
        p1 = GetComponent<Rigidbody>();
        p1rend = animMesh.GetComponent<Renderer>();

        p1rend.material.color = Color.yellow;
        state = "walk";

        hbMax = frameData.hbMax;

        //hitbox queue
        for (int i = 0; i < hbMax; i++)
        {
            //instantiate a sphere
            hbObjList.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            //add hitbox Component to that sphere and to our hitbox component list
            hbCompList.Add(hbObjList[i].AddComponent<hitbox>());
            //hbCompList[i].player = gameObject;
            hbCompList[i].p = this;
            hbCompList[i].offset = i;
            hbObjList[i].GetComponent<SphereCollider>().isTrigger = true;
            hbObjList[i].tag = "hitbox";
            hbObjList[i].transform.position = new Vector3(-10, 5 + 5 * i, 3);


        }

        isHit = false;

        //walk
        walkAccel = frameData.walkAccel;
        walkSpeed = frameData.walkMaxSpeed;

        //run/dash
        runAccel = frameData.runAccel;
        runSpeed = frameData.runSpeed;
        dashWindow = frameData.dashWindow;
        dashTimer = 0;
        dashl = frameData.dashl;
        dashToRun = frameData.dashToRun;

        //crawl
        crawlspd = frameData.crawlAccel;

        //jump
        airMaxSpeed = frameData.airMaxSpeed;
        airAccel = frameData.airAccel;
        fallSpdMax = frameData.fallSpdMax;
        fallAccel = frameData.fallAccel;
        fhJumpHeight = frameData.fhJumpHeight;
        shJumpHeight = frameData.shJumpHeight;
        aJumpHeight = frameData.aJumpHeight;
        jumpCdMax = frameData.jumpCdMax;
        jumpsMax = frameData.jumpsMax;
        jumps = jumpsMax;
        squatTimerMax = frameData.squatTimerMax;
        hopTimerMax = frameData.hopTimerMax; //cannot be more than squatTimerMax, WILL break game
        hopTimer = 0;
        squatTimer = 0;


        //crawl
        crawlToStand = frameData.crawlToStand;
        standupTimer = 0;
        okCrawl = false;

        //shield
        shieldUp = frameData.shieldUp;
        shieldTimer = 0;
        inShield = false;
        shieldHealthMax = frameData.maxShield;
        shieldHealth = shieldHealthMax;
        shieldStun = 0;

        //various state related
        land = false;
        oneframe = 0;
        runoff = false;
        facingr1 = true;
        lag = 0;

        //getting hurt
        hitstun = 0;
        hitstunTimer = 0;
        isDead = false;
        placePlayer = 40;
        respawnTime = 80;

        //running off the edge like a boss
        runoffTimerMax = 5;
        //lrunoffTimer = runoffTimerMax;
        //rrunoffTimer = runoffTimerMax;
        lrunoffTimer = 0;
        rrunoffTimer = 0;
        lrunoffStall = false;
        rrunoffStall = false;
        landOrRegrabTimer = 0;
        landOrRegrabTimerMax = 1;//might need different timers for runoff/ledge

        //dodge
        intangible = false;
        dodgeTime = frameData.dodgeTime;
        dodgeIntStart = frameData.dodgeIntStart;
        dodgeIntEnd = frameData.dodgeIntEnd;

        //roll
        rollTime = frameData.rollTime;
        rollLength = frameData.rollLength;
        rollStart = frameData.rollStart;
        rollIntStart = frameData.rollIntStart;
        rollIntEnd = frameData.rollIntEnd;

        //size
        capsuleVOffset = frameData.capsuleVOffset;
        capsuleHOffset = frameData.capsuleHOffset;
        ledgeGrab = false;
        ledgelag = 15;
        ledgeIntang = 5;
        nGetupFrames = frameData.nGetupFrames;
        jGetupFrames = frameData.jGetupFrames;
        rGetupFrames = frameData.rGetupFrames;
        weight = frameData.weight;

        //skid
        skidFrames = frameData.skidFrames;

        //initialize moveset
        jab = moveList.jab;
        grab = moveList.grab;
        fWeak = moveList.fweak;
        uWeak = moveList.uweak;
        dWeak = moveList.dweak;
        fStrong = moveList.fstrong;
        uStrong = moveList.ustrong;
        dStrong = moveList.dstrong;
        fSpecial = moveList.fspec;
        uSpecial = moveList.uspec;
        dSpecial = moveList.dspec;
        nAir = moveList.nair;
        fAir = moveList.fair;
        uAir = moveList.uair;
        dAir = moveList.dair;
        dashAttack = moveList.dash;

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
            state = "ledge";
            ledgeGrab = false;
        }
        if (isHit)
        {
            state = "hitstun";
        }

        if (isDead)
        {
            state = "dead";
        }


        //for some reason, capsule is not triggering "onTriggerStay". This is a temp solution
        //Ian to bill - what is the capsul trying to trigger on trigger stay on?

        //Bill to Ian - capsule should register 'stay' in the runoff zone when standing on the edge, the commented out version below looks a lot like this.
        if (Physics.Raycast(transform.position, Vector3.down, out hit, capsuleVOffset))
        {
            if (hit.collider.tag == "lrunoff" && (state == "walk" || state == "run" || state == "dash" || state == "skid"))
            {
                //Debug.Log("stay");
                if (lrunoffTimer > 0)
                {
                    lrunoffTimer--;

                }

            }
            if (hit.collider.tag == "rrunoff")
            {
                if (rrunoffTimer > 0)
                {
                    rrunoffTimer--;
                }
            }
        }
        if (landOrRegrabTimer > 0)
        {
            landOrRegrabTimer--;
        }


        //begin state processing:

        switch (state)
        {
            case "walk":
                //Color Debug State
                p1rend.material.color = Color.yellow;

                //-Ian to Bill -- what is this check doing, it looks like it is looking
                //at wether or not we are dashing to see if we can crouch??? is crouch still in?

                //-Bill to Ian -- yea, that's what it does

                //check to see if we are dashing, if not we can crouch
                if (dashTimer > 0)
                {
                    okCrawl = false;
                    dashTimer--;
                    if (dashTimer == 0)
                    {
                        okCrawl = true;
                    }
                }

                if (runoff)
                {
                    //Debug.Log("walk - runoff");
                    state = "airborn";
                    jumps--;
                    land = false;
                    runoff = false;
                    dashTimer = 0;
                    if (facingr1)
                    {
                        currentDir = 1;
                    }
                    else
                    {
                        currentDir = -1;
                    }
                    p1.position = p1.position + Vector3.right * currentDir * capsuleHOffset;
                    p1.AddForce(currentDir * Vector3.right * walkSpeed);
                }

                //input
                if (UnityEngine.Input.GetKey(KeyCode.A) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                {
                    facingr1 = false;
                    if (dashTimer > 0 && UnityEngine.Input.GetKeyDown(KeyCode.A))
                    {
                        p1.velocity = Vector3.zero;
                        state = "dash";
                        p1.AddForce(-transform.right * dashl);
                        dtrtimer = dashToRun;
                    }
                    else
                    {
                        if (p1.velocity.x > -walkSpeed && !(lrunoffStall && lrunoffTimer > 0))
                        {
                            p1.AddForce(-transform.right * walkAccel);
                        }
                        if (lrunoffTimer == 0 && lrunoffStall == true)
                        {
                            runoff = true;
                            landOrRegrabTimer = landOrRegrabTimerMax;
                        }
                    }
                }

                if (UnityEngine.Input.GetKey(KeyCode.D) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                {
                    facingr1 = true;
                    if (dashTimer > 0 && UnityEngine.Input.GetKeyDown(KeyCode.D))
                    {
                        p1.velocity = Vector3.zero;
                        state = "dash";
                        p1.AddForce(transform.right * dashl);
                        dtrtimer = dashToRun;
                    }
                    else
                    {

                        if (p1.velocity.x < walkSpeed && !(rrunoffStall && rrunoffTimer > 0))
                        {
                            p1.AddForce(transform.right * walkAccel);
                        }
                        if (rrunoffTimer == 0 && rrunoffStall == true)
                        {
                            runoff = true;
                            landOrRegrabTimer = landOrRegrabTimerMax;
                        }
                    }
                }



                if (UnityEngine.Input.GetKeyDown(KeyCode.W) && jumps > 0)
                {
                    //p1.AddForce(transform.up * gjumphgt);
                    state = "jumpsquat";
                    jumpcd = jumpCdMax;
                    jumps--;
                    land = false;
                    squatTimer = squatTimerMax;
                    hopTimer = hopTimerMax;
                    //Debug.Log("walk jump");
                }

                //crouch to dash

                if (UnityEngine.Input.GetKeyDown(KeyCode.S))
                {
                    dashTimer = dashWindow;
                }




                if (UnityEngine.Input.GetKey(KeyCode.S) && dashTimer == 0)
                {
                    standupTimer = crawlToStand;
                    state = "crawl";
                }

                if (UnityEngine.Input.GetKey(KeyCode.F) && (UnityEngine.Input.GetMouseButton(0)) && shieldHealth > 0)
                {
                    state = "shield";
                    dashTimer = 0;
                    shieldTimer = 3;
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.Space)) //check cooldown, charges
                {
                    jab(0);
                    //hbq = hbqueue.GetComponent<HBQ>();
                    //hbq.DeQ(15, .2f, 55, Vector3.right, true, Vector3.zero,
                        //1, Vector3.up, 5, 5, false, 0, 50, 10);
                    //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
                    //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb
                }

                if (UnityEngine.Input.GetMouseButtonDown(1) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                {
                    dSpecial(0);
                    /*
                    state = "attacking";
                    lag = 10;
                    hbq = hbqueue.GetComponent<HBQ>();
                    hbq.DeQ(15, .5f, 200, Vector3.right, false, Vector3.zero,
                        2, Vector3.up, 10, 0, false, 0, 150, 10);
                    //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
                    //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb
                    */
                }


                if (UnityEngine.Input.GetKeyDown(KeyCode.A) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    rolltimer = 0;
                    rollDir = -1;
                    state = "roll";
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.D) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    rolltimer = 0;
                    rollDir = 1;
                    state = "roll";
                }

                if (UnityEngine.Input.GetKey(KeyCode.S) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    dodgeTimer = 0;
                    state = "dodge";
                }

                //alternate input
                if (UnityEngine.Input.GetMouseButton(1) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    dodgeTimer = 0;
                    state = "dodge";
                }




                //doubletap dash

                if (UnityEngine.Input.GetKeyDown(KeyCode.D) && dashTimer == 0)
                {
                    dashTimer = dashWindow;
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.A) && dashTimer == 0)
                {
                    dashTimer = dashWindow;
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
                    state = "walk";
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
                        state = "run";
                        p1.AddForce(-Vector3.right * runSpeed);
                    }
                    if (UnityEngine.Input.GetKey(KeyCode.D))
                    {
                        state = "run";
                    }

                    if (runoff)
                    {

                        state = "airborn";
                        jumps--;
                        land = false;
                        runoff = false;
                        dashTimer = 0;
                        if (facingr1)
                        {
                            currentDir = 1;
                        }
                        else
                        {
                            currentDir = -1;
                        }
                        p1.position = p1.position + Vector3.right * currentDir * capsuleHOffset;
                        p1.AddForce(currentDir * Vector3.right * runSpeed);
                    }

                }


                break;



            case "run":
                p1rend.material.color = Color.grey;

                if (UnityEngine.Input.GetKeyDown(KeyCode.W) && jumps > 0)
                {
                    state = "jumpsquat";
                    jumpcd = jumpCdMax;
                    jumps--;
                    land = false;
                    squatTimer = squatTimerMax;
                    hopTimer = hopTimerMax;
                }
                else
                if (UnityEngine.Input.GetKey(KeyCode.A) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                {
                    facingr1 = false;
                    if (p1.velocity.x > -runSpeed && !(lrunoffStall && lrunoffTimer > 0))
                    {
                        p1.AddForce(-transform.right * runAccel);
                    }

                    if (lrunoffTimer == 0 && lrunoffStall == true)
                    {
                        runoff = true;
                        landOrRegrabTimer = landOrRegrabTimerMax;
                    }
                }
                else if (UnityEngine.Input.GetKey(KeyCode.D) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                {
                    facingr1 = true;
                    if (p1.velocity.x < runSpeed && !(rrunoffStall && lrunoffTimer > 0))
                    {
                        p1.AddForce(transform.right * runAccel);
                    }

                    if (lrunoffTimer == 0 && rrunoffStall == true)
                    {
                        runoff = true;
                        landOrRegrabTimer = landOrRegrabTimerMax;
                    }
                }
                else
                {
                    if (p1.velocity.x > walkSpeed || p1.velocity.x < -runSpeed)
                    {
                        state = "skid";
                        skidTimer = skidFrames;
                    }
                    else
                    {
                        state = "walk";
                    }
                }

                if (runoff)
                {
                    state = "airborn";
                    jumps--;
                    land = false;
                    runoff = false;
                    dashTimer = 0;
                    if (facingr1)
                    {
                        currentDir = 1;
                    }
                    else
                    {
                        currentDir = -1;
                    }
                    p1.position = p1.position + Vector3.right * currentDir * capsuleHOffset;
                    p1.AddForce(currentDir * Vector3.right * runSpeed);
                    //impart maxrunspeed in direction we are facing
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.A) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    rolltimer = 0;
                    rollDir = -1;
                    state = "roll";
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.D) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    rolltimer = 0;
                    rollDir = 1;
                    state = "roll";
                }

                if (UnityEngine.Input.GetKey(KeyCode.S) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    dodgeTimer = 0;
                    state = "dodge";
                }

                //alternate input
                if (UnityEngine.Input.GetMouseButton(1) && UnityEngine.Input.GetKey(KeyCode.F))
                {
                    dodgeTimer = 0;
                    state = "dodge";
                }

                break;


            case "jumpsquat":


                p1rend.material.color = Color.magenta;

                if (hopTimer > 0)
                {
                    hopTimer--;
                    if (hopTimer == 0)
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
                if (squatTimer > 0)
                {
                    squatTimer--;
                    if (squatTimer == 0)
                    {
                        if (fullhop)
                        {
                            p1.AddForce(transform.up * fhJumpHeight);
                        }
                        else
                        {
                            p1.AddForce(transform.up * shJumpHeight);
                        }
                        state = "airborn";
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
                        p1.AddForce(transform.right * walkAccel);
                    }
                }


                if (UnityEngine.Input.GetKeyDown(KeyCode.W) && jumpcd == 0 && jumps > 0 && !(land))
                {
                    p1.velocity = new Vector3(p1.velocity.x, 0, 0);
                    p1.AddForce(transform.up * aJumpHeight);

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
                    state = "attacking";
                    lag = 60;
                }


                if (land)
                {
                    //Debug.Log("hi");
                    if (p1.velocity.x > walkSpeed)
                    {
                        state = "run";
                    }
                    else
                    {
                        state = "walk";
                    }

                    jumps = jumpsMax;
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
                                state = "walk";
                                land = false;
                                runoff = false;
                                dashTimer = 0;
                                shieldTimer = 0;
                                dtrtimer = 0;
                                okCrawl = false;
                                standupTimer = 0;

                                jumps = jumpsMax;
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
                                    p1.AddForce(transform.right * walkAccel);
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
                                state = "airborn";

                                //land = false;
                                runoff = false;
                                dashTimer = 0;
                                shieldTimer = 0;
                                lag = 0;
                                dtrtimer = 0;
                                okCrawl = false;
                                standupTimer = 0;
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
                                p1.AddForce(transform.right * walkAccel);
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
                            state = "airborn";

                            //land = false;
                            runoff = false;
                            dashTimer = 0;
                            shieldTimer = 0;
                            lag = 0;
                            dtrtimer = 0;
                            okCrawl = false;
                            standupTimer = 0;
                        }
                    }
                }



                break;



            case "shield":
                p1rend.material.color = Color.blue;

                if (shieldStun > 0)
                {
                    shieldStun--;
                }

                if (shieldTimer > 0)
                {
                    shieldTimer--;
                    if (shieldTimer == 0)
                    {
                        inShield = true;
                        shieldStun = 4;
                    }
                }

                if (!(UnityEngine.Input.GetKey(KeyCode.F)) || !(UnityEngine.Input.GetMouseButton(0)))
                {
                    inShield = false;
                }

                if (UnityEngine.Input.GetKey(KeyCode.F) == false && shieldStun == 0)
                {
                    shieldTimer = 0;
                    state = "walk";
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
                    state = "walk";
                }


                break;

            case "dodge":
                p1rend.material.color = Color.white;
                if (dodgeTimer < dodgeTime)
                {
                    dodgeTimer++;
                    if (dodgeTimer == 1)
                    {
                        p1.velocity = Vector3.zero;
                    }
                    if (dodgeTimer == dodgeIntStart)
                    {
                        intangible = true;
                    }
                    if (dodgeTimer == dodgeIntEnd)
                    {
                        intangible = false;
                    }
                }
                else
                {
                    state = "walk";
                }

                break;

            case "hitstun":
                p1rend.material.color = Color.red;
                if (hitstunTimer > 0)
                {
                    //Debug.Log("ah shit 1", gameObject);
                    if (hitstunTimer == hitstun)
                    {
                        //Debug.Log("ah shit 2", gameObject);
                        isHit = false;
                        p1.velocity = Vector3.zero;
                        //p1.velocity = (currentHB.bkb+currentHB.skb*dmg)*currentHB.angle;
                        p1.AddForce((currentHB.bkb + currentHB.skb * dmg) * currentHB.angle);
                    }

                    hitstunTimer--;
                    //two frames of hitLAG give increased directional influence, otherwise, DI is low during hitstun

                    if (hitstun - hitstunTimer < 3)
                    {
                        if (UnityEngine.Input.GetKey(KeyCode.A))
                        {
                            p1.AddForce(-transform.right * airAccel * 1.9f);
                        }

                        if (UnityEngine.Input.GetKey(KeyCode.D))
                        {
                            p1.AddForce(transform.right * airAccel * 1.9f);
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
                if (hitstunTimer == 0)
                {
                    if (Physics.Raycast(transform.position, -Vector3.up, out hit, .01f))
                    {
                        if (hit.collider.tag == "stage")
                        {

                            state = "walk";
                            //land = false;
                            runoff = false;
                            dashTimer = 0;
                            shieldTimer = 0;
                            lag = 0;
                            dtrtimer = 0;
                            okCrawl = false;
                            standupTimer = 0;

                            jumps = jumpsMax;

                        }
                        else
                        {
                            // Debug.Log("out of hitstun 1", gameObject);
                            state = "airborn";
                            land = false;
                            runoff = false;
                            dashTimer = 0;
                            shieldTimer = 0;
                            lag = 0;
                            dtrtimer = 0;
                            okCrawl = false;
                            standupTimer = 0;

                            if (jumps == jumpsMax)
                            {
                                jumps--;
                            }
                        }
                    }
                    else
                    {
                        //Debug.Log("out of hitstun 2", gameObject);
                        state = "airborn";
                        land = false;
                        runoff = false;
                        dashTimer = 0;
                        shieldTimer = 0;
                        lag = 0;
                        dtrtimer = 0;
                        okCrawl = false;
                        standupTimer = 0;

                        if (jumps == jumpsMax)
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
                    state = "airborn";
                    jumps = jumpsMax - 1;
                    land = false;
                    runoff = false;
                    dashTimer = 0;
                    shieldTimer = 0;
                    lag = 0;
                    dtrtimer = 0;
                    okCrawl = false;
                    standupTimer = 0;

                }
                break;

            case "ledge":
                p1rend.material.color = Color.black;
                //Debug.Log("ledge");
                if (ledgeTimer > 0)
                {
                    intangible = true;
                    ledgeTimer--;
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
                            ledgeTimer2 = rGetupFrames;
                        }
                        if (UnityEngine.Input.GetKey(KeyCode.D) && facingr1)
                        {
                            nGetup = true;
                            ledgeTimer2 = nGetupFrames;
                        }
                        if (UnityEngine.Input.GetKey(KeyCode.A) && !facingr1)
                        {
                            nGetup = true;
                            ledgeTimer2 = nGetupFrames;
                        }
                        if (UnityEngine.Input.GetKey(KeyCode.D) && !facingr1)
                        {
                            rGetup = true;
                            ledgeTimer2 = rGetupFrames;
                        }

                        if (UnityEngine.Input.GetKey(KeyCode.S))
                        {
                            dGetup = true;
                        }
                        if (UnityEngine.Input.GetKey(KeyCode.W))
                        {
                            jGetup = true;
                            ledgeTimer2 = jGetupFrames;
                        }
                    }


                    //getup

                    if (nGetup)
                    {
                        if (ledgeTimer2 > 0)
                        {
                            if (ledgeTimer2 == nGetupFrames)
                            {
                                p1.position = ledgePos + (capsuleVOffset + .5f) * Vector3.up + 2 * currentDir * capsuleHOffset * Vector3.right;
                            }
                            ledgeTimer2--;
                        }
                        else
                        {
                            state = "walk";
                        }
                    }
                    if (jGetup)
                    {
                        if (ledgeTimer2 > 0)
                        {
                            if (ledgeTimer2 == jGetupFrames)
                            {
                                p1.position = ledgePos + (capsuleVOffset + .5f) * Vector3.up; //+ 2 * currentDir * capsuleHOffset * Vector3.right
                                p1.AddForce(Vector3.up * fhJumpHeight + Vector3.right * 50 * currentDir * airMaxSpeed);
                            }
                            ledgeTimer2--;
                        }
                        else
                        {
                            jumps--;
                            state = "airborn";
                        }
                    }
                    if (rGetup)
                    {
                        if (ledgeTimer2 > 0)
                        {
                            if (ledgeTimer2 == rGetupFrames)
                            {
                                p1.position = ledgePos + (capsuleVOffset + .5f) * Vector3.up + 2 * currentDir * capsuleHOffset * Vector3.right;
                                p1.AddForce(Vector3.right * currentDir * rollLength);
                            }
                            ledgeTimer2--;
                        }
                        else
                        {
                            state = "walk";
                        }
                    }
                    if (dGetup)
                    {
                        landOrRegrabTimer = landOrRegrabTimerMax;
                        state = "airborn";
                        jumps--;
                    }
                }
                break;

            case "skid":
                p1rend.material.color = Color.blue;
                if (skidTimer > 0)
                {
                    skidTimer--;
                    if (skidFrames - skidTimer < 6)
                    {
                        if (UnityEngine.Input.GetKey(KeyCode.A) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                        {
                            facingr1 = false;
                            if (p1.velocity.x > -runSpeed && !(lrunoffStall && lrunoffTimer > 0))
                            {
                                p1.AddForce(-transform.right * runAccel);
                            }

                            state = "run";
                        }
                        if (UnityEngine.Input.GetKey(KeyCode.D) && !(UnityEngine.Input.GetKey(KeyCode.F)))
                        {
                            facingr1 = true;
                            if (p1.velocity.x < runSpeed && !(rrunoffStall && lrunoffTimer > 0))
                            {
                                p1.AddForce(transform.right * runAccel);
                            }

                            state = "run";
                        }
                        if (UnityEngine.Input.GetKeyDown(KeyCode.A) && UnityEngine.Input.GetKey(KeyCode.F))
                        {
                            rolltimer = 0;
                            rollDir = -1;
                            state = "roll";
                        }

                        if (UnityEngine.Input.GetKeyDown(KeyCode.D) && UnityEngine.Input.GetKey(KeyCode.F))
                        {
                            rolltimer = 0;
                            rollDir = 1;
                            state = "roll";
                        }

                        if (UnityEngine.Input.GetKey(KeyCode.S) && UnityEngine.Input.GetKey(KeyCode.F))
                        {
                            dodgeTimer = 0;
                            state = "dodge";
                        }

                        //alternate input
                        if (UnityEngine.Input.GetMouseButton(1) && UnityEngine.Input.GetKey(KeyCode.F))
                        {
                            dodgeTimer = 0;
                            state = "dodge";
                        }
                    }
                }
                else
                {
                    state = "walk";
                }
                break;

            case "freeze":
                p1rend.material.color = Color.black;
                break;


            case "crawl":
                p1rend.material.color = Color.magenta;
                if (standupTimer == 0)
                {
                    state = "walk";
                }
                if (UnityEngine.Input.GetKey(KeyCode.S) == false)
                {
                    standupTimer--;
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
        if (col.gameObject.tag == "stage" && Physics.Raycast(transform.position, -Vector3.up, out hit, capsuleVOffset) && landOrRegrabTimer == 0)
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
            if ((currentHB.playerNum != thisPlayer) && currentHB.special == "" && isHit == false && inShield == false && intangible == false)
            {
                //Debug.Log("extra dildos");
                p1rend.material.color = Color.magenta;
                isHit = true;
                hitstun = 2 * currentHB.dmg; //!!!! this is a temporary hitstun formula!!!!
                hitstunTimer = hitstun;
                dmg += currentHB.dmg;
                //Debug.Log("hs"+hitstun, gameObject);
                //Debug.Log("hst"+hitstunTimer, gameObject);
            }

        }

        if (collider.tag == "lrunoff" && (state == "walk" || state == "run" || state == "dash" || state == "skid"))
        {
            lrunoffTimer = runoffTimerMax;
            p1.velocity = Vector3.zero;
            lrunoffStall = true;
            temptrans = collider.GetComponent<Transform>();
            p1.transform.position = temptrans.position + Vector3.up * capsuleVOffset;
        }

        if (collider.tag == "rrunoff" && (state == "walk" || state == "run" || state == "dash" || state == "skid"))
        {
            rrunoffTimer = runoffTimerMax;
            p1.velocity = Vector3.zero;
            rrunoffStall = true;
            temptrans = collider.GetComponent<Transform>();
            p1.transform.position = temptrans.position + Vector3.up * capsuleVOffset;
        }

        if (collider.tag == "blastline")
        {
            isDead = true;
            respawntimer = respawnTime;
        }

        if (collider.tag == "lledge" && landOrRegrabTimer == 0 && !(UnityEngine.Input.GetKey(KeyCode.S)) && !(state == "attacking"))
        {
            ledgeGrab = true;
            facingr1 = true;
            jumps = jumpsMax;
            ledgeTimer = ledgelag;
            nGetup = false;
            jGetup = false;
            rGetup = false;
            dGetup = false;
            p1.velocity = Vector3.zero;
            temptrans = collider.GetComponent<Transform>();
            p1.transform.position = temptrans.position - Vector3.right * capsuleHOffset;
            ledgePos = temptrans.position - Vector3.right * capsuleHOffset;
            currentDir = 1;
            ledgeTimer2 = 0;
        }
        if (collider.tag == "rledge" && landOrRegrabTimer == 0 && !(UnityEngine.Input.GetKey(KeyCode.W)) && !(state == "attacking"))
        {
            ledgeGrab = true;
            facingr1 = false;
            jumps = jumpsMax;
            ledgeTimer = ledgelag;
            nGetup = false;
            jGetup = false;
            rGetup = false;
            dGetup = false;
            p1.velocity = Vector3.zero;
            temptrans = collider.GetComponent<Transform>();
            p1.transform.position = temptrans.position + Vector3.right * capsuleHOffset;
            ledgePos = temptrans.position + Vector3.right * capsuleHOffset;
            currentDir = -1;
            ledgeTimer2 = 0;
        }

    }
    /*
    void onTriggerStay (Collider collider)
    {
        Debug.Log("stay");
        if (collider.tag == "lrunoff")
        {
            Debug.Log("stay");
            if (lrunoffTimer > 0)
            {
                lrunoffTimer--;
                
            }
            
        }
        if (collider.tag == "rrunoff")
        {
            if (rrunoffTimer > 0)
            {
                rrunoffTimer--;
            }
        }

    }
    */

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "lrunoff")
        {
            //Debug.Log("exit");
            //lrunoffTimer = runoffTimerMax;
            lrunoffTimer = 0;
            lrunoffStall = false;
            runoff = false;
        }
        if (collider.tag == "rrunoff")
        {
            //rrunoffTimer = runoffTimerMax;
            rrunoffTimer = 0;
            rrunoffStall = false;
            runoff = false;
        }
    }

    public void EnQ(hitbox h) //once a move is copmleted, a hitbox is added to the unused hitbox list
    {
        hbCompList.Add(h);
    }

    public void DeQ(int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction,
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
