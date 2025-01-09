using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using UnityEngine.Localization.Components;

public class PanelGarage : MonoBehaviour
{
    [SerializeField] GameObject UI_PanelGarage;
    //[SerializeField] LocalizeStringEvent myStringReference_nameFact; //������ �� ���� � �������������
    [SerializeField] TextMeshProUGUI[] coastUpdate; //������ �������� ������� ���������� EnumCarParameters
    [SerializeField] TextMeshProUGUI[] deltaUpdate; //������ �������� ������� ���������� EnumCarParameters
    [SerializeField] TextMeshProUGUI[] currParam; //������ �������� ������� ���������� EnumCarParameters
    [SerializeField] Image[] btnUpdate; //������ �������� ������� ���������� EnumCarParameters
    //[SerializeField] Color disableColorBtn;

    private void OnEnable()
    {
        //ResourceManager.ResChanged += UpdateBtn;
    }
    private void OnDisable()
    {
        //ResourceManager.ResChanged -= UpdateBtn;
    }

    void Awake()
    {
        
    }
    void Start()
    {
        //UI_PanelGarage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
        UI_PanelGarage.SetActive(true);
        UpdatePanel();
        UpdateBtn();
    }

    public void ClosePanel()
    {
        UI_PanelGarage.SetActive(false);
    }

    private void UpdatePanel() //��������� �������, ������������, ����� � ���� ���������
    {
        //��������� ��������� ���������� - ��������� ��������
        //��������� ��������� ���������� - ������ ���������
        //��������� ��������� ���������� - �������
        for (int i = 0; i < coastUpdate.Length; i++)
        {
            //coastUpdate[i].text = ;
        }
        for (int i = 0; i < deltaUpdate.Length; i++)
        {
            //deltaUpdate[i].text = "+" + ;
        }
        for (int i = 0; i < currParam.Length; i++)
        {
            //currParam[i].text = ;
        }
    }

    void UpdateBtn() //����������� ������ ���� ���� ������
    {
        //��������� ��������� ���������� - ��������� ��������
        //��������� ���-�� ��������
        for (int i = 0; i < btnUpdate.Length; i++)
        {
            //if () btnUpdate[i].color = Color.white;
            //else btnUpdate[i].color = Color.gray;
        }

    }

    public void UpgradeParameterBtn_Speed() //������� ������ �������� ���������
    {
        //�������� �������� ����������
        UpdatePanel();
        FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
    }

    public void UpgradeParameterBtn_HP() //������� ������ �������� ���������
    {
        //�������� �������� ����������
        UpdatePanel();
        FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
    }
    public void UpgradeParameterBtn_Trunk() //������� ������ �������� ���������
    {
        //�������� �������� ����������
        UpdatePanel();
        FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
    }
    public void UpgradeParameterBtn_SpeedAttack() //������� ������ �������� ���������
    {
        //�������� �������� ����������
        UpdatePanel();
        FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
    }
    public void UpgradeParameterBtn_DamageAttack() //������� ������ �������� ���������
    {
        //�������� �������� ����������
        UpdatePanel();
        FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
    }

}
