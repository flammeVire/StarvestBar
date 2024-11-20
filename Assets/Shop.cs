using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nametext;
    string currentname = "";
    IEnumerator NameAnimation(string name)
    {
        for (int i = 0; i <= name.Length; i++)
        {
            currentname = name.Substring(0, i);
            nametext.text = currentname;
            yield return null;

        }
    }
}
