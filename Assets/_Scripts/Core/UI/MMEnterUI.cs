using UnityEngine.UI;
using UnityEngine;

public class MMEnterUI : MonoBehaviour
{
    [Header("DEPENDENCIES")]
    public Button matchButton;

    public void SwitchMatchButton(bool on)
    {
        matchButton.gameObject.SetActive(on);
    }
}
