using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Save_Data : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Plantation_Controller plantation;
    [SerializeField] Player_Controller player;
    public Game_Data Data = new Game_Data();

    #region save
    void SaveToJson()
    {
        string game_Data = JsonUtility.ToJson(Data);
        string filePath = Application.persistentDataPath + "/GameData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, game_Data);
    }
    public void Save()
    {        
        Data.money = player.Money;
        Data.Player_seeds = player.SeedInventory;
        Data.Player_Vegetables = player.vegetablesInventory;
        Data.PlayerPosition = player.PlayerPosition;
        Data.arroisoir_Capacity = player.arrosoirCapacity;
        Data.arroisoirIsUpgraded = false; // changer avec code de mael
        

        foreach (var plantes in plantation.SeedPosition)
        {
            Debug.Log(plantes);
            Seed_Data seed_Data = AddingSeedCloneData(plantes.Value, plantes.Key);
            Debug.Log(seed_Data);
            Data.Seeds_OnMap.Add(seed_Data);
            Debug.Log(Data.Seeds_OnMap.Count);           
        }
        foreach(var plantes in plantation.VegetablePosition)
        {
           Vegetable_Data vegetable_Data = AddingVegetableCloneData(plantes.Value,plantes.Key);
           Data.Vegetable_OnMap.Add(vegetable_Data);
        }
        Data.DayCycle = gameManager.CurrentDayCycle;
        SaveToJson();
    }

    #region other Data
    Seed_Data AddingSeedCloneData(Seed seed, Vector2Int pos)
    {
        Seed_Data sdt = new Seed_Data();
        sdt.Grow = seed.ActualGrowthTime;
        sdt.Nom = seed.Nom;
        sdt.position = pos;
        if (player.tilemap.GetTile((Vector3Int)pos) == seed.Tile)
        {
            sdt.IsWatered = false;
        }
        else
        {
            sdt.IsWatered = true;
        }
        sdt.HaveMutated = seed.HaveMuted;
        sdt.vegetableCorrespondant = seed.Vegetable;
        return sdt;
    }

    Vegetable_Data AddingVegetableCloneData(Vegetable vegetable,Vector2Int pos)
    {
        Vegetable_Data vdt = new Vegetable_Data();
        vdt.position = pos;
        vdt.Nom = vegetable.Nom;
        return vdt;
    }
    #endregion


    #endregion
    #region load
    void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/GameData.json";
        string game_Data = System.IO.File.ReadAllText(filePath);
        Data = JsonUtility.FromJson<Game_Data>(game_Data);
    }

    public void Load()
    {
        LoadFromJson();

        player.Money = Data.money;
        player.SeedInventory = Data.Player_seeds;
        player.vegetablesInventory = Data.Player_Vegetables;
        player.PlayerPosition = Data.PlayerPosition;
        player.arrosoirCapacity = Data.arroisoir_Capacity;

        plantation.SeedPosition = ReturnDictSeedFromJson(Data);
        plantation.VegetablePosition = ReturnDictVegetableFromJson(Data) ;

        gameManager.CurrentDayCycle = Data.DayCycle;
        player.uiManagement.UpdateMoneyDisplay(player.Money);
        changeTileMap();
    }

    void changeTileMap()
    {
        foreach (var plantes in plantation.SeedPosition)
        {
            Vector3Int pos = new Vector3Int(plantes.Key.x - 1, plantes.Key.y - 1, 0);
            foreach (var valeur in Data.Seeds_OnMap)
            {
                if (valeur.position == plantes.Key)
                {
                    if (valeur.IsWatered)
                    {
                        player.tilemap.SetTile(pos, plantes.Value.WateredTiles);

                    }
                    else
                    {
                        player.tilemap.SetTile(pos, plantes.Value.Tile);

                    }
                }
            }

        }
        foreach(var plantes in plantation.VegetablePosition)
        {
            Vector3Int pos = new Vector3Int(plantes.Key.x - 1, plantes.Key.y - 1, 0);
            player.tilemap.SetTile(pos, plantes.Value.Tile);
        }
    }
    #region Other Data
    Dictionary<Vector2Int, Seed> ReturnDictSeedFromJson(Game_Data data)
    {
        Dictionary<Vector2Int, Seed> dico = new Dictionary<Vector2Int, Seed>();

        //crée un clone de la graine
        //quel est la graine

        foreach (var valeur in data.Seeds_OnMap)
        {
            Debug.Log(valeur);
            Seed clone = null;
            foreach (Seed plante in gameManager.PossibleSeed)
            {
                if (plante.Nom == valeur.Nom)
                {
                    clone = Instantiate(plante);
                }
            }
            if (clone != null)
            {
                clone.HaveMuted = valeur.HaveMutated;
                clone.ActualGrowthTime = valeur.Grow;
                clone.Vegetable = valeur.vegetableCorrespondant;
            }
            dico.Add(valeur.position, clone);
        }
        return dico;
    }

    Dictionary<Vector2Int,Vegetable> ReturnDictVegetableFromJson(Game_Data data)
    {
        Dictionary<Vector2Int, Vegetable> dico = new Dictionary<Vector2Int, Vegetable>();
        foreach(var valeur in data.Vegetable_OnMap)
        {
            Vegetable clone = null;
            foreach(Vegetable vegetable in gameManager.PossibleVegetable)
            {
                if(vegetable.Nom == valeur.Nom)
                {
                    clone= Instantiate(vegetable);
                }   
            }
            
            dico.Add(valeur.position,clone);
        }

        return dico;
    }
    #endregion
    #endregion
}


public class Game_Data
{
    public int money;
    public List<Seed> Player_seeds;
    public List<Vegetable> Player_Vegetables;
    public Vector2Int PlayerPosition;
    public int arroisoir_Capacity;
    public bool arroisoirIsUpgraded;

    public List<Seed_Data> Seeds_OnMap = new List<Seed_Data>();
    public List<Vegetable_Data> Vegetable_OnMap = new List<Vegetable_Data>();
    public GameManager.DayCycle DayCycle;
}


[System.Serializable]
public class Seed_Data
{
    public int Grow;
    public string Nom;
    public Vector2Int position;
    public bool IsWatered;
    public bool HaveMutated;
    public Vegetable vegetableCorrespondant;
}


[System.Serializable]
public class Vegetable_Data
{
    public Vector2Int position;
    public string Nom;
}
