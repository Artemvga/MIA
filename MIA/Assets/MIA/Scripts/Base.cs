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
            Debug.Log("Вы сообщили о прибытии");
        }
    }

    public void InspectionDTP()
    {
        if(!inspectionDTPComplite && radioComplite)
        {
            inspectionDTPComplite = true;
            //Вызвать звук одобрения от инспектора
<<<<<<< HEAD
            Debug.Log("Вы сообщили о прибытии");
=======
            Debug.Log("Вы проверили участников ДТП");
>>>>>>> Map
        }
        else
        {
            //Вызвать звук неодобрения
<<<<<<< HEAD
            Debug.Log("Вы не выполнили предыдущие действия");
=======
            Debug.Log("Вы еще не сообщили о прибытии");
>>>>>>> Map
        }
    }

    public void InstallationConeRed()
    {
        if (!installationConeRedComplite && inspectionDTPComplite)
        {
            installationConeRedComplite = true;
            //Вызвать звук одобрения от инспектора
<<<<<<< HEAD
            Debug.Log("Вы установили красный конус");
=======
            Debug.Log("Вы установили красный конус в месте столкновения");
>>>>>>> Map
        }
        else
        {
            //Вызвать звук неодобрения
<<<<<<< HEAD
            Debug.Log("Вы не выполнили предыдущие действия");
=======
            Debug.Log("Вы не выполнили предыдущий шаг");
>>>>>>> Map
        }
    }

    public void InstallationConeOrange()
    {
        if(!installationConeOrangeComplite && installationConeRedComplite)
        {
            installationConeOrangeComplite = true;
<<<<<<< HEAD
            //Вызвать звук одобрения от инспектора
            Debug.Log("Вы расставили оранжевые конусы");
=======
            //Вызвать звук одобрения от инспектора  
            Debug.Log("Вы установили оранжевые конусы");
>>>>>>> Map
        }
        else
        {
            //Вызвать звук неодобрения
<<<<<<< HEAD
            Debug.Log("Вы не выполнили предыдущие действия");
        }
    }

    public void MakePhoto()
    {
        if (!makePhotoCompition && installationConeOrangeComplite)
        {
            installationConeOrangeComplite = true;
            //Вызвать звук одобрения от инспектора  
            Debug.Log("Вы сделали фото улици, переходите к замерам");
        }
        else
        {
            //Вызвать звук неодобрения
            Debug.Log("Вы не выполнили предыдущие действия");
=======
            Debug.Log("Вы не выполнили предыдущий шаг");
>>>>>>> Map
        }
    }

}
