using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarreInv : MonoBehaviour
{
    public List<GameObject> caseBarre;
    private int index = 0;
    public bool bloquer = false;
    // Start is called before the first frame update
    void Start()
    {
        caseBarre[index].GetComponent<Image>().color = Color.gray;
    }

    // Update is called once per frame
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
                Debug.Log("Arrosoir");
            }
            else if (index == 1)
            {
                Debug.Log("Rateau");
            }
            else if (index == 2)
            {
                Debug.Log("Main");
            }
            else if (index == 3)
            {
                Debug.Log("Graine1");
            }
            else if (index == 4)
            {
                Debug.Log("Graine2");
            }
            else if (index == 5)
            {
                Debug.Log("Graine3");
            }
            else if (index == 6)
            {
                Debug.Log("Graine4");
            }
        }
    }
}
