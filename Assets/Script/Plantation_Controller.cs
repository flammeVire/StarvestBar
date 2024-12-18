using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Plantation_Controller : MonoBehaviour
{
    // recuperer la position de la tile selectionner
    // change la tile par les graines correspondant

    //si beche change la tile par champs
    //si planter change la tile par sol humide


    [SerializeField] Tilemap tilemap;
    Dictionary<Vector2Int, Seed> SeedPosition = new Dictionary<Vector2Int, Seed>();
    public Tile Seededfield;
    public Tile WateredField;
    public Tile Dirt;
    [SerializeField] GameManager gameManager;
    public void Seeding(Vector2Int seedpos, Seed SeedSelected)
    {
        
        Vector3Int seedpos3 = new Vector3Int(seedpos.x, seedpos.y, 0);
        Vector3Int cellPos = tilemap.WorldToCell(seedpos3);
        tilemap.SetTile(cellPos, Seededfield);

        SeedPosition.Add(seedpos, SeedSelected);
    }

    public void Watering(Vector2Int seedpos)
    {
        Vector3Int seedpos3 = new Vector3Int(seedpos.x, seedpos.y, 0);
        Debug.Log("seedpos3 ==" + seedpos3);
        Vector3Int cellPos = tilemap.WorldToCell(seedpos3);
        tilemap.SetTile(cellPos, WateredField);

    }

    public void Beching(Vector2Int seedpos)
    {
        Vector3Int seedpos3 = new Vector3Int(seedpos.x, seedpos.y, 0);
        Debug.Log("seedpos3 ==" + seedpos3);
        Vector3Int cellPos = tilemap.WorldToCell(seedpos3);
        tilemap.SetTile(cellPos, Dirt);
    }
}
