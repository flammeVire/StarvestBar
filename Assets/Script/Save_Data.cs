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
    List<Seed_Data> seedsOnMap = new List<Seed_Data>();
    List<Vegetable_Data> vegetablesOnMap = new List<Vegetable_Data>();

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
        Data.arroisoir_Capacity = 0; //changer avec le code de mael
        Data.arroisoirIsUpgraded = false; // changer avec code de mael
        Data.Current_Seeds = new List<Seed_Data>();
        Data.Current_Vegetable = new List<Vegetable_Data>();
        foreach (var plantes in plantation.SeedPosition)
        {
            Seed_Data seed_Data = AddingSeedCloneData(plantes.Value, plantes.Key);
            Data.Current_Seeds.Add(seed_Data);
        }
        foreach(var plantes in plantation.VegetablePosition)
        {
            Vegetable_Data vegetable_Data = AddingVegetableCloneData(plantes.Value,plantes.Key);
            Data.Current_Vegetable.Add(vegetable_Data);
        }
        Data.DayCycle = gameManager.CurrentDayCycle;
        SaveToJson();
    }
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

        seedsOnMap = Data.Current_Seeds;
        vegetablesOnMap = Data.Current_Vegetable;

        player.Money = Data.money;
        player.SeedInventory = Data.Player_seeds;
        player.vegetablesInventory = Data.Player_Vegetables;
        player.PlayerPosition = Data.PlayerPosition;

        //arroisoir capacity
        //upgrade arroisoir
        plantation.SeedPosition = ReturnDictSeedFromJson();
        plantation.VegetablePosition = ReturnDictVegetableFromJson() ;

        gameManager.CurrentDayCycle = Data.DayCycle;

    }
    #endregion

    #region seedManagement
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
            sdt.IsWatered = true ;
        }
        sdt.HaveMutated = seed.HaveMuted;
        sdt.vegetableCorrespondant = seed.Vegetable;
        return sdt;
    }
    Dictionary<Vector2Int, Seed> ReturnDictSeedFromJson()
    {
        Dictionary<Vector2Int, Seed> dico = new Dictionary<Vector2Int, Seed>();

        //crée un clone de la graine
        //quel est la graine

        foreach (var valeur in seedsOnMap) 
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

                if (valeur.IsWatered) 
                {
                    Debug.Log("Watered tiles set");
                    player.tilemap.SetTile(new Vector3Int(valeur.position.x, valeur.position.y, 0), clone.WateredTiles);
                }
                else
                {
                    Debug.Log("classic tiles set");
                    player.tilemap.SetTile(new Vector3Int(valeur.position.x, valeur.position.y, 0), clone.Tile);
                }
            }
            dico.Add(valeur.position, clone);
        }
        return dico;
    }



    Vegetable_Data AddingVegetableCloneData(Vegetable vegetable,Vector2Int pos)
    {
        Vegetable_Data vdt = new Vegetable_Data();
        vdt.position = pos;
        vdt.Nom = vegetable.Nom;
        return vdt;
    }
    Dictionary<Vector2Int,Vegetable> ReturnDictVegetableFromJson()
    {
        Dictionary<Vector2Int, Vegetable> dico = new Dictionary<Vector2Int, Vegetable>();
        foreach(var valeur in vegetablesOnMap)
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
}


public class Game_Data
{
    public int money;
    public List<Seed> Player_seeds;
    public List<Vegetable> Player_Vegetables;
    public Vector2Int PlayerPosition;
    public int arroisoir_Capacity;
    public bool arroisoirIsUpgraded;

    public List<Seed_Data> Current_Seeds;
    public List<Vegetable_Data> Current_Vegetable;
    public GameManager.DayCycle DayCycle;
}

public class Seed_Data
{
    public int Grow;
    public string Nom;
    public Vector2Int position;
    public bool IsWatered;
    public bool HaveMutated;
    public Vegetable vegetableCorrespondant;
}

public class Vegetable_Data
{
    public Vector2Int position;
    public string Nom;
}
