using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

    public GameObject hbqueue;
    HBQ hbq;

    public Vector3 pos;
    public Rigidbody p1;
    public Renderer p1rend;
    public string p1s;
    public float wgt;
    public float walkspd;
    public float crawlspd;
    public float runspd;
    public float dashl;
    public float airspd;
    public float flty;
    public float fallspd;
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


    // Use this for initialization
    void Start () {
		p1 = GetComponent<Rigidbody>();
        p1rend= GetComponent<Renderer>();

        p1rend.material.color = Color.yellow;
        p1s = "walk";

        walkspd = 10;
        runspd = 15;
        crawlspd = 8;

        airspd = 5;
        fallspd = 10;
        gjumphgt = 350;
        ajumphgt = 250;
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

        switch(p1s)
        {
            case "walk":
                p1rend.material.color = Color.yellow;

                
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
                    p1s = "airborn";
                    jumps--;
                    land = false;
                    runoff = false;
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
                        p1.AddForce(-transform.right * walkspd);
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
                        p1.AddForce(transform.right * walkspd);
                    }
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.W) && jumps>0)
                {
                    p1.AddForce(transform.up * gjumphgt);
                    p1s="airborn";
                    jumpcd = jumpmaxcd;
                    jumps--;
                    land = false;
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.S))
                {
                    dashtimer = dashwindow;
                }

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
                } else
                if (UnityEngine.Input.GetKey(KeyCode.A)) {
                    facingr1 = false;
                    p1.AddForce(-transform.right * runspd);
                }
                else if (UnityEngine.Input.GetKey(KeyCode.D)) {
                    facingr1 = true;
                    p1.AddForce(transform.right * runspd);
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
                    p1.AddForce(-transform.right * airspd);
                }

                if (UnityEngine.Input.GetKey(KeyCode.D))
                {
                    p1.AddForce(transform.right * airspd);
                }

                if (UnityEngine.Input.GetKeyDown(KeyCode.W) && jumpcd==0 && jumps >0)
                {
                    p1.AddForce(transform.up * ajumphgt);
                    jumps--;
                }
                if (UnityEngine.Input.GetKey(KeyCode.S))
                {
                    p1.AddForce(-transform.up * fallspd);
                }
                

                if (land)
                {
                    p1s = "walk";
                    jumps = maxjumps;
                }
                
                break;



            case "attacking":
                p1rend.material.color = Color.green;
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
        if (col.gameObject.tag == "stage" ) {
            land = true;
        }


    }

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



}
