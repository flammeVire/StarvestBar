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
            paneloption.SetActive(true);
        }
        
        if (panelachat.activeInHierarchy)
        {
            panelachat.SetActive(false);
            panelgraine.SetActive(true);
        }
    }

    public void Fermer()
    {
        shop.SetActive(false);
    }

    public void Quitter()
    {
        Application.Quit();
    }

     IEnumerator AfficherText()
    {
        yield return new WaitUntil(() => panel2.activeInHierarchy);
        yield return new WaitForSeconds(1f);
        textmdp.text = mdp;
        textid.text = id;
        yield return null;
    }
}
