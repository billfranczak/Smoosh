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
    public Action<int> fspec;
    public Action<int> uspec;
    public Action<int> dspec;
    public Action<int> nair;
    public Action<int> fair;
    public Action<int> bair;
    public Action<int> uair;
    public Action<int> dair;
    public Action<int> dash;

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
                fspec = TMANfspec;
                uspec = TMANuspec;
                dspec = TMANdspec;
               

                break;
        }
    }

    /*
    public void TMANjab()
    {
        this.state = "attacking";
        this.lag = 10;
        this.hbq = hbqueue.GetComponent<HBQ>();
        this.hbq.DeQ(15, .5f, 200, Vector3.right, false, Vector3.zero,
            2, Vector3.up, 10, 0, false, 0, 150, 10);
        //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
        //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb
    }
    */



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
        p1.DeQ(15, .5f, 200, Vector3.right * p1.currentDir, false, Vector3.zero,
            2, Vector3.up, 10, 0, false, 0, 150, 10);
        //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
        //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb
    }

    public void TMANjab(int na)
    {
        p1.state = "attacking";
        p1.lag = 60;
        p1.DeQ(15, .2f, 55, Vector3.right * p1.currentDir, true, Vector3.zero,
        1, Vector3.up, 5, 5, false, 0, 50, 10);
        //bool active, int activeOn, float size, int duration, Vector3 location, bool tethered, Vector3 direction
        //int playerNum, float angle, int dmg, int sdmg, bool grab, int priority, float bkb, float skb
    }
    public void TMANgrab(int na)
    {
        blank();
    }


    public void blank() { }








}
