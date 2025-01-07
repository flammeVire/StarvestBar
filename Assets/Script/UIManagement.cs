using UnityEngine;
using TMPro;

public class UIManagement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    public void UpdateMoneyDisplay(int money)
    {
        moneyText.text = $"Argent : {money}";
    }
}
