using IdleArcade.Services;
using IdleArcade.UI.Services.Factory;
using IdleArcade.UI.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartTrigger : MonoBehaviour
{
    [SerializeField] private BuildingType enumBuilding;
    private IUIFactory _uiFactory;

    private void Awake()
    {
        _uiFactory = AllServices.Container.Single<IUIFactory>(); ;
        if (_uiFactory == null)
        {
            Debug.LogError("UIFactory not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") PanelStart(true);        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") PanelStart(false);
    }

    private void PanelStart(bool isActive)
    {
        switch (enumBuilding)
        {
            case BuildingType.Garage:
                //FindAnyObjectByType<PanelGarage>().PanelActiveOrDeactive(isActive);
                if (isActive)
                {
                    _uiFactory.CreateGarage();
                }
                else
                {
                    var garageWindow = FindAnyObjectByType<GarageWindow>();
                    if (garageWindow != null)
                    {
                        garageWindow.PanelActiveOrDeactive(false);
                    }
                }
                break;

            case BuildingType.Market:
                FindAnyObjectByType<PanelMarket>().PanelActiveOrDeactive(isActive);
                break;

            case BuildingType.Workshop:
                FindAnyObjectByType<PanelMechWorkshop>().PanelActiveOrDeactive(isActive);
                break;

            default:
                break;
        }
    }
}
