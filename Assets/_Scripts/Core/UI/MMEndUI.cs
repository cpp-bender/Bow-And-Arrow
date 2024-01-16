using UnityEngine;
using TMPro;

public class MMEndUI : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public GameObject backToTrainingButton;
    [SerializeField] TextMeshProUGUI winNameText;
    [SerializeField] TextMeshProUGUI winPriceText;

    public void SwitchMMEndPanelObjects(bool on)
    {
        backToTrainingButton.gameObject.SetActive(on);
        winNameText.gameObject.SetActive(on);
        winPriceText.gameObject.SetActive(on);
    }

    public void SetWinText(string str)
    {
        winNameText.SetText(str);
    }

    public void SetWinPriceText(int price)
    {
        winPriceText.SetText("+$" + price.ToString());
    }
}
