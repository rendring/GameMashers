using UnityEngine;

public enum ChessPieceType
{
    none = 0,
    Pawn1 = 1,
    Pawn2 = 2,
    Pawn3 = 3,
    Commander = 4,
}
public class PawnPiece : MonoBehaviour
{
    public int team;
    public int currentX;
    public int currentY;
    public ChessPieceType type;

    private Vector3 desiredPosition;
    private Vector3 desiredScale;


}
