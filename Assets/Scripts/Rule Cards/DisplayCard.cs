using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DisplayCard : MonoBehaviour
{
    // Ren H:  Creating a card for the database to display on
    public List<Card> displayCard = new List<Card>();
    public int displayId;

    // Ren H: calling the card database
    public int id;
    public bool type;
    public string desc;

    // 3/6/2024- Ren H: Set-up for commander & pawn colours
    //  public SpriteRenderer typeColor;

    /* 8/6/2024: Ren H- changing colour in code is not displaying. Changeing the srpite to enable/disable based on type boolean
     * public Color32 pblue = new Color32() { r = 60, g = 127, b = 129 };
     * public Color32 cred = new Color32() { r = 217, g = 93, b = 65 }; */

    public GameObject comObj;
    public Text comDesc;

    public GameObject pawnObj;
    public Text pawnDesc;

    // Start is called before the first frame update
    void Start()
    {
        displayCard[0] = CardDatabase.cardList[displayId];
        type = false;
    }

    // Update is called once per frame
    void Update()
    {
        id = displayCard[0].id;
        type = displayCard[0].type;
        desc = displayCard[0].desc;

        if (type == false)
        {
            pawnObj.gameObject.SetActive(true);
            pawnDesc.text = desc;
        } 
        if (type == true) 
        {
            comObj.gameObject.SetActive(true);
            comDesc.text = desc;
        }
    }
}
