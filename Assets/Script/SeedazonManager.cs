using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeedazonManager : MonoBehaviour
{
    public GameObject shop;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject paneloption;
    public GameObject panelgraine;
    public GameObject panelachat;
    public GameObject suivant;
    public GameObject Arrosoire;
    public TMP_Text textmdp;
    public TMP_Text textid;
    public string mdp = "********";
    public string id = "KykOuDu47";

    public Player_Controller player;

    private void Update()
    {
        
        if (shop.activeSelf)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        
    }

    public void SeConnecter()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
        StartCoroutine(AfficherText(id,textid));
        StartCoroutine(AfficherText(mdp,textmdp));
    }
    public void Suivant()
    {
        panel2.SetActive(false);
        panel3.SetActive(true);
    }
    public void Retour()
    {
        if (panel2.activeInHierarchy)
        {
            panel2.SetActive(false);
            panel1.SetActive(true);
        }
        
        if (panel3.activeInHierarchy)
        {
            panel3.SetActive(false);
            panel2.SetActive(true);
        }

        if (paneloption.activeInHierarchy)
        {
            paneloption.SetActive(false);
            panel3.SetActive(true);
        }

        if (panelgraine.activeInHierarchy)
        {
            panelgraine.SetActive(false);
            panel3.SetActive(true);
        }
        
        if (panelachat.activeInHierarchy)
        {
            panelachat.SetActive(false);
            panel3.SetActive(true);
            Arrosoire.SetActive(false);
        }
    }

    public void Fermer()
    {
        shop.SetActive(false);
    }

    public void Quitter()
    {
        Application.Quit();
        Debug.Log("Fermer le jeu");
    }

     IEnumerator AfficherText(string nom,TMP_Text text)
    {
        string currentname = "";

        for (int i = 0; i <= nom.Length; i++)
        {
            currentname = nom.Substring(0, i);
            text.text = currentname;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    public void Ustensile()
    {
        panel3.SetActive(false);
        panelachat.SetActive(true);
        Arrosoire.SetActive(true);
    }
    public void Option()
    {
        panel3.SetActive(false);
        paneloption.SetActive(true);
    }
    public void Graine()
    {
        panel3.SetActive(false);
        panelgraine.SetActive(true);
    }
    public void Acheter(Seed graineAcheter)
    {
        Debug.Log("Objet acheté " + graineAcheter);
        if (player.Money >= graineAcheter.Price)
        {
            player.Money -= graineAcheter.Price;
            player.SeedInventory.Add(graineAcheter);
        }
    }
    public void AmeliorationArrosoir()
    {
        player.arrosoirCapacity += 5;
    }
}
