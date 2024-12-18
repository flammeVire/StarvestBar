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
    public GameObject Graine1;
    public GameObject Graine2;
    public GameObject Graine3;
    public GameObject Graine4;
    public GameObject Arrosoire;
    public TMP_Text textmdp;
    public TMP_Text textid;
    public string mdp = "********";
    public string id = "KykOuDu47";

    private void Start()
    {
        StartCoroutine(AfficherText());
    }
    public void SeConnecter()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
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
            Graine1.SetActive(false);
            Graine2.SetActive(false);
            Graine3.SetActive(false);
            Graine4.SetActive(false);
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

     IEnumerator AfficherText()
    {
        yield return new WaitUntil(() => panel2.activeInHierarchy);
        yield return new WaitForSeconds(1f);
        textmdp.text = mdp;
        textid.text = id;
        yield return null;
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
    public void Graine01()
    {
        panelgraine.SetActive(false);
        panelachat.SetActive(true);
        Graine1.SetActive(true);
    }
    public void Graine02()
    {
        panelgraine.SetActive(false);
        panelachat.SetActive(true);
        Graine2.SetActive(true);
    }
    public void Graine03()
    {
        panelgraine.SetActive(false);
        panelachat.SetActive(true);
        Graine3.SetActive(true);
    }
    public void Graine04()
    {
        panelgraine.SetActive(false);
        panelachat.SetActive(true);
        Graine4.SetActive(true);
    }
    public void Acheter()
    {
        Debug.Log("Objet acheté");
    }
}
