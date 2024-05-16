using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    // 6/5/2024- Ren H: creating global array
    public static List<Card> cardList = new List<Card>();

    private void Awake()
    {
        // 6/5/2024- Ren H: Blank Card
        cardList.Add(new Card(0, false, "None"));
        cardList.Add(new Card(1, false, "Pawn can move 1 diagonal space"));
        cardList.Add(new Card(2, true, "Commanders can jump over the pawns"));
        cardList.Add(new Card(3, true, "Commanders can move 2 diagonal spaces"));
        cardList.Add(new Card(4, false, "Pawns that capture commanders become commanders"));
        cardList.Add(new Card(5, false, "Pawns that capture other pawns elimates both pawn"));
        cardList.Add(new Card(6, false, "Pawns move 2 spaces"));
        cardList.Add(new Card(7, true, "Commanders move 1 space"));
        cardList.Add(new Card(8, false, "Pawns can vertically warp to the other side"));
        cardList.Add(new Card(9, false, "All pieces can horizontally warp to the other side"));
        cardList.Add(new Card(10, true, "Only the commander can move"));
        cardList.Add(new Card(11, true, "You cannot move your commander two rounds in a row"));
        cardList.Add(new Card(12, false, "Capturing a piece means you can move again"));
    }
}