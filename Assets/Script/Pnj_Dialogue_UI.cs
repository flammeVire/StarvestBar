using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pnj_Dialogue_UI : MonoBehaviour
{
    public GameObject Panel;
    public GameObject personnage;
    public GameObject dialogue;
    public GameObject ButonText;
    private void Start()
    { 
    
    }
        private void Update()
    {
        PasseDialogue();
    }
    //quand appuye sur espace
    public void PasseDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("banana");
     // affiche un gameobject (canva)
            Panel.SetActive(true);
    // met l'img du pnj a sa place correspndante
            personnage.SetActive(true);
    // met le dialogue correspondant au pnj
            dialogue.SetActive(true);
            ButonText.SetActive(true);
        }
    }



    // detect quand joueur appuye sur le bouton de dialogue
    public void boutonCliquer()
    {
        Debug.Log("fatigue");
    }
    

}

