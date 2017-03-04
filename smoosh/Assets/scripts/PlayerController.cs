using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ScriptableObject {

    public ChallengerCon player;

    public PlayerController (ChallengerCon p)
    {
        player = p;
    }
    
    public void inputUpdate()
    {

        //input buffer


        player.attack = false;
        player.special = false;

        if (UnityEngine.Input.GetKey(player.button1Input))
        {
            player.inputSelect(player.button1Assign);
        }
        if (UnityEngine.Input.GetKey(player.button2Input))
        {
            player.inputSelect(player.button2Assign);
        }
        if (UnityEngine.Input.GetMouseButton(0))
        {
            player.inputSelect(player.mb1);
        }
        if (UnityEngine.Input.GetMouseButton(1))
        {
            player.inputSelect(player.mb2);
        }

        if (UnityEngine.Input.GetKey(player.modifier1) && UnityEngine.Input.GetKey(player.button1Input))
        {
            player.inputSelect(player.mod1button1);
        }
        if (UnityEngine.Input.GetKey(player.modifier1) && UnityEngine.Input.GetKey(player.button2Input))
        {
            player.inputSelect(player.mod1button2);
        }
        if (UnityEngine.Input.GetKey(player.modifier1) && UnityEngine.Input.GetMouseButton(0))
        {
            player.inputSelect(player.mod1mb1);
        }
        if (UnityEngine.Input.GetKey(player.modifier1) && UnityEngine.Input.GetMouseButton(1))
        {
            player.inputSelect(player.mod1mb2);
        }
        if (UnityEngine.Input.GetKey(player.modifier1) && (UnityEngine.Input.GetKey(player.left) || UnityEngine.Input.GetKey(player.right)))
        {
            player.inputSelect(player.mod1lr);
        }
        if (UnityEngine.Input.GetKey(player.modifier1) && UnityEngine.Input.GetKey(player.down))
        {
            player.inputSelect(player.mod1down);
        }

        if (UnityEngine.Input.GetKey(player.modifier2) && UnityEngine.Input.GetKey(player.button1Input))
        {
            player.inputSelect(player.mod2button1);
        }
        if (UnityEngine.Input.GetKey(player.modifier2) && UnityEngine.Input.GetKey(player.button2Input))
        {
            player.inputSelect(player.mod2button2);
        }
        if (UnityEngine.Input.GetKey(player.modifier2) && UnityEngine.Input.GetMouseButton(0))
        {
            player.inputSelect(player.mod2mb1);
        }
        if (UnityEngine.Input.GetKey(player.modifier2) && UnityEngine.Input.GetMouseButton(1))
        {
            player.inputSelect(player.mod2mb2);
        }
        if (UnityEngine.Input.GetKey(player.modifier2) && (UnityEngine.Input.GetKey(player.left) || UnityEngine.Input.GetKey(player.right)))
        {
            player.inputSelect(player.mod2lr);
        }
        if (UnityEngine.Input.GetKey(player.modifier2) && UnityEngine.Input.GetKey(player.down))
        {
            player.inputSelect(player.mod2down);
        }

        player.mouseX = Input.mousePosition.x / Screen.width;
        player.mouseY = Input.mousePosition.y / Screen.height;
        player.screenPos = player.camera.WorldToViewportPoint(player.transform.position);

        if (player.attack)
        {
            player.special = false;
            player.dodgeInput = false;
            player.rollInput = false;
            if (player.mouseX >= player.screenPos.x)
            {
                player.mouseRight = true;
            }
            else
            {
                player.mouseRight = false;
            }

            if (UnityEngine.Input.GetKey(player.left) || UnityEngine.Input.GetKey(player.left))
            {
                player.fAttackInput = true;
            }
            else if (UnityEngine.Input.GetKey(player.jump))
            {
                player.uAttackInput = true;
            }
            else if (UnityEngine.Input.GetKey(player.down))
            {
                player.dAttackInput = true;
            }
            else
            {
                player.jabInput = true;
            }

            /*
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
            */
        }

        if (player.special)
        {
            player.dodgeInput = false;
            player.rollInput = false;

            if (player.mouseX >= player.screenPos.x)
            {
                player.mouseRight = true;
            }
            else
            {
                player.mouseRight = false;
            }

            if (UnityEngine.Input.GetKey(player.left) || UnityEngine.Input.GetKey(player.left))
            {
                player.fSpecInput = true;
            }
            else if (UnityEngine.Input.GetKey(player.jump))
            {
                player.uSpecInput = true;
            }
            else if (UnityEngine.Input.GetKey(player.down))
            {
                player.dSpecInput = true;
            }
            else
            {
                player.nSpecInput = true;
            }


            /*
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
            */
        }


        if (UnityEngine.Input.GetKey(player.left) && !(UnityEngine.Input.GetKey(player.modifier1) || UnityEngine.Input.GetKey(player.modifier2)))
        {
            player.leftInput = true;
        }
        if (UnityEngine.Input.GetKey(player.right) && !(UnityEngine.Input.GetKey(player.modifier1) || UnityEngine.Input.GetKey(player.modifier2)))
        {
            player.rightInput = true;
        }
        if (UnityEngine.Input.GetKey(player.down) && !(UnityEngine.Input.GetKey(player.modifier1) || UnityEngine.Input.GetKey(player.modifier2)))
        {
            player.downInput = true;
        }
        if (UnityEngine.Input.GetKeyDown(player.left) || UnityEngine.Input.GetKeyDown(player.right) && !(UnityEngine.Input.GetKey(player.modifier1) || UnityEngine.Input.GetKey(player.modifier2)))
        {
            player.dashInput = true;
        }
        if (UnityEngine.Input.GetKey(player.jump) && !(player.attack || player.special))
        {
            player.jumpInput = true;
        }
        if (UnityEngine.Input.GetKeyDown(player.jump) && !(player.attack || player.special))
        {
            player.jumpInputDown = true;
        }
        if (UnityEngine.Input.GetKey(player.left))
        {
            player.leftMod = true;
        }

        if (UnityEngine.Input.GetKey(player.right))
        {
            player.rightMod = true;
        }


    }

}
