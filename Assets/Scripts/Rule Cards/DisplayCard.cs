using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayCard : MonoBehaviour
{
    
    public List<Card> displayCard = new List<Card>();

    public int displayId;
    
    public int id;
    public bool type;
    public string desc;

    public Text typeText;
    public Text descText;


    // Start is called before the first frame update
    void Start()
    {
        displayCard[0] = CardDatabase.cardList[displayId];
    }

    // Update is called once per frame
    void Update()
    {
        id = displayCard[0].id;
        type = displayCard[0].type;
        desc = displayCard[0].desc;
    }
}
