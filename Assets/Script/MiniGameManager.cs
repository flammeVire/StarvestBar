using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public Image barre;
    private float maxPoint = 100;
    public float curPoint = 0;
    private float score;
    private bool up = false;
    private bool down = false;
    private bool stop = true;
    public float minigameSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        barre.fillAmount = curPoint / maxPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && stop == true)
        {
            up = true;
            stop = false;
        }
        else if (Input.GetButtonDown("Jump") && up == true || Input.GetButtonDown("Jump") && down == true)
        {
            stop = true;
            up = false;
            down = false;
            score = curPoint;
            Debug.Log(score);
            curPoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (up == true)
        {
            if (curPoint <= maxPoint)
            {
                curPoint += minigameSpeed;
            }
            else
            {
                down = true;
                up = false;
            }
            barre.fillAmount = curPoint / maxPoint;
        }

        if (down == true)
        {
            if (curPoint > 0.1f)
            {
                curPoint -= minigameSpeed;
            }
            else
            {
                down = false;
                up = true;
            }
            barre.fillAmount = curPoint / maxPoint;
        }
    }
}
