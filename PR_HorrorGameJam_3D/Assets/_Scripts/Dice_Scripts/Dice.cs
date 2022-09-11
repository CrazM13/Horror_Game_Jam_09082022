using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    bool hasLanded;
    bool thrown;

    Vector3 initPosition;

    public int diceValue;

    public DiceSide[] diceSies;

	public bool IsDone => hasLanded;


	 void Awake()
    {
        initPosition = transform.position;
        rb.useGravity = true;
        
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

    public void RollDice()
    {
		ResetDice();
        thrown = true;

        rb.useGravity = true;
		rb.isKinematic = false;
        rb.AddTorque(Random.Range(0,500), Random.Range(0,500),Random.Range(0,500));
       //else if (thrown && hasLanded)
       // {
       //     ResetDice();
       // }
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
