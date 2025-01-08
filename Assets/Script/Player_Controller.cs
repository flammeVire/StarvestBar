using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    [SerializeField] public UIManagement uiManagement;
    [SerializeField] public Pnj_Dialogue_UI pnj;
    bool lockPlayer = false;

    [Header("Inventory")]
    public List<Vegetable> vegetablesInventory;
    public List<Seed> SeedInventory;
    public int Money;
    
    [Header("Arrosoir")]
    public int arrosoirCapacity = 5;
    public int currentWater;
    public bool isUpgraded;
    private void Start()
    {
        PlayerPosition = new Vector2Int((int)transform.position.x,(int)transform.position.y);
        currentWater = arrosoirCapacity;
        uiManagement.UpdateMoneyDisplay(Money);
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

        if (Input.GetAxisRaw("StopMove") > 0)
        {
            lockPlayer = true;
        }
        else
        {
            lockPlayer = false;
        }
        
        //defini un vector2 avec le vector2 position du joueur + le vector2 des input( (0,0) par defaut) 
        Vector2Int DesiredPosition = PlayerPosition + InputDirection(lockPlayer);
        

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

    Vector2Int InputDirection(bool locked)
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

        if (locked)
        {
            return new Vector2Int(0, 0);
        }
        else 
        { 
        return new Vector2Int(x, y);
        }
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

                        if (ActualSeed == GameManager.Instance.PossibleSeed[0])
                        {
                            uiManagement.UpdateSeedInventory(uiManagement.seeds1Text, uiManagement.numberOfSeeds1 -= 1);
                        }
                        else if (ActualSeed == GameManager.Instance.PossibleSeed[1])
                        {
                            uiManagement.UpdateSeedInventory(uiManagement.seeds2Text,uiManagement.numberOfSeeds2 -= 1);
                        }
                        else if (ActualSeed == GameManager.Instance.PossibleSeed[2])
                        {
                            uiManagement.UpdateSeedInventory(uiManagement.seeds3Text,uiManagement.numberOfSeeds3 -= 1);
                        }
                        else if (ActualSeed == GameManager.Instance.PossibleSeed[3])
                        {
                            uiManagement.UpdateSeedInventory(uiManagement.seeds4Text,uiManagement.numberOfSeeds4 -= 1);
                        }
                    }
                }
                //cas pousse
                else if (tile == tileInterract[1] || tile == tileInterract[2] || tile == tileInterract[3] || tile == tileInterract[4])
                {
                    if (inventoryScript.index == 1 && currentWater > 0)
                    {
                        gameManager.plantation.Watering(FTDpos);
                        currentWater --;
                        uiManagement.UpdateCapacityArrosoir(currentWater,arrosoirCapacity);

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
                    if(InMixeur == false && vegetablesInventory.Count > 0)
                    {
                        pnj.PasseDialogue();
                    }

                }
                //cas legume
                else if (tile == tileInterract[8] || tile == tileInterract[9] || tile == tileInterract[10] || tile == tileInterract[11] || tile == tileInterract[12] || tile == tileInterract[13] || tile == tileInterract[14] || tile == tileInterract[15] || tile == tileInterract[16] || tile == tileInterract[17] && inventoryScript.index == 0)
                {
                    vegetablesInventory.Add(gameManager.plantation.PickUpVegetable(FTDpos));

                }
                // cas eau
                else if (inventoryScript.index == 1 && tile == tileInterract[18])
                {
                    currentWater = arrosoirCapacity;
                    uiManagement.UpdateCapacityArrosoir(currentWater, arrosoirCapacity);
                }
            }
        }
    }

        //mettre code de mat
    public string MakeSmoothie()
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
                        return LegumeChoisi.Nom;
                    }
                }
            }
        }
        return null;

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
