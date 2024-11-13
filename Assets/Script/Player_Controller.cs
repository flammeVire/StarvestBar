using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Controller : MonoBehaviour
{
    public Tilemap tilemap; // Référence à la Tilemap
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
        //defini un vector2 avec le vector2 position du joueur + le vector2 des input( (0,0) par defaut) 
        Vector2Int DesiredPosition = PlayerPosition + InputDirection();

        // si le joueur veut changé de position
        if (DesiredPosition != PlayerPosition)
        {
            //on recupere le type de tuile correspondant a la case
            TileBase tile = GetTile(DesiredPosition);

            //si la tuile existe
            if ( tile != null) 
            {
                //check si un element correspondant a la tuile trouve existe dans la list de tuile de sol
                if (Array.Exists(tileFloor, element => element.Equals(tile)))
                {
                    //bouge le joueur et defini son vector2 position a sa position
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
