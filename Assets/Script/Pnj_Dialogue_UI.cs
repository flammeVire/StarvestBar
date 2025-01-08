using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pnj_Dialogue_UI : MonoBehaviour
{
    public GameObject Panel;
    public GameObject personnage;
    public GameObject dialogue;
    public GameObject ButonText;
    
    // Liste des PNJ et leurs demandes
    public List<string> alimentsDemandes = new List<string> { "carotte", "citrouille", "raisin", "choux" };
    private string alimentActuel; // Ce que le PNJ demande 

    private void Start()
    {
        // Initialisation du panel en mode caché
        Panel.SetActive(false);
        personnage.SetActive(false);
        dialogue.SetActive(false);
        ButonText.SetActive(false);

        // Générer une première demande
        GenererDemande();
    }

    private void Update()
    {
        PasseDialogue();
    }

    // Gérer l'affichage du dialogue avec la touche "Espace"
    public void PasseDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Dialogue activé");

            // Active les éléments de l'interface
            Panel.SetActive(true);
            personnage.SetActive(true);
            dialogue.SetActive(true);
            ButonText.SetActive(true);

            // Met à jour le texte du dialogue
            dialogue.GetComponent<UnityEngine.UI.Text>().text = "Bonjour ! J'aimerais avoir un smoothie " + alimentActuel + ".";
        }
    }

    // Gère le clic sur le bouton de dialogue
    public void boutonCliquer()
    {
        Debug.Log("Dialogue suivant");

        // réponse du joueur
        dialogue.GetComponent<UnityEngine.UI.Text>().text = "Bonne dégustation à vous !";

        // Cache le bouton pour éviter de cliquer plusieurs fois
        ButonText.SetActive(false);

        // Nouvelle demande
        GenererDemande();
    }

    // Générer une demande aléatoire pour le PNJ
    private void GenererDemande()
    {
        int index = Random.Range(0, alimentsDemandes.Count);
        alimentActuel = alimentsDemandes[index];
        Debug.Log("Nouvelle demande générée : " + alimentActuel);
    }
}