using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarreInv : MonoBehaviour
{
    public List<GameObject> caseBarre;
    public int index { get; private set; } = 0;
    public bool bloquer = false;
    public Player_Controller playerController;
    public GameManager gameManager;

    void Start()
    {
        caseBarre[index].GetComponent<Image>().color = Color.gray;
    }

    void Update()
    {
       #region barre
        if (bloquer == false)
        {
            BougeBarre();
        }

        #endregion barre
        void BougeBarre()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (index < 7)
                {
                    index += 1;
                    caseBarre[index].GetComponent<Image>().color = Color.gray;
                    caseBarre[index - 1].GetComponent<Image>().color = Color.white;
                }
                else
                {
                    index = 0;
                    caseBarre[index].GetComponent<Image>().color = Color.gray;
                    caseBarre[7].GetComponent<Image>().color = Color.white;
                }
                ObjectSelected();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (index > 0)
                {
                    index -= 1;
                    caseBarre[index].GetComponent<Image>().color = Color.gray;
                    caseBarre[index + 1].GetComponent<Image>().color = Color.white;
                }
                else
                {
                    index = 7;
                    caseBarre[index].GetComponent<Image>().color = Color.gray;
                    caseBarre[0].GetComponent<Image>().color = Color.white;
                }
                ObjectSelected();
            }
            if (Input.GetButtonDown("Case1"))
            {
                caseBarre[index].GetComponent<Image>().color = Color.white;
                index = 0;
                caseBarre[index].GetComponent<Image>().color = Color.gray;
                ObjectSelected();
            }
            else if (Input.GetButtonDown("Case2"))
            {
                caseBarre[index].GetComponent<Image>().color = Color.white;
                index = 1;
                caseBarre[index].GetComponent<Image>().color = Color.gray;
                ObjectSelected();
            }
            else if (Input.GetButtonDown("Case3"))
            {
                caseBarre[index].GetComponent<Image>().color = Color.white;
                index = 2;
                caseBarre[index].GetComponent<Image>().color = Color.gray;
                ObjectSelected();
            }
            else if (Input.GetButtonDown("Case4"))
            {
                caseBarre[index].GetComponent<Image>().color = Color.white;
                index = 3;
                caseBarre[index].GetComponent<Image>().color = Color.gray;
                ObjectSelected();
            }
            else if (Input.GetButtonDown("Case5"))
            {
                caseBarre[index].GetComponent<Image>().color = Color.white;
                index = 4;
                caseBarre[index].GetComponent<Image>().color = Color.gray;
                ObjectSelected();
            }
            else if (Input.GetButtonDown("Case6"))
            {
                caseBarre[index].GetComponent<Image>().color = Color.white;
                index = 5;
                caseBarre[index].GetComponent<Image>().color = Color.gray;
                ObjectSelected();
            }
            else if (Input.GetButtonDown("Case7"))
            {
                caseBarre[index].GetComponent<Image>().color = Color.white;
                index = 6;
                caseBarre[index].GetComponent<Image>().color = Color.gray;
                ObjectSelected();
            }
            else if (Input.GetButtonDown("Case8"))
            {
                caseBarre[index].GetComponent<Image>().color = Color.white;
                index = 7;
                caseBarre[index].GetComponent<Image>().color = Color.gray;
                ObjectSelected();
            }
        }
        void ObjectSelected()
        {
            if(index  == 0)
            {
                Debug.Log("Main");
            }
            else if (index == 1)
            {
                Debug.Log("Arrosoire");
            }
            else if (index == 2)
            {
                Debug.Log("Beche");
            }
            else if (index == 3)
            {
                playerController.ActualSeed = gameManager.PossibleSeed[0];
            }
            else if (index == 4)
            {
                playerController.ActualSeed = gameManager.PossibleSeed[1];

            }
            else if (index == 5)
            {
                playerController.ActualSeed = gameManager.PossibleSeed[2];

            }
            else if (index == 6)
            {
                playerController.ActualSeed = gameManager.PossibleSeed[3];

            }
        }
    }
}
