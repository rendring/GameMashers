using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card : MonoBehaviour
{
    // 6/5/2024- Ren H: Set-Up for variables
    public int id;
    public bool type;
    public string desc;

    /* 6/5/2024- Ren H: Remove as cards do not need names
    public string cardName; */

    public Card()
    {

    }

    // 6/5/2024- Ren H: Defining the variables
    public Card (int Id, bool Type, string Desc)
    {
        id = Id;
        // 13/5/2024- Ren H: false = pawn, true = commander
        type = Type;
        desc = Desc;
    }
}
