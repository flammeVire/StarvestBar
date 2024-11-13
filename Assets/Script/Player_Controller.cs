using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Controller : MonoBehaviour
{
    public Tilemap tilemap; // Référence à la Tilemap
    public Vector3 worldPosition; // Position dans le monde
    public Vector2Int PlayerPosition;
    public TileBase[] tileFloor;
    private void Start()
    {
        PlayerPosition = new Vector2Int((int)transform.position.x,(int)transform.position.y);
    }
    void Update()
    {
        PlayerMovement();
    }

    //Update All Movement Script
    void PlayerMovement()
    {
        
        Vector2Int DesiredPosition = PlayerPosition + InputDirection();

        if (DesiredPosition != PlayerPosition)
        {
            TileBase tile = GetTile(DesiredPosition);
            Debug.Log(tile);
            if ( tile != null) 
            {
                if (Array.Exists(tileFloor, element => element.Equals(tile)))
                {
                    transform.position = new Vector3(DesiredPosition.x, DesiredPosition.y);
                    PlayerPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
                }
            }
        }
    }


    TileBase GetTile(Vector2Int DesiredPosition)
    {
        
        Vector3Int cellPosition = tilemap.WorldToCell(new Vector3(DesiredPosition.x,DesiredPosition.y,0));
        return tilemap.GetTile(cellPosition);
    }

    //a suppr (gpt)
    void GetCellType()
    {
        // Convertir la position du monde en position de cellule de la Tilemap
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);

        // Récupérer la Tile à la position donnée
        TileBase tile = tilemap.GetTile(cellPosition);

        // Vérifier si la tile existe et obtenir le sprite
        if (tile != null)
        {
            // Si la tile est une Tile simple (par exemple, Tile2D), on peut accéder à son sprite
            Tile tileAsTile = tile as Tile;
            if (tileAsTile != null)
            {
                Sprite tileSprite = tileAsTile.sprite;
                Debug.Log("Sprite trouvé : " + tileSprite.name);
            }
        }
        else
        {
            Debug.Log("Pas de tile à cette position.");
        }
    }

    Vector2Int InputDirection()
    {
        int x = 0;int y = 0;

        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                x = 1;
            }
            else
            {
                x = -1;
            }
        }
        else if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                y = 1;
            }
            else
            {
                y = -1;
            }
        }

        return new Vector2Int(x, y);
    }
}
