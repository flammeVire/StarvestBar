using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Seed : Vegetable_Scriptable
{
    [SerializeField] private int growthTime;

    public int GrowthTime => growthTime;
    public int ActualGrowthTime = 0;
}
