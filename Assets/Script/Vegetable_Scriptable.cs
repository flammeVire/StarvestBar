using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Vegetable_Scriptable : ScriptableObject
{
    [SerializeField]private string nom;

    [SerializeField] private int price;

    [SerializeField] private Sprite sprite;


    public string Nom => nom;
    public int Price => price;
    public Sprite Sprite => sprite;

}
