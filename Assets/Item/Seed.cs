using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu]
public class Seed : Vegetable_Scriptable
{
    [SerializeField] private int growthTime;
    [SerializeField] private int mutationInPercent;
    public int GrowthTime => growthTime;
    public int MutationInPercent => mutationInPercent;
    [HideInInspector]public int ActualGrowthTime = 0;
    public Vegetable Vegetable;
    public Tile WateredTiles;
    public bool HaveMuted;
    
}
