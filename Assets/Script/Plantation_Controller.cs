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
            if (seedDico.Key == seedpos)
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
            if (vegetableDico.Key == seedpos)
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
                if (!plantes.Value.HaveMuted)
                {
                    Mutation(plantes.Key, plantes.Value);
                }
                if (plantes.Value.ActualGrowthTime > plantes.Value.GrowthTime)
                {
                    tilemap.SetTile(cellPos, plantes.Value.Vegetable.Tile);
                    VegetablePosition.Add(plantes.Key, Instantiate(plantes.Value.Vegetable));
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


    public void Mutation(Vector2Int seedpos, Seed seedSelect)
    {
        int randomizer = Random.Range(0, 101);
        Debug.Log("Seed Percentage mutation = " + seedSelect.MutationInPercent + "  |actual randomizer = " + randomizer);
        if (randomizer <= seedSelect.MutationInPercent)
        {
            Debug.Log("Try To Mutated");
            Vegetable clone = VegetableMutated(seedpos, seedSelect);
            Debug.Log("Mutated to " + clone);
            if (clone != null) 
            {
                Debug.Log("Mutation Reussi");
                seedSelect.Vegetable = Instantiate(clone);
                seedSelect.HaveMuted = true;
            }
        }
    }

    Vector2Int GetOneTileAdjacent(Vector2Int seedpos)
    {
        int randomDirection = Random.Range(0, 4);
        switch (randomDirection)
        {
            //bas
            case 0:
                return seedpos + Vector2Int.down;
            //gauche
            case 1:
                return seedpos + Vector2Int.left;
            //droite
            case 2:
                return seedpos + Vector2Int.right;
            //haut
            case 3:
                return seedpos + Vector2Int.up;
            default:
                return seedpos;
        }
    }

    Vegetable VegetableMutated(Vector2Int seedPos, Seed seed)
    {
        Vector3Int adjacentPosition = new Vector3Int(GetOneTileAdjacent(seedPos).x, GetOneTileAdjacent(seedPos).y,0);
        Debug.Log("Seed in VegetableMutated ==" + seed);
        if (seed.Nom == GameManager.Instance.PossibleSeed[0].Nom)
        {
            Debug.Log("La graine planté est une carotte");
            Debug.Log("tilemap.gettile = " + tilemap.GetTile(adjacentPosition));
            if(tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[1].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[1].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[5];
            }
            else if (tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[2].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[2].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[9];
            }
            else if (tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[3].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[3].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[4];
            }
        }
        else if (seed.Nom == GameManager.Instance.PossibleSeed[1].Nom)
        {
            Debug.Log("La graine planté est un chou");
            if (tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[0].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[0].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[5];
            }
            else if (tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[2].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[2].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[6];
            }
            else if (tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[3].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[3].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[7];   
            }
        }
        else if (seed.Nom == GameManager.Instance.PossibleSeed[2].Nom)
        {
            Debug.Log("La graine planté est un tomate");

            if (tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[1].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[1].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[6];
            }
            else if (tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[0].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[0].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[9];
            }
            else if (tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[3].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[3].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[8];
            }
        }
        else if (seed.Nom == GameManager.Instance.PossibleSeed[3].Nom)
        {
            Debug.Log("La graine planté est un aubergine");

            if (tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[1].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[1].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[7];
            }
            else if (tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[2].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[2].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[8];
            }
            if (tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[0].Tile || tilemap.GetTile(adjacentPosition) == GameManager.Instance.PossibleSeed[0].WateredTiles)
            {
                return GameManager.Instance.PossibleVegetable[4];
            }
        }
        return null;
    }
}

