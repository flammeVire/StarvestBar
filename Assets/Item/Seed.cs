using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu]
public class Seed : Vegetable_Scriptable
{
    [SerializeField] private int growthTime;
    public int GrowthTime => growthTime;
    [HideInInspector]public int ActualGrowthTime = 0;
    public Vegetable Vegetable;
    public Tile WateredTiles;

    
}
