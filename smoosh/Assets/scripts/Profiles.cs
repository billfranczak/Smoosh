using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profiles : ScriptableObject{



    public List<string> profileNames;

    //basic controls
    public List<bool> shiftWalk;
    public List<KeyCode> left;
    public List<KeyCode> right;
    public List<KeyCode> down;
    public List<KeyCode> jump;
    public List<KeyCode> modifier1;
    public List<KeyCode> modifier2;
    public List<KeyCode> button1Input;
    public List<KeyCode> button2Input;
    public List<KeyCode> shiftWalkInput;
    public List<KeyCode> altDashWindown;

    //assignments
    public List<string> mb1;
    public List<string> mb2;
    public List<string> button1Assign;
    public List<string> button2Assign;
    public List<string> mod1mb1;
    public List<string> mod1mb2;
    public List<string> mod1button1;
    public List<string> mod1button2;
    public List<string> mod2mb1;
    public List<string> mod2mb2;
    public List<string> mod2button;
    public List<string> mod2button1;
    public List<string> mod2button2;
    public List<string> mod1lr;
    public List<string> mod1down;
    public List<string> mod2lr;
    public List<string> mod2down;


    public Profiles() // erase after serializable is implemented
    {
        //bill
        //Debug.Log("Profiles loading?");
        profileNames = new List<string>();

        shiftWalk = new List<bool>(); ;
        left = new List<KeyCode>();
        right = new List<KeyCode>();
        down = new List<KeyCode>();
        jump = new List<KeyCode>();
        modifier1 = new List<KeyCode>();
        modifier2 = new List<KeyCode>();
        button1Input = new List<KeyCode>();
        button2Input = new List<KeyCode>();
        shiftWalkInput = new List<KeyCode>();
        altDashWindown = new List<KeyCode>();

        //assignments
        mb1 = new List<string>();
        mb2 = new List<string>();
        button1Assign = new List<string>();
        button2Assign = new List<string>();
        mod1mb1 = new List<string>();
        mod1mb2 = new List<string>();
        mod1button1 = new List<string>();
        mod1button2 = new List<string>();
        mod2mb1 = new List<string>();
        mod2mb2 = new List<string>();
        mod2button = new List<string>();
        mod2button1 = new List<string>();
        mod2button2 = new List<string>();
        mod1lr = new List<string>();
        mod1down = new List<string>();
        mod2lr = new List<string>();
        mod2down = new List<string>();


        profileNames.Clear();
        profileNames.Add("ccoma");
        shiftWalk.Add(false);
        left.Add(KeyCode.A);
        right.Add(KeyCode.D);
        down.Add(KeyCode.S);
        jump.Add(KeyCode.W);
        modifier1.Add(KeyCode.F);
        modifier2.Add(KeyCode.G);
        button1Input.Add(KeyCode.Space);
        button2Input.Add(KeyCode.E);
        shiftWalkInput.Add(KeyCode.LeftShift);
        altDashWindown.Add(KeyCode.S);

        mb1.Add("normal");
        mb2.Add("special");
        button1Assign.Add("jab");
        button2Assign.Add("grab");

        mod1mb1.Add("shield");
        mod1mb2.Add("grab");
        mod1button1.Add("shield");
        mod1button2.Add("");
        mod1lr.Add("roll");
        mod1down.Add("dodge");

        mod2mb1.Add("");
        mod2mb2.Add("");
        mod2button1.Add("");
        mod2button2.Add("");
        mod2lr.Add("");
        mod2down.Add("");
        //Debug.Log("Profiles loaded");
    }



}
