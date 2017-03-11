using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlankBoxNet : MonoBehaviour
{

    public ChallengerConNet player;

    public Action<int> act;
    public int duration;

    public BlankBoxNet(ChallengerConNet chal)
    {
        player = chal;
        act = blank;
        duration = 0;
    }

    // Use this for initialization
    void Start()
    {
        act = blank;
        duration = 0;
    }

    // Update is called once per frame
    void Update()
    {

        act(0);

    }

    public void blank(int i)
    {

    }

}
