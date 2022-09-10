using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Tracker : MonoBehaviour
{
    [SerializeField] private string result;
    [SerializeField] private int player;
    [SerializeField] private int oracle;
    // Start is called before the first frame update
    void Start()
    {
        player = Random.Range(2, 13);
        oracle = Random.Range(2, 13);
    }

    // Update is called once per frame
    void Update()
    {
        if(player > oracle)
        {
            result = "Player_Win";
        }
        else if(oracle > player)
        {
            result = "Player_Lose";
        }
        else
        {
            result = "Tie";
        }
    }

    string getResult()
    {
        return result;
    }
}
