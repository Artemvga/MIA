using System.Collections.Generic;
using CarAI.Scripts.Constants;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{

    public List<AudioClip> somethingWrongClips = new List<AudioClip>();
    public AudioSource policeAudioSource;
    private Dictionary<string, AudioClip> _allClips = new();
    public SoundsManager soundsManager;
    public static Base instance { get; private set; }
    
    private  bool _radioComplete = false;
    private bool _makeRoullete = false;
    private bool _makeCone = false;
    private bool _inspectionDtpComplete = false;
    /*private bool _installationConeRedComplete = false;
    private bool _installationConeOrangeComplete = false;*/
    private bool _makePhotoQuestComplete = false;
    
    public TextMeshProUGUI _coneText;
    public TextMeshProUGUI _roulleteText;
    public TextMeshProUGUI _photoText;

    private void Start()
    {
        if (instance == null)
            instance = this;
        
        _allClips.Add("Camera", somethingWrongClips[0]);
        soundsManager.PlaySound(SoundsConstances.BACKGROUND_SOUND);
    }

    public void Radio()
    {
        if(!_radioComplete)
        {
            _radioComplete = true;
            //??????? ???? ????????? ?? ??????????
            Debug.Log("?? ???????? ? ????????");
        }
    }

    public void InspectionDTP()
    {
        if(!_inspectionDtpComplete && _radioComplete)
        {
            _inspectionDtpComplete = true;
            //??????? ???? ????????? ?? ??????????
            Debug.Log("?? ???????? ? ????????");
        }
        else
        {
            //??????? ???? ???????????
            Debug.Log("?? ?? ????????? ?????????? ????????");
        }
    }

    public void MakePhoto()
    {
        _makePhotoQuestComplete = true;
        _photoText.color = Color.green;
        Debug.Log("Pobeda");
    }
    
    public void RoulleteMake()
    {
        _makeRoullete = true;
        _roulleteText.color = Color.green;
        Debug.Log("Pobeda2");
    }
    
    public void ConeMake()
    {
        _makeCone = true;
        _coneText.color = Color.green;
        Debug.Log("Pobeda2");
    }

    public void PlayPoliceAudio(string audioName)
    {
        policeAudioSource.Stop();
        policeAudioSource.PlayOneShot(_allClips[audioName], 1);
    }

}
