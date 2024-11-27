using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Player_Controller player;
    [SerializeField] public Plantation_Controller plantation;
    [SerializeField] float DayTime;
    public DayCycle CurrentDayCycle;

    public enum DayCycle
    {
        matin,
        midi,
        soir,
        nuit,
    }

    private void Start()
    {
        StartCoroutine(ChangeDayCycle());
    }
    #region DayManager
    IEnumerator ChangeDayCycle()
    {
        yield return new WaitForSeconds(DayTime/4);
        CurrentDayCycle += 1;
        switch (CurrentDayCycle)
        {
            case DayCycle.matin:
                break;
            case DayCycle.midi:
                break;
            case DayCycle.soir:
                break;
            case DayCycle.nuit:
                break;
            default:
                CurrentDayCycle = DayCycle.matin;
                break;
        }
        plantation.growSeed();
        Debug.Log("Daytime == " + CurrentDayCycle);
        StartCoroutine(ChangeDayCycle());
    }

    

    #endregion
}
