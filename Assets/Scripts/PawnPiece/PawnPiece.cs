using UnityEngine;

public enum ChessPieceType
{
    none = 0,
    Pawn = 1,
    Commander = 2,
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
