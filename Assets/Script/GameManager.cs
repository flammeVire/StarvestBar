using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Player_Controller player;
    [SerializeField] public Plantation_Controller plantation;
    [SerializeField] float Quartan;

    IEnumerator Switching()
    {
        yield return null;
    }
}
