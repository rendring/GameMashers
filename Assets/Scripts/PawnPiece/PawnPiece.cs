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
    private Vector3 desiredScale = Vector3.one;

    //smoothing of pawn movement
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);
        transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, Time.deltaTime * 10);
    }

    public virtual void SetPosition(Vector3 position, bool force = false)
    {
        desiredPosition = position;
        if (force) 
            transform.position = desiredPosition;
    } 
    public virtual void Setscale(Vector3 scale, bool force = false)
    {
        desiredScale = scale;
        if (force) 
            transform.localScale = desiredScale;
    }


}
