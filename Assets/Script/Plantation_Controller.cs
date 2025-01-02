using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Plantation_Controller : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameManager gameManager;
    public Dictionary<Vector2Int, Seed> SeedPosition = new Dictionary<Vector2Int, Seed>();
    public Dictionary<Vector2Int, Vegetable> VegetablePosition = new Dictionary<Vector2Int, Vegetable>();

    [Header("Tile")]
    public Tile Hard_Dirt;
    public Tile Dirt;


    public void Seeding(Vector2Int seedpos, Seed SeedSelected)
    {
        Vector3Int seedpos3 = new Vector3Int(seedpos.x, seedpos.y, 0);
        Vector3Int cellPos = tilemap.WorldToCell(seedpos3);
        tilemap.SetTile(cellPos, SeedSelected.Tile);

        //créé un clone du scriptable et on l'ajoute a la liste
        Seed clone = Instantiate(SeedSelected);
        SeedPosition.Add(seedpos, clone);
    }
    
    public void Watering(Vector2Int seedpos)
    {
        Vector3Int seedpos3 = new Vector3Int(seedpos.x, seedpos.y, 0);
        Vector3Int cellPos = tilemap.WorldToCell(seedpos3);

        foreach (var seedDico in SeedPosition) 
        { 
            if(seedDico.Key == seedpos)
            {
                tilemap.SetTile(cellPos, seedDico.Value.WateredTiles);
                break;
            }
        }

    }

    public void Beching(Vector2Int seedpos)
    {
        Vector3Int seedpos3 = new Vector3Int(seedpos.x, seedpos.y, 0);
        Debug.Log("seedpos3 ==" + seedpos3);
        Vector3Int cellPos = tilemap.WorldToCell(seedpos3);
        tilemap.SetTile(cellPos, Dirt);
    }

    public Vegetable PickUpVegetable(Vector2Int seedpos) 
    {
        Vector3Int seedpos3 = new Vector3Int(seedpos.x, seedpos.y, 0);
        Debug.Log("seedpos3 ==" + seedpos3);
        Vector3Int cellPos = tilemap.WorldToCell(seedpos3);
        tilemap.SetTile(cellPos, Dirt);

        
        foreach (var vegetableDico in VegetablePosition)
        {
            if(vegetableDico.Key == seedpos)
            {
                VegetablePosition.Remove(vegetableDico.Key);
                return vegetableDico.Value;
            }
        }
        return null;
    }

    public void growSeed()
    {
        List<Vector2Int> removePlante = new List<Vector2Int>(0);
        foreach (var plantes in SeedPosition)
        {
            Vector3Int seedpos3 = new Vector3Int(plantes.Key.x, plantes.Key.y, 0);
            Vector3Int cellPos = tilemap.WorldToCell(seedpos3);

            //si la plante est arrosé
            if (tilemap.GetTile(cellPos) == plantes.Value.WateredTiles)
            {
                plantes.Value.ActualGrowthTime += 1;
                if (plantes.Value.ActualGrowthTime > plantes.Value.GrowthTime)
                {
                    tilemap.SetTile(cellPos, plantes.Value.Vegetable.Tile);
                    VegetablePosition.Add(plantes.Key,Instantiate(plantes.Value.Vegetable));
                    removePlante.Add(plantes.Key);
                }
            }
        }

        if (removePlante.Count > 0)
        {
            for (int i = 0; i < removePlante.Count; i++)
            {
                SeedPosition.Remove(removePlante[i]);
            }
        }
        
        


    }
    
}
