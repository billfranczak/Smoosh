using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameData : ScriptableObject
{

    //walk
    public float walkAccel;
    public float walkMaxSpeed;
    public float runAccel;
    public float runSpeed;
    public float crawlAccel;

    //jump
    public float airMaxSpeed;
    public float airAccel;
    public float fallSpdMax;
    public float fallAccel;
    public float fhJumpHeight;
    public float shJumpHeight;
    public float aJumpHeight;
    public int jumpCdMax;
    public int jumpsMax;
    public int squatTimerMax;
    public int hopTimerMax; //cannot be more than maxsquattimer

    //run/dash
    public int dashWindow;
    public int dashl;
    public int dashToRun;

    //crawl
    public int crawlToStand;

    //shield
    public int shieldUp;
    public int maxShield;

    //dodge
    public int dodgeTime;
    public int dodgeIntStart;
    public int dodgeIntEnd;

    //airdodge
    public int airDodgeTime;
    public int airDodgeIntStart;
    public int airDodgeIntEnd;

    //roll
    public int rollTime;
    public float rollLength;
    public int rollStart;
    public int rollIntStart;
    public int rollIntEnd;

    //size
    public float capsuleVOffset;
    public float capsuleHOffset;
    public float weight;

    //ledge
    public int ledgeIntang;
    public int nGetupFrames;
    public int jGetupFrames;
    public int rGetupFrames;

    //mis
    public int skidFrames;
    public int hbMax;
    public int bbMax;
    

    
    public FrameData(string character)
    {
        switch (character)
        {
            case "TMan":

                //walk
                walkAccel = 30;
                walkMaxSpeed = 3;
                runAccel = 55;
                runSpeed = 8;
                crawlAccel = 8;


                //jump
                airMaxSpeed = 3;
                airAccel = 20;
                fallSpdMax = 3;
                fallAccel = 10;
                fhJumpHeight = 350;
                shJumpHeight = 250;
                aJumpHeight = 350;
                jumpCdMax = 1;
                jumpsMax = 2;
                squatTimerMax = 6;
                hopTimerMax = 5; //cannot be more than maxsquattimer

                //run/dash
                dashWindow = 10;
                dashl = 400;
                dashToRun = 7;

                //crawl
                crawlToStand = 3;

                //shield
                shieldUp = 3;
                maxShield = 100;

                //dodge
                dodgeTime = 30;
                dodgeIntStart = 7;
                dodgeIntEnd = 22;

                //dodge
                airDodgeTime = 30;
                airDodgeIntStart = 7;
                airDodgeIntEnd = 22;

                //roll
                rollTime = 30;
                rollLength = 400;
                rollStart = 7;
                rollIntStart = 7;
                rollIntEnd = 22;

                //size
                capsuleVOffset = .31f;
                capsuleHOffset = .2f;
                weight = 100;

                //ledge
                ledgeIntang = 5;
                nGetupFrames = 10;
                jGetupFrames = 20;
                rGetupFrames = 30;

                //skid
                skidFrames = 13;

                //misc
                hbMax = 10;
                bbMax = 10;

                break;



        }
            
    }

/*
    //walk
    public float walkAccel = 30;
    public float walkMaxSpeed = 3;
    public float runAccel = 55;
    public float runSpeed = 8;
    public float crawlAccel = 8;

    //jump
    public float airMaxSpeed = 3;
    public float airAccel = 20;
    public float fallSpdMax = 3;
    public float fallAccel = 20;
    public float fhJumpHeight = 350;
    public float shJumpHeigt = 250;
    public float aJumpHeigt = 350;
    public int jumpCdMax = 1;
    public int jumpsMax = 2;
    public int squatTimerMax = 6;
    public int hopTimerMax = 5; //cannot be more than maxsquattimer

    //run/dash
    public int dashWindow = 10;
    public int dashl = 400;
    public int dashToRun = 7;

    //crawl
    public int crawlToStand = 3;

    //shield
    public int shieldUp = 3;
    public int maxShield = 100;

    //dodge
    public int dodgeTime = 30;
    public int dodgeIntStart = 7;
    public int dodgeIntEnd = 22;

    //roll
    public int rollTime = 30;
    public float rollLength = 400;
    public int rollStart = 7;
    public int rollIntStart = 7;
    public int rollIntEnd = 22;

    //size
    public float capsuleVOffset = .31f;
    public float capsuleHOffset = .2f;

    //ledge
    public int ledgeIntang = 5;
    public int nGetupFrames = 10;
    public int jGetupFrames = 20;
    public int rGetupFrames = 30;

    //skid
    public float skidFrames = 13;

    */
}
