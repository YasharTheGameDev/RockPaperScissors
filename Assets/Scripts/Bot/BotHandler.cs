using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHandler : MonoBehaviour
{
    [SerializeField] private BotNameCollection botNameCollection;

    private int playerNum;

    public void AssignBot(int playerNum) 
    {
        this.playerNum = playerNum;
        GameMaster.Instance.EndSelectEvent += SendInput;
    }
    public void UnassignBot()
    {
        GameMaster.Instance.EndSelectEvent -= SendInput;
    }

    public void SendInput() 
    {
        //GameMaster.Instance.GetInput();
    }
}
