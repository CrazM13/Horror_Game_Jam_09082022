using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;

    bool hasLanded;
    bool thrown;

    Vector3 initPosition;

    public int diceValue;

    public DiceSide[] diceSies;

     void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPosition = transform.position;
        rb.useGravity = false;
        
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {                    
            RollDice();
        }

        if(rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;

            SideValueCheck ();

        }
        else if(rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            RollAgain();
        }
        
    }

    void RollDice()
    {
       if(!thrown && !hasLanded)
        {
            thrown = true;

            rb.useGravity = true;
            rb.AddTorque(Random.Range(0,500), Random.Range(0,500),Random.Range(0,500));
        }
       else if (thrown && hasLanded)
        {
            ResetDice();
        }
    }

     void ResetDice()
    {
        transform.position = initPosition;  
        thrown = false;
        hasLanded = false;      
        rb.useGravity = false;
        rb.isKinematic= false;
    }

    void RollAgain()
    {
        ResetDice();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    void SideValueCheck()
    {
        diceValue = 0;
        foreach(DiceSide side in diceSies)
        {
            if(side.OnGround())
            {
                diceValue = side.sideValue;
                Debug.Log(diceValue + " Has been rolled");
            }
        }

    }
}
