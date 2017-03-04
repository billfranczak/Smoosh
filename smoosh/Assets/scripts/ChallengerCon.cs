using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChallengerCon : MonoBehaviour
{

    
    public FrameData frameData;
    public MoveList moveList;
    public Profiles profile;
    public string prof;
    public Camera camera;

    public BlankBox bbox;

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
    public Action<int> bAir;
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

    public int bbMax;
    public GameObject bObj;
    public List<GameObject> bbObjList;
    public List<BlankBox> bbCompList;
    BlankBox currentBB;


    public bool shiftwalk;
    public KeyCode left;
    public KeyCode right;
    public KeyCode down;
    public KeyCode jump;
    public KeyCode modifier1;
    public KeyCode modifier2;
    public KeyCode button1Input;
    public KeyCode button2Input;
    public KeyCode shiftWalkInput;
    public KeyCode altDashWindow;

    //assignments
    public string mb1;
    public string mb2;
    public string button1Assign;
    public string button2Assign;
    public string mod1mb1;
    public string mod1mb2;
    public string mod1button1;
    public string mod1button2;
    public string mod2mb1;
    public string mod2mb2;
    public string mod2button;
    public string mod2button1;
    public string mod2button2;
    public string mod1lr;
    public string mod1down;
    public string mod2lr;
    public string mod2down;



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


    public bool leftInput;
    public bool rightInput;
    public bool downInput;
    public bool dashInput;
    public bool jumpInput;
    public bool jumpInputDown;
    public bool shieldInput;
    public bool dodgeInput;
    public bool rollInput;
    public bool jabInput;
    public bool grabInput;
    public bool fSpecInput;
    public bool dSpecInput;
    public bool uSpecInput;
    public bool fAttackInput;
    public bool dAttackInput;
    public bool uAttackInput;
    public bool leftMod;
    public bool rightMod;

    /*
    public bool fWeakInput;
    public bool dWeakInput;
    public bool uWeakInput;
    public bool fStrongInput;
    public bool dStrongInput;
    public bool uStrongInput;
    public bool fSpecInput;
    public bool dSpecInput;
    public bool uSpecInput;
    public bool fAirInput;
    public bool bAirInput;
    public bool dAirInput;
    public bool uAirInput;
    */
    public bool attack;
    public bool special;

    public float mouseX;
    public float mouseY;
    public bool mouseRight;

    public Vector3 screenPos;


    // Use this for initialization
    void Start()
    {
        camera = Camera.main;
        
        frameData = new FrameData("TMan"); //move to constructor
        moveList = new MoveList(this,"TMan");
        profile = new Profiles();
        dmg = 0;
        thisPlayer = 1;
        p1 = GetComponent<Rigidbody>();
        p1rend = animMesh.GetComponent<Renderer>();

        p1rend.material.color = Color.yellow;
        state = "walk";

        prof = "ccoma";
        int ind = profile.profileNames.IndexOf(prof);
        shiftwalk = profile.shiftWalk[ind];
        left = profile.left[ind];
        right = profile.right[ind];
        down = profile.down[ind];
        jump = profile.jump[ind];
        modifier1 = profile.modifier1[ind];
        modifier2 = profile.modifier2[ind];
        button1Input = profile.button1Input[ind];
        button2Input = profile.button2Input[ind];
        shiftWalkInput = profile.shiftWalkInput[ind];
        altDashWindow = profile.altDashWindown[ind];

        //assignments
        mb1 = profile.mb1[ind];
        mb2 = profile.mb2[ind];
        button1Assign = profile.button1Assign[ind];
        button2Assign = profile.button2Assign[ind];
        mod1mb1 = profile.mod1mb1[ind];
        mod1mb2 = profile.mod1mb2[ind];
        mod1button1 = profile.mod1button1[ind];
        mod1button2 = profile.mod1button2[ind];
        mod2mb1 = profile.mod2mb1[ind];
        mod2mb2 = profile.mod2mb2[ind];
        //mod2button = profile.mod2button[ind];
        mod2button1 = profile.mod2button1[ind];
        mod2button2 = profile.mod2button2[ind];
        mod1lr = profile.mod1lr[ind];
        mod1down = profile.mod1down[ind];
        mod2lr = profile.mod2lr[ind];
        mod2down = profile.mod2down[ind];

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
        bbox = new BlankBox(this);
        //bbq
        for (int i=0; i<bbMax; i++)
        {
            bbCompList.Add(Instantiate(bbox));
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
        bAir = moveList.bair;
        uAir = moveList.uair;
        dAir = moveList.dair;
        dashAttack = moveList.dash;

    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;


        //input buffer
        attack = false;
        special = false;

        if (UnityEngine.Input.GetKey(button1Input))
        {
            inputSelect(button1Assign);
        }
        if (UnityEngine.Input.GetKey(button2Input))
        {
            inputSelect(button2Assign);
        }
        if (UnityEngine.Input.GetMouseButton(0))
        {
            inputSelect(mb1);
        }
        if (UnityEngine.Input.GetMouseButton(1))
        {
            inputSelect(mb2);
        }

        if (UnityEngine.Input.GetKey(modifier1) && UnityEngine.Input.GetKey(button1Input))
        {
            inputSelect(mod1button1);
        }
        if (UnityEngine.Input.GetKey(modifier1) && UnityEngine.Input.GetKey(button2Input))
        {
            inputSelect(mod1button2);
        }
        if (UnityEngine.Input.GetKey(modifier1) && UnityEngine.Input.GetMouseButton(0))
        {
            inputSelect(mod1mb1);
        }
        if (UnityEngine.Input.GetKey(modifier1) && UnityEngine.Input.GetMouseButton(1))
        {
            inputSelect(mod1mb2);
        }
        if (UnityEngine.Input.GetKey(modifier1) && (UnityEngine.Input.GetKey(left)|| UnityEngine.Input.GetKey(right)))
        {
            inputSelect(mod1lr);
        }
        if (UnityEngine.Input.GetKey(modifier1) && UnityEngine.Input.GetKey(down))
        {
            inputSelect(mod1down);
        }

        if (UnityEngine.Input.GetKey(modifier2) && UnityEngine.Input.GetKey(button1Input))
        {
            inputSelect(mod2button1);
        }
        if (UnityEngine.Input.GetKey(modifier2) && UnityEngine.Input.GetKey(button2Input))
        {
            inputSelect(mod2button2);
        }
        if (UnityEngine.Input.GetKey(modifier2) && UnityEngine.Input.GetMouseButton(0))
        {
            inputSelect(mod2mb1);
        }
        if (UnityEngine.Input.GetKey(modifier2) && UnityEngine.Input.GetMouseButton(1))
        {
            inputSelect(mod2mb2);
        }
        if (UnityEngine.Input.GetKey(modifier2) && (UnityEngine.Input.GetKey(left) || UnityEngine.Input.GetKey(right)))
        {
            inputSelect(mod2lr);
        }
        if (UnityEngine.Input.GetKey(modifier2) && UnityEngine.Input.GetKey(down))
        {
            inputSelect(mod2down);
        }

        mouseX = Input.mousePosition.x/Screen.width;
        mouseY = Input.mousePosition.y/Screen.height;
        screenPos = camera.WorldToViewportPoint(transform.position);

        if (attack)
        {
            special = false;
            //right quad
            if (mouseY >-(mouseX-screenPos.x)+ screenPos.y && mouseY < (mouseX - screenPos.x) + screenPos.y)
            {
                fAttackInput = true;
                mouseRight = true;
            }
            //up quad
            if (mouseY > -(mouseX - screenPos.x) + screenPos.y && mouseY >= (mouseX - screenPos.x) + screenPos.y)
            {
                uAttackInput = true;
            }
            //down quad
            if (mouseY <= -(mouseX - screenPos.x) + screenPos.y && mouseY < (mouseX - screenPos.x) + screenPos.y)
            {
                dAttackInput = true;
            }
            //left quad
            if (mouseY <= -(mouseX - screenPos.x) + screenPos.y && mouseY >= (mouseX - screenPos.x) + screenPos.y)
            {
                fAttackInput = true;
            }

        }

        if (special)
        {
            //right quad
            if (mouseY > -(mouseX - screenPos.x) + screenPos.y && mouseY < (mouseX - screenPos.x) + screenPos.y)
            {
                fSpecInput = true;
                //Debug.Log("rspec"+mouseX+" "+mouseY + "pos "+ screenPos.x + ","+ screenPos.y );
            }
            //up quad
            if (mouseY > -(mouseX - screenPos.x) + screenPos.y && mouseY >= (mouseX - screenPos.x) + screenPos.y)
            {
                uSpecInput = true;
                //Debug.Log("uspec" + mouseX + " " + mouseY + "pos " + screenPos.x + "," + screenPos.y);
            }
            //down quad
            if (mouseY <= -(mouseX - screenPos.x) + screenPos.y && mouseY < (mouseX - screenPos.x) + screenPos.y)
            {
                dSpecInput = true;
                //Debug.Log("dspec" + mouseX + " " + mouseY + "pos " + screenPos.x + "," + screenPos.y);
            }
            //left quad
            if (mouseY <= -(mouseX - screenPos.x) + screenPos.y && mouseY >= (mouseX - screenPos.x) + screenPos.y)
            {
                fSpecInput = true;
               // Debug.Log("lspec" + mouseX + " " + mouseY + "pos " + screenPos.x + "," + screenPos.y);
            }
        }


        if (UnityEngine.Input.GetKey(left) && !(UnityEngine.Input.GetKey(modifier1) || UnityEngine.Input.GetKey(modifier2)))
        {
            leftInput = true;
        }
        if (UnityEngine.Input.GetKey(right) && !(UnityEngine.Input.GetKey(modifier1) || UnityEngine.Input.GetKey(modifier2)))
        {
            rightInput = true;
        }
        if (UnityEngine.Input.GetKey(down) && !(UnityEngine.Input.GetKey(modifier1) || UnityEngine.Input.GetKey(modifier2)))
        {
            downInput = true;
        }
        if (UnityEngine.Input.GetKeyDown(left) || UnityEngine.Input.GetKeyDown(right) && !(UnityEngine.Input.GetKey(modifier1) || UnityEngine.Input.GetKey(modifier2)))
        {
            dashInput = true;
        }
        if (UnityEngine.Input.GetKey(jump) && !(attack || special))
        {
            jumpInput = true;
        }
        if (UnityEngine.Input.GetKeyDown(jump) && !(attack || special))
        {
            jumpInputDown = true;
        }
        if (UnityEngine.Input.GetKey(left) )
        {
            leftMod = true;
        }

        if (UnityEngine.Input.GetKey(right) )
        {
            rightMod = true;
        }




        //high priority

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
                if (leftInput)
                {
                    facingr1 = false;
                    
                    if (dashTimer > 0 && dashInput)
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

                if (rightInput)
                {
                    facingr1 = true;
                    
                    if (dashTimer > 0 && dashInput)
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



                if (jumpInput && jumps > 0)
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

                if (UnityEngine.Input.GetKeyDown(altDashWindow))
                {
                    dashTimer = dashWindow;
                }

                if (downInput && dashTimer == 0)
                {
                    standupTimer = crawlToStand;
                    state = "crawl";
                }

                if (shieldInput && shieldHealth > 0)
                {
                    state = "shield";
                    dashTimer = 0;
                    shieldTimer = 3;
                }

                if (facingr1)
                {
                    currentDir = 1;
                }
                else
                {
                    currentDir = -1;
                }

                if (jabInput) //check cooldown, charges
                {
                    jab(0);
                }

                if (grabInput)
                {
                    grab(0);
                }

                if (fAttackInput)
                {
                    fWeak(0);
                }
                if (uAttackInput)
                {
                    uWeak(0);
                }
                if (dAttackInput)
                {
                    dWeak(0);
                }

                if (fSpecInput)
                {
                    fSpecial(0);
                }
                if (uSpecInput)
                {
                    uSpecial(0);
                }
                if (dSpecInput)
                {
                    dSpecial(0);
                    Debug.Log("dspec");
                }


                if (rollInput && leftMod)
                {
                    rolltimer = 0;
                    rollDir = -1;
                    state = "roll";
                }

                if (rollInput && rightMod)
                {
                    rolltimer = 0;
                    rollDir = 1;
                    state = "roll";
                }

                if (dodgeInput)
                {
                    dodgeTimer = 0;
                    state = "dodge";
                }





                //doubletap dash

                if (dashInput && dashTimer == 0)
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
                    if (leftInput)
                    {
                        state = "run";
                        p1.AddForce(-Vector3.right * runSpeed);
                    }
                    if (rightInput)
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

                if (jumpInput && jumps > 0)
                {
                    state = "jumpsquat";
                    jumpcd = jumpCdMax;
                    jumps--;
                    land = false;
                    squatTimer = squatTimerMax;
                    hopTimer = hopTimerMax;
                }
                else
                if (leftInput)
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
                else if (rightInput)
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

                if (rollInput && leftMod)
                {
                    rolltimer = 0;
                    rollDir = -1;
                    state = "roll";
                }

                if (rollInput && rightMod)
                {
                    rolltimer = 0;
                    rollDir = 1;
                    state = "roll";
                }

                if (dodgeInput)
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
                        if (jumpInput)
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
                        //Debug.Log("jumpSquat");
                        if (fullhop)
                        {
                            //Debug.Log("FH");
                            p1.AddForce(transform.up * fhJumpHeight);
                        }
                        else
                        {
                            //Debug.Log("SH");
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

                if (leftInput)
                {
                    //p1.AddForce(-transform.right * airspd);
                    if (p1.velocity.x > -airMaxSpeed)
                    {
                        p1.AddForce(-transform.right * airAccel);
                    }
                }

                if (rightInput)
                {
                    if (p1.velocity.x < airMaxSpeed)
                    {
                        p1.AddForce(transform.right * walkAccel);
                    }
                }


                if (jumpInputDown && jumpcd == 0 && jumps > 0 && !(land))
                {
                    p1.velocity = new Vector3(p1.velocity.x, 0, 0);
                    p1.AddForce(transform.up * aJumpHeight);

                    jumps--;
                    //Debug.Log("DJ");
                }
                if (downInput)
                {
                    if (p1.velocity.y > -fallSpdMax)
                    {
                        p1.AddForce(-transform.up * fallAccel);
                    }
                }

                if (facingr1)
                {
                    currentDir = 1;
                }
                else
                {
                    currentDir = -1;
                }

                if (jabInput) //check cooldown, charges
                {
                    nAir(0);
                }

                if (grabInput)
                {
                    grab(0);
                }

                if (fAttackInput)
                {
                    if (mouseRight == facingr1)
                    {
                        fAir(0);
                    } 
                    else
                    {
                        bAir(0);
                    }
                }

                if (uAttackInput)
                {
                    uAir(0);
                }
                if (dAttackInput)
                {
                    dAir(0);
                }

                if (fSpecInput)
                {
                    fSpecial(0);
                }
                if (uSpecInput)
                {
                    uSpecial(0);
                }
                if (dSpecInput)
                {
                    dSpecial(0);
                    //Debug.Log("dspec");
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

                            if (leftInput)
                            {
                                if (p1.velocity.x > -airMaxSpeed)
                                {
                                    p1.AddForce(-transform.right * airAccel);
                                };
                            }

                            if (rightInput)
                            {
                                if (p1.velocity.x < airMaxSpeed)
                                {
                                    p1.AddForce(transform.right * walkAccel);
                                }
                            }

                            if (downInput)
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
                        if (leftInput)
                        {
                            if (p1.velocity.x > -airMaxSpeed)
                            {
                                p1.AddForce(-transform.right * airAccel);
                            }
                        }

                        if (rightInput)
                        {
                            if (p1.velocity.x < airMaxSpeed)
                            {
                                p1.AddForce(transform.right * walkAccel);
                            }
                        }

                        if (downInput)
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

                if (shieldInput)
                {
                    inShield = false;
                }

                if (shieldInput == false && shieldStun == 0)
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
                        if (leftMod)
                        {
                            p1.AddForce(-transform.right * airAccel * 1.9f);
                        }

                        if (rightMod)
                        {
                            p1.AddForce(transform.right * airAccel * 1.9f);
                        }
                    }
                    else
                    {
                        if (leftMod)
                        {
                            p1.AddForce(-transform.right * airAccel * .1f);
                        }

                        if (rightMod)
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
                        if (leftInput && facingr1)
                        {
                            rGetup = true;
                            ledgeTimer2 = rGetupFrames;
                        }
                        if (rightInput && facingr1)
                        {
                            nGetup = true;
                            ledgeTimer2 = nGetupFrames;
                        }
                        if (leftInput && !facingr1)
                        {
                            nGetup = true;
                            ledgeTimer2 = nGetupFrames;
                        }
                        if (rightInput && !facingr1)
                        {
                            rGetup = true;
                            ledgeTimer2 = rGetupFrames;
                        }

                        if (downInput)
                        {
                            dGetup = true;
                        }
                        if (jumpInput)
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
                        if (leftInput)
                        {
                            facingr1 = false;
                            if (p1.velocity.x > -runSpeed && !(lrunoffStall && lrunoffTimer > 0))
                            {
                                p1.AddForce(-transform.right * runAccel);
                            }

                            state = "run";
                        }
                        if (rightInput)
                        {
                            facingr1 = true;
                            if (p1.velocity.x < runSpeed && !(rrunoffStall && lrunoffTimer > 0))
                            {
                                p1.AddForce(transform.right * runAccel);
                            }

                            state = "run";
                        }
                        if (rollInput && leftMod)
                        {
                            rolltimer = 0;
                            rollDir = -1;
                            state = "roll";
                        }

                        if (rollInput && rightMod)
                        {
                            rolltimer = 0;
                            rollDir = 1;
                            state = "roll";
                        }

                        if (dodgeInput)
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
                if (downInput == false)
                {
                    standupTimer--;
                }
                if (leftInput)
                {
                    p1.AddForce(-transform.right * crawlspd);
                }
                if (rightInput)
                {
                    p1.AddForce(transform.right * crawlspd);
                }
                break;


        }

        //if not hold buffer
        if (0==0)
        {
            leftInput = false;
            rightInput = false;
            downInput = false;
            dashInput = false;
            jumpInput = false;
            jumpInputDown = false;
            shieldInput = false;
            dodgeInput = false;
            rollInput = false;
            jabInput = false;
            grabInput = false;
            fSpecInput = false;
            dSpecInput = false;
            uSpecInput = false;
            fAttackInput = false;
            dAttackInput = false;
            uAttackInput = false;

            leftMod = false;
            rightMod = false;

            attack = false;
            special = false;
            mouseRight = false;
        }
        if( facingr1)
        {
            //Change Facing direction, Hacked together and hard coded to shit plz fix
            animMesh.transform.parent.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        if (!facingr1)
        {
            //Change Facing direction, Hacked together and hard coded to shit plz fix
            animMesh.transform.parent.transform.localEulerAngles = new Vector3(0, -90, 0);
        }

    }//Workflow End of Update

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

    public void bbEnQ(BlankBox b)
    {
        bbCompList.Add(b);
    }

    public void DeQ(int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction,
        int playerNum, Vector3 angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb)
    {
        //Debug.Log("1", gameObject);
        currentHB = hbCompList[0]; //refactor potential

        //Question??? if we take the 0 index out of a list does that make the 1 index roll down to 0???? O>o
        //--yes

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

    public void bbDeQ (Action<int> a)
    {
        currentBB = bbCompList[0];
        bbCompList.Remove(bbCompList[0]);
        currentBB.act = a;
    }





    public void inputSelect(string input)
    {
        switch (input)
        {
            case "jab":
                jabInput = true;
                break;
            case "grab":
                grabInput = true;
                break;
            case "dodge":
                dodgeInput = true;
                break;
            case "roll":
                rollInput = true;
                break;
            case "attack":
                attack = true;
                break;
            case "special":
                special = true;
                break;
            case "shield":
                shieldInput = true;
                break;
        }
    }

}
