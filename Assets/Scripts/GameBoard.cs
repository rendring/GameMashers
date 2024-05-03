using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [Header("Art stuff")]
    [SerializeField] private Material tileMaterial;
    [SerializeField] private float tileSize = 1.0f;
    [SerializeField] private float yOffset = 0.2f;
    [SerializeField] private Vector3 boardCenter = Vector3.zero;

    [Header("Prefabs / Materials")]
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Material[] teamMaterials;


    //logic 
    private PawnPiece[,] pawnPieces;
    private const int TILE_COUNT_X = 8;
    private const int TILE_COUNT_Y = 8;
    private GameObject[,] tiles;
    private Camera currentCamera;
    private Vector2Int currentHover;
    private Vector3 bounds;




    private void Awake()
    {
        GenerateAllTiles(tileSize, TILE_COUNT_X, TILE_COUNT_Y);

        SpawnAllPieces();
        PositionAllPieces();
    }
    private void Update()
    {
        //tile light up on hover
        if (!currentCamera)
        {
            currentCamera = Camera.main;
            return;
        }
        RaycastHit info;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Tile", "Hover")))
        {
            //Get the indexes of tile we hit
            Vector2Int hitPosition = LookupTileIndex(info.transform.gameObject);

            //If hovering any tile after not hovering any tile
            if (currentHover == -Vector2Int.one)
            {
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }
            //if  already hovering over tile, change previous
            if (currentHover != hitPosition)
            {
                tiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("Tile");
                currentHover = hitPosition;
                tiles[currentHover.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }
        }
        else
        {
            if (currentHover != -Vector2Int.one)
            {
                tiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("Tile");
                currentHover = -Vector2Int.one;
            }
        }
    }
    //board generation
    private void GenerateAllTiles(float tileSize, int tileCountX, int tileCountY)
    {
        yOffset += transform.position.y;
        bounds = new Vector3((tileCountX / 2) * tileSize, 0, (tileCountX / 2) * tileSize) + boardCenter;

        tiles = new GameObject[tileCountX, tileCountY];
        for (int x = 0; x < tileCountX; x++)
            for (int y = 0; y < tileCountY; y++)
                tiles[x, y] = GenrateSingleTile(tileSize, x, y);
    }
    private GameObject GenrateSingleTile(float tileSize, int x, int y)
    {
        GameObject tileObject = new GameObject(string.Format("x:{0}, Y:{1}", x, y));
        tileObject.transform.parent = transform;

        Mesh mesh = new Mesh();
        tileObject.AddComponent<MeshFilter>().mesh = mesh;
        tileObject.AddComponent<MeshRenderer>().material = tileMaterial;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * tileSize, yOffset, y * tileSize) - bounds;
        vertices[1] = new Vector3(x * tileSize, yOffset, (y + 1) * tileSize) - bounds;
        vertices[2] = new Vector3((x + 1) * tileSize, yOffset, y * tileSize) - bounds;
        vertices[3] = new Vector3((x + 1) * tileSize, yOffset, (y + 1) * tileSize) - bounds;

        int[] tris = new int[] { 0, 1, 2, 1, 3, 2 };

        mesh.vertices = vertices;
        mesh.triangles = tris;

        mesh.RecalculateNormals();

        tileObject.layer = LayerMask.NameToLayer("Tile");
        tileObject.AddComponent<BoxCollider>();

        return tileObject;
    }

    // spawning of pawns
    private void SpawnAllPieces()
    {
        pawnPieces = new PawnPiece[TILE_COUNT_X, TILE_COUNT_Y];

        int GreenTeam = 0, BlueTeam = 1, OrangeTeam = 2, PurpleTeam = 3;

        //Green Team
        pawnPieces[0, 0] = SpawnSinglePiece(ChessPieceType.Commander, GreenTeam);
        pawnPieces[1, 0] = SpawnSinglePiece(ChessPieceType.Pawn1, GreenTeam);
        pawnPieces[1, 1] = SpawnSinglePiece(ChessPieceType.Pawn2, GreenTeam);
        pawnPieces[0, 1] = SpawnSinglePiece(ChessPieceType.Pawn3, GreenTeam);
        //Blue Team
        pawnPieces[7, 0] = SpawnSinglePiece(ChessPieceType.Commander, BlueTeam);
        pawnPieces[7, 1] = SpawnSinglePiece(ChessPieceType.Pawn1, BlueTeam);
        pawnPieces[6, 1] = SpawnSinglePiece(ChessPieceType.Pawn2, BlueTeam);
        pawnPieces[6, 0] = SpawnSinglePiece(ChessPieceType.Pawn3, BlueTeam);
        //Orange Team
        pawnPieces[7, 7] = SpawnSinglePiece(ChessPieceType.Commander, OrangeTeam);
        pawnPieces[7, 6] = SpawnSinglePiece(ChessPieceType.Pawn1, OrangeTeam);
        pawnPieces[6, 6] = SpawnSinglePiece(ChessPieceType.Pawn2, OrangeTeam);
        pawnPieces[6, 7] = SpawnSinglePiece(ChessPieceType.Pawn3, OrangeTeam);
        //Purple Team
        pawnPieces[0, 7] = SpawnSinglePiece(ChessPieceType.Commander, PurpleTeam);
        pawnPieces[1, 7] = SpawnSinglePiece(ChessPieceType.Pawn1, PurpleTeam);
        pawnPieces[1, 6] = SpawnSinglePiece(ChessPieceType.Pawn2, PurpleTeam);
        pawnPieces[0, 6] = SpawnSinglePiece(ChessPieceType.Pawn3, PurpleTeam);
    }
    private PawnPiece SpawnSinglePiece(ChessPieceType type, int team)
    {
        PawnPiece cp = Instantiate(prefabs[(int)type - 1], transform).GetComponent<PawnPiece>();

        cp.type = type;
        cp.team = team;
        cp.GetComponent<MeshRenderer>().material = teamMaterials[team];

        return cp;

    }

    //positioning 
    private void PositionAllPieces()
    {
        for (int x = 0; x < TILE_COUNT_X; x++)
            for (int y = 0; y < TILE_COUNT_Y; y++)
                if (pawnPieces[x,y] != null)
                    PositionSinglePiece(x,y, true);
    }
    private void PositionSinglePiece(int x, int y, bool force = false)
    {
        pawnPieces[x, y].currentX = x;
        pawnPieces[x, y].currentY = y;
        pawnPieces[x, y].transform.position = GetTileCentre(x, y);
    }
    private Vector3 GetTileCentre(int x, int y)
    {
        return new Vector3(x * tileSize, yOffset, y * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2);
    }

    // operation
    private Vector2Int LookupTileIndex(GameObject hitInfo)
    {
        for (int X = 0; X < TILE_COUNT_X; X++)
            for (int Y = 0; Y < TILE_COUNT_Y; Y++)
                if (tiles[X, Y] == hitInfo)
                    return new Vector2Int(X, Y);
       
        return -Vector2Int.one; // Invalid
     
    }
}