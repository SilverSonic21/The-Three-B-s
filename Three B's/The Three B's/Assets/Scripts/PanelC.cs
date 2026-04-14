using UnityEngine;

public class PanelC : MonoBehaviour
{
     public GameObject panel;

    public void TurnOnPanel()
    {
        panel.SetActive(true);
    }

    public void TurnOffPanel()
    {
        panel.SetActive(false);
    }
}
