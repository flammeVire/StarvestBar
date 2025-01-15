using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public Button acheterButton1;
    public Button venduButton1;
    public Button acheterButton2;
    public Button venduButton2;

    private int ameliorationAchatCount = 0;
    private const int maxAmeliorationAchat = 2;
    public TMP_Text shopMoneyText;
    public TMP_Text arrosoireMoneyText;

    public TMP_Text grainesAcheteesText1;
    public TMP_Text grainesAcheteesText2;
    public TMP_Text grainesAcheteesText3;
    public TMP_Text grainesAcheteesText4;
    public UIManagement UIManager;

    private void Start()
    {
        acheterButton1.gameObject.SetActive(true);
        venduButton1.gameObject.SetActive(false);
        acheterButton2.gameObject.SetActive(true);
        venduButton2.gameObject.SetActive(false);

        grainesAcheteesText1.gameObject.SetActive(false);
        grainesAcheteesText2.gameObject.SetActive(false);
        grainesAcheteesText3.gameObject.SetActive(false);
        grainesAcheteesText4.gameObject.SetActive(false);

        UpdateMoneyDisplay(shopMoneyText);
    }

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

    public void AcheterObjet2()
    {

        if (player.Money >= 2000)
        {
            player.Money -= 2000;

            player.isUpgraded = true;
            acheterButton2.gameObject.SetActive(false);
            venduButton2.gameObject.SetActive(true);

            player.uiManagement.UpdateMoneyDisplay(player.Money);
        }

    }

    public void SeConnecter()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
        StartCoroutine(AfficherText(id, textid));
        StartCoroutine(AfficherText(mdp, textmdp));
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

    IEnumerator AfficherText(string nom, TMP_Text text)
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
        UpdateMoneyDisplay(arrosoireMoneyText);
        Debug.Log(player.Money);
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
        UpdateMoneyDisplay(shopMoneyText);
        UpdateGrainesAcheteesText(player.uiManagement.numberOfSeeds1);
        UpdateGrainesAcheteesText(player.uiManagement.numberOfSeeds2);
        UpdateGrainesAcheteesText(player.uiManagement.numberOfSeeds3);
        UpdateGrainesAcheteesText(player.uiManagement.numberOfSeeds4);
    }

    public void Acheter(Seed graineAcheter)
    {
        Debug.Log("Objet acheté " + graineAcheter);
        if (player.Money >= graineAcheter.Price)
        {
            player.Money -= graineAcheter.Price; 
            player.SeedInventory.Add(graineAcheter);
            player.uiManagement.UpdateMoneyDisplay(player.Money);

            shopMoneyText.text = "Argent : " + player.Money + " pièces";

            if (Arrosoire.activeSelf)
            {
                arrosoireMoneyText.text = "Argent : " + player.Money + " pièces";
            }

            if (graineAcheter == GameManager.Instance.PossibleSeed[0])
            {
                grainesAcheteesText1.gameObject.SetActive(true);
                player.uiManagement.UpdateSeedInventory(player.uiManagement.seeds1Text, player.uiManagement.numberOfSeeds1 += 1);
                UpdateGrainesAcheteesText(1);
            }
            else if (graineAcheter == GameManager.Instance.PossibleSeed[1])
            {
                grainesAcheteesText2.gameObject.SetActive(true);
                player.uiManagement.UpdateSeedInventory(player.uiManagement.seeds2Text, player.uiManagement.numberOfSeeds2 += 1);
                UpdateGrainesAcheteesText(2);
            }
            else if (graineAcheter == GameManager.Instance.PossibleSeed[2])
            {
                grainesAcheteesText3.gameObject.SetActive(true);
                player.uiManagement.UpdateSeedInventory(player.uiManagement.seeds3Text, player.uiManagement.numberOfSeeds3 += 1);
                UpdateGrainesAcheteesText(3);
            }
            else if (graineAcheter == GameManager.Instance.PossibleSeed[3])
            {
                grainesAcheteesText4.gameObject.SetActive(true);
                player.uiManagement.UpdateSeedInventory(player.uiManagement.seeds4Text, player.uiManagement.numberOfSeeds4 += 1);
                UpdateGrainesAcheteesText(4);
            }
        }
        else
        {
            Debug.Log("Pas assez d'argent pour acheter cet objet !");
        }
    }


    public void AmeliorationArrosoir()
    {
        if (ameliorationAchatCount < maxAmeliorationAchat)
        {
            Debug.Log(player.Money);
            if (player.Money >= 500)
            {
                player.Money -= 500;
                player.arrosoirCapacity += 5;

                player.uiManagement.UpdateMoneyDisplay(player.Money);

                if (Arrosoire.activeSelf)
                {
                    arrosoireMoneyText.text = "Argent : " + player.Money + " pièces";
                }

                ameliorationAchatCount++;

                Debug.Log($"Amélioration de l'arrosoir achetée {ameliorationAchatCount} fois.");
            }
            if (ameliorationAchatCount == maxAmeliorationAchat)
            {
                acheterButton1.gameObject.SetActive(false);
                venduButton1.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Pas assez d'argent pour améliorer l'arrosoir !");
            }
        }
    }


    private void UpdateMoneyDisplay(TMP_Text text)
    {
        text.text = "Argent: " + player.Money + " pièces";
    }

    private void UpdateGrainesAcheteesText(int graineType)
    {
        switch (graineType)
        {
            case 1:
                grainesAcheteesText1.text = "Carotte achetée : " + UIManager.numberOfSeeds1.ToString();
                break;
            case 2:
                grainesAcheteesText2.text = "Chou achetée : " + UIManager.numberOfSeeds2.ToString();
                break;
            case 3:
                grainesAcheteesText3.text = "Citrouille achetée : " + UIManager.numberOfSeeds3.ToString();
                break;
            case 4:
                grainesAcheteesText4.text = "Raisin achetée : " + UIManager.numberOfSeeds4.ToString();
                break;
        }
    }
    public void ResetGrainesText()
    {
        grainesAcheteesText1.gameObject.SetActive(false);
        grainesAcheteesText2.gameObject.SetActive(false);
        grainesAcheteesText3.gameObject.SetActive(false);
        grainesAcheteesText4.gameObject.SetActive(false);
    }
}
