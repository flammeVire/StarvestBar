using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pnj_Dialogue_UI : MonoBehaviour
{
    public GameObject Panel;
    public GameObject personnage;
    public TextMeshProUGUI dialogue;
    public Player_Controller player;

    public Sprite[] sprites;

    private void Start()
    {
        hide();
    }

    // Gérer l'affichage du dialogue quand interraction
    public void PasseDialogue()
    {
        // Active les éléments de l'interface
        Panel.SetActive(true);
        personnage.SetActive(true);
        personnage.GetComponent<SpriteRenderer>().sprite = SelectRandomClient();
        dialogue.gameObject.SetActive(true);

        // Met à jour le texte du dialogue
        Debug.Log(player.MakeSmoothie());
        dialogue.text = "Bonjour ! J'aimerais avoir le smoothie: \n" + player.MakeSmoothie();
        
    }

    Sprite SelectRandomClient()
    {
        int index = Random.Range(0, sprites.Length);
        return sprites[index];

    }

    public IEnumerator sell()
    {
        // Initialisation du panel en mode caché
        dialogue.text = " Voici votre smoothie";
        yield return new WaitForSecondsRealtime(1f);
        hide();
        
    }

    public void hide()
    {
        Panel.SetActive(false);
        personnage.SetActive(false);
        dialogue.gameObject.SetActive(false);
    }
}