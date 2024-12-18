using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Controller : MonoBehaviour
{
    public Tilemap tilemap; // R�f�rence � la Tilemap
    public Vector2Int PlayerPosition;
    public TileBase[] tileFloor;
    public TileBase[] tileInterract;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject FrontTilesDetector;
    public Seed ActualSeed;
    private void Start()
    {
        PlayerPosition = new Vector2Int((int)transform.position.x,(int)transform.position.y);
    }
    void Update()
    {
        PlayerMovement();
        PlayerInterract();
    }
    #region Movement
    //Update All Movement Script
    void PlayerMovement()
    {
        //defini un vector2 avec le vector2 position du joueur + le vector2 des input( (0,0) par defaut) 
        Vector2Int DesiredPosition = PlayerPosition + InputDirection();

        // si le joueur veut chang� de position
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
            transform.forward = new Vector3(0, 0,x);
            FrontTilesDetector.transform.position = new Vector3(transform.position.x + x, transform.position.y, 0);

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
            FrontTilesDetector.transform.position = new Vector3(transform.position.x, transform.position.y + y, 0);

        }

        return new Vector2Int(x, y);
    }
    #endregion
    #region Interract

    void PlayerInterract()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("interract");
            TileBase tile = GetTile(new Vector2Int((int)FrontTilesDetector.transform.position.x,(int)FrontTilesDetector.transform.position.y));
            Vector2Int FTDpos = new Vector2Int((int)FrontTilesDetector.transform.position.x, (int)FrontTilesDetector.transform.position.y);
            if (Array.Exists(tileInterract, element => element.Equals(tile)))
            {
                if(tile == tileInterract[0])
                {
                    Debug.Log("Actual Seed ==" + ActualSeed);
                    gameManager.plantation.Seeding(FTDpos, ActualSeed);
                }
                else if(tile == tileInterract[1])
                {
                    gameManager.plantation.Watering(FTDpos);
                }
                else if(tile == tileInterract[2])
                {
                    Debug.Log("REMPLIT LE arrousiouri");
                }
                else if(tile == tileInterract[3])
                {
                    gameManager.plantation.Beching(FTDpos);
                }
            }
        }
    }
    #endregion

    
}
