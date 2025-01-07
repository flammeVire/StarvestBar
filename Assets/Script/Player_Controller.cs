using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class Player_Controller : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector2Int PlayerPosition;
    public TileBase[] tileFloor;
    public TileBase[] tileInterract;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject FrontTilesDetector;
    public Seed ActualSeed;
    [SerializeField] BarreInv inventoryScript;
    [SerializeField] GameObject ShopObj;
    [SerializeField] GameObject MiniGameObj;
    public bool InMixeur = false;
    public Vegetable LegumeChoisi;

    [Header("Inventory")]
    public List<Vegetable> vegetablesInventory;
    public List<Seed> SeedInventory;
    public int Money;

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
            TileBase tile = GetTile(new Vector2Int((int)FrontTilesDetector.transform.position.x, (int)FrontTilesDetector.transform.position.y));
            Vector2Int FTDpos = new Vector2Int((int)FrontTilesDetector.transform.position.x, (int)FrontTilesDetector.transform.position.y);

            if (Array.Exists(tileInterract, element => element.Equals(tile)))
            {
                //cas terre
                if (tile == tileInterract[0] && inventoryScript.index >= 3)
                {
                    Debug.Log("Actual Seed ==" + ActualSeed);
                    if (haveSeedInInventory(ActualSeed))
                    {
                        gameManager.plantation.Seeding(FTDpos, ActualSeed);
                    }
                }
                //cas pousse
                else if (tile == tileInterract[1] || tile == tileInterract[2] || tile == tileInterract[3] || tile == tileInterract[4])
                {
                    if (inventoryScript.index == 1)
                    {
                        gameManager.plantation.Watering(FTDpos);
                    }
                }
                //cas terre dur
                else if (tile == tileInterract[5] && inventoryScript.index == 2)
                {
                    gameManager.plantation.Beching(FTDpos);
                }

                //cas Ordinateur
                else if (tile == tileInterract[6] && inventoryScript.index == 0)
                {
                    OpenComputer();
                    
                }
                //cas mixeur
                else if (tile == tileInterract[7] && inventoryScript.index == 0)
                {
                    if(InMixeur == false)
                    {
                        MakeSmoothie();
                    }

                }
                //cas legume
                else if (tile == tileInterract[8] || tile == tileInterract[9] || tile == tileInterract[10] || tile == tileInterract[11] || tile == tileInterract[12] || tile == tileInterract[13] || tile == tileInterract[14] || tile == tileInterract[15] || tile == tileInterract[16] || tile == tileInterract[17] && inventoryScript.index == 0)
                {
                    vegetablesInventory.Add(gameManager.plantation.PickUpVegetable(FTDpos));

                }

                /*
                //cas eau
                else if(tile == tileInterract[x])
                {
                    Debug.Log("REMPLIT LE arrousiouri");
                }*/
            }
        }
    }

        //mettre code de mat
    void MakeSmoothie()
    {
        if (vegetablesInventory.Count > 0)
        {

            //récupérer un légume aléatoire
            while (true)
            {
                int ChoixLegume = UnityEngine.Random.Range(0, 10);
                LegumeChoisi = GameManager.Instance.PossibleVegetable[ChoixLegume];
                Debug.Log(LegumeChoisi);

                foreach (Vegetable vegetable in vegetablesInventory)
                {
                    if (LegumeChoisi.Nom == vegetable.Nom)
                    {
                        MiniGameObj.SetActive(true);
                        InMixeur = true;
                        vegetablesInventory.Remove(vegetable);
                        Debug.Log("Lance le jeu fank");
                        return;
                    }
                }
            }
        }

        //si oui lance mini jeu
        //si non on vérifi un autre légume

    }

    void OpenComputer()
    {
        ShopObj.SetActive(true);
    }

    #endregion
    
    bool haveSeedInInventory(Seed seedselect)
    {
        for (int i = 0; i < SeedInventory.Count; i++)
        {
            if (SeedInventory[i] == seedselect)
            {
                SeedInventory.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

}
