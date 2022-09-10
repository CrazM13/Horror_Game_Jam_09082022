using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    bool onGround;
    public int sideValue;

    public Vector3 checkSize;

    /*void OnTriggerStay(Collider col)
   {
       if (col.tag == "Grounded")
       {
           Debug.Log("ground");
           onGround = true;
       }
   }

    /*void OnTriggerExit(Collider col)
   {
       if(col.tag == "Grounded")
       {
           Debug.Log("has left ground");
           onGround = false; 
       }
   }*/

    private void Update()
    {
        onGround = Physics.CheckBox(transform.position + checkSize, checkSize, transform.rotation, ~LayerMask.GetMask("Dice"));


    }
    public bool OnGround()
    {
        return onGround;
    }
}
