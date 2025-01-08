 using UnityEngine;
using TMPro;

public class UIManagement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    public int numberOfSeeds1;
    public int numberOfSeeds2;
    public int numberOfSeeds3;
    public int numberOfSeeds4;
    public TextMeshProUGUI seeds1Text;
    public TextMeshProUGUI seeds2Text;
    public TextMeshProUGUI seeds3Text;
    public TextMeshProUGUI seeds4Text;
    public TextMeshProUGUI ArrosoirText;

    public GameObject dayWheel;
    public Sprite[] days;
    private void Awake()
    {
        UpdateCapacityArrosoir(5 , 5);
        UpdateSeedInventory(seeds1Text, 0);
        UpdateSeedInventory(seeds2Text, 0);
        UpdateSeedInventory(seeds3Text, 0);
        UpdateSeedInventory(seeds4Text, 0);
    }

    public void UpdateMoneyDisplay(int money)
    {
        moneyText.text = "Argent:\n" + money;
    }

    public void UpdateSeedInventory(TextMeshProUGUI text, int number)
    {
        text.text = number.ToString();
    }

    public void UpdateCapacityArrosoir(int actualcapacity, int maximalcapacity)
    {
        ArrosoirText.text = actualcapacity.ToString() + " / " + maximalcapacity.ToString();
    }


    public void DaytimeUI(GameManager.DayCycle dayCycle)
    {
        switch (dayCycle)
        {
            case GameManager.DayCycle.matin:
                dayWheel.gameObject.GetComponent<SpriteRenderer>().sprite = days[0];
                break;
             case GameManager.DayCycle.midi:
                dayWheel.gameObject.GetComponent<SpriteRenderer>().sprite = days[1];
                break;
            case GameManager.DayCycle.soir:
                dayWheel.gameObject.GetComponent<SpriteRenderer>().sprite = days[2];
                break;
            case GameManager.DayCycle.nuit:
                dayWheel.gameObject.GetComponent<SpriteRenderer>().sprite = days[3];
                break;
        }
    }

}
