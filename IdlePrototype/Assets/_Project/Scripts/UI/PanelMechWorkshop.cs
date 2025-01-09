using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelMechWorkshop : MonoBehaviour
{
    [SerializeField] GameObject UI_PanelMechWorkshop;
    void Start()
    {
 
    }
    public void PanelActiveOrDeactive(bool isActive)
    {
        if (isActive) StartPanel();
        else ClosePanel();
        FindAnyObjectByType<AudioEffects>().AudioMenuActive();
    }
    public void StartPanel()
    {
        UI_PanelMechWorkshop.SetActive(true);
    }

    public void ClosePanel()
    {
        UI_PanelMechWorkshop.SetActive(false);
    }
    
}
