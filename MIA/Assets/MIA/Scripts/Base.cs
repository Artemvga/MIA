using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public static Base instance;
    public static Base Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Base>();
            return instance;
        }
    }
    private  bool radioComplite = false;
    private bool inspectionDTPComplite = false;
    private bool installationConeRedComplite = false;
    private bool installationConeOrangeComplite = false;

    public void Radio()
    {
        if(!radioComplite)
        {
            radioComplite = true;
            //Вызвать звук одобрения от инспектора
        }
    }

    public void InspectionDTP()
    {
        if(!inspectionDTPComplite && radioComplite)
        {
            inspectionDTPComplite = true;
            //Вызвать звук одобрения от инспектора
        }
        else
        {
            //Вызвать звук неодобрения
        }
    }

    public void InstallationConeRed()
    {
        if (!installationConeRedComplite && inspectionDTPComplite)
        {
            installationConeRedComplite = true;
            //Вызвать звук одобрения от инспектора
        }
        else
        {
            //Вызвать звук неодобрения
        }
    }

    public void InstallationConeOrange()
    {
        if(!installationConeOrangeComplite && installationConeRedComplite)
        {
            installationConeOrangeComplite = true;
            //Вызвать звук одобрения от инспектора  
        }
        else
        {
            //Вызвать звук неодобрения
        }
    }

}
