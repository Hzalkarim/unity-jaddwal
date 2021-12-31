using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnButton : MonoBehaviour
{
    public Text text;
    public Team myTeam;

    public void SetActiveThisTurn()
    {
        text.text = "My Turn";
    }

    public void SetInactiveThisTurn()
    {
        text.text = "";
    }
}
