using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Tracker : MonoBehaviour
{
    [SerializeField] private string result;
    [SerializeField] private int player;
    [SerializeField] private int oracle;
    public bool isHigher;
    public void checkWin()
    {
        if (isHigher == true)
        {
            if (player > oracle)
            {
                result = "Player_Win";
            }
            else if (oracle > player)
            {
                result = "Player_Lose";
            }
            else
            {
                result = "Tie";
            }
        }
        else
        {
            if (player > oracle)
            {
                result = "Player_Lose";
            }
            else if (oracle > player)
            {
                result = "Player_Win";
            }
            else
            {
                result = "Tie";
            }
        }
    }

    public void setDice(int player, int oracle, bool isHigher)
    {
        this.player = player;
        this.oracle = oracle;
        this.isHigher = isHigher;

        checkWin();
    }

    public string getResult()
    {
        return result;
    }
}
