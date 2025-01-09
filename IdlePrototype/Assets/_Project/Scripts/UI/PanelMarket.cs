using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelMarket : MonoBehaviour
{
    [SerializeField] GameObject UI_PanelMarket;

    private void OnEnable()
    {
        //ResourceManager.ResChanged += UpdatePanel;
    }
    private void OnDisable()
    {
        //ResourceManager.ResChanged -= UpdatePanel;
    }
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
        UI_PanelMarket.SetActive(true);
        UpdatePanel();
    }

    public void ClosePanel()
    {
        UI_PanelMarket.SetActive(false);
    }

    private void UpdatePanel() //обновляем панель
    {
        //обновить сумму в полях SellAll
    }
    public void Sell1_Salvage()
    {
        FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
    }
    public void SellAll_Salvage()
    {
        FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
    }
    public void Sell1_Glowstone()
    {
        FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
    }
    public void SellAll_Glowstone()
    {
        FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
    }
}
