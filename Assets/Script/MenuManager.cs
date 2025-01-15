using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string SceneName;
    public GameObject MenuManagers;
    public GameObject PanelMenuPrincipal;
    public GameObject PanelMenuNotice;

    public void Quitter()
    {
        Application.Quit();
        Debug.Log("Fermer le jeu");
    }
    public void Jouer()
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("Lancer le jeu");
    }
    public void Notice()
    {
        PanelMenuPrincipal.SetActive(false);
        PanelMenuNotice.SetActive(true);
    }
    
    public void Retour()
    {
        if (PanelMenuNotice.activeInHierarchy)
        {
            PanelMenuNotice.SetActive(false);
            PanelMenuPrincipal.SetActive(true);
        }
    }
}