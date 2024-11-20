using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Seed : Vegetable_Scriptable
{
    // Start is called before the first frame update
    [SerializeField] private int growthTime;

    public int GrowthTime => growthTime;

}
