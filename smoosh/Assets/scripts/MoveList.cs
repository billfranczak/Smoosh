using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveList : ScriptableObject
{

    ChallengerCon p1;

    public Action<int> jab;
    public Action<int> grab;
    public Action<int> fweak;
    public Action<int> uweak;
    public Action<int> dweak;
    public Action<int> fstrong;
    public Action<int> ustrong;
    public Action<int> dstrong;
    public Action<int> nspec;
    public Action<int> fspec;
    public Action<int> uspec;
    public Action<int> dspec;
    public Action<int> nair;
    public Action<int> fair;
    public Action<int> bair;
    public Action<int> uair;
    public Action<int> dair;
    public Action<int> dash;

    public Action<int> resUpdate;
    public int numResources;

    public MoveList (ChallengerCon player, string character)
    {

        p1 = player;
        switch (character)
        {
            
            case "TMan":

                jab = TMANjab;
                grab = TMANgrab;
                fweak = TMANfweak;
                uweak = TMANuweak;
                dweak = TMANdweak;
                fstrong = TMANfstrong;
                ustrong = TMANustrong;
                dstrong = TMANdstrong;
                nair = TMANnair;
                fair = TMANfair;
                bair = TMANbair;
                uair = TMANuair;
                dair = TMANdair;
                nspec = TMANnspec;
                fspec = TMANfspec;
                uspec = TMANuspec;
                dspec = TMANdspec;
                numResources = 2;
                resUpdate = TMANresUpdate;
               

                break;
        }
    }


    // TMAN //////////////////////////////////////////////////////////////


    public void TMANfweak(int na)
    {
        blank();
    }

    public void TMANuweak(int na)
    {
        blank();
    }

    public void TMANdweak(int na)
    {
        blank();
    }

    public void TMANfstrong(int na)
    {
        blank();
    }

    public void TMANustrong(int na)
    {
        blank();
    }

    public void TMANdstrong(int na)
    {
        blank();
    }

    public void TMANnair(int na)
    {
        blank();
    }

    public void TMANfair(int na)
    {
        blank();
    }

    public void TMANbair(int na)
    {
        blank();
    }
    public void TMANuair(int na)
    {
        blank();
    }
    public void TMANdair(int na)
    {
        blank();
    }

    public void TMANnspec(int na)
    {
        if (p1.resource[1]>0)
        {
            p1.resource[1]--;
            p1.state = "attacking";
            p1.lag = 7;
            p1.bbDeQ();
            BlankBox bb = p1.currentBB;
            bb.duration = 4;
            bb.act= delegate (int n) 
            {
                bb.duration--;
                if (bb.duration == 1)
                {
                    //
                    p1.transform.position += Vector3.right*((p1.mouseX-p1.screenPos.x)/ (float) Math.Sqrt((p1.mouseX - p1.screenPos.x)* (p1.mouseX - p1.screenPos.x)+ (p1.mouseY - p1.screenPos.y) * (p1.mouseY - p1.screenPos.y)));
                    p1.transform.position += Vector3.up * ((p1.mouseY - p1.screenPos.y) / (float)Math.Sqrt((p1.mouseX - p1.screenPos.x) * (p1.mouseX - p1.screenPos.x) + (p1.mouseY - p1.screenPos.y) * (p1.mouseY - p1.screenPos.y)));
                }
                if (bb.duration ==0)
                {
                    p1.bbEnQ(bb);
                    bb.act = bb.blank;
                }
            };
        }

    }

    public void TMANfspec(int na)
    {
        blank();
    }

    public void TMANuspec(int na)
    {
        blank();
    }

    public void TMANdspec(int na)
    {
        p1.state = "attacking";
        p1.lag = 10;
        p1.DeQ(15, .5f, 200, Vector3.right * p1.currentDir, false, Vector3.zero, 0,
            2, Vector3.up, 10, 0, false, 0, 150, 10);
        //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
        //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb
    }

    public void TMANjab(int na)
    {
        p1.state = "attacking";
        p1.lag = 60;
        p1.DeQ(15, .2f, 55, Vector3.right * p1.currentDir, true, Vector3.zero, 0,
        1, Vector3.up, 5, 5, false, 0, 50, 10);
        //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
        //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb
    }
    public void TMANgrab(int na)
    {
        blank();
    }

    public void TMANresUpdate(int na)
    {
        //Debug.Log("update");
        if (p1.resource[0]<120)
        {
            p1.resource[0]++;
        }
        else
        {
            if (p1.resource[1]<2)
            {
                p1.resource[1]++;
                p1.resource[0] = 0;
            }
            else
            {
                p1.resource[0] = 0;
            }
        }
    }



    public void blank() { }








}
