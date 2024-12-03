using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager _instance;

    [SerializeField] AudioSource audioSourceRadio;
    [SerializeField] AudioSource audioSourceSFX;
    [SerializeField] Audios[] audios;
    [SerializeField] Radio[] BGM;
    [SerializeField] int currClip;
    public int CurrentRadio;
    bool FirstTime = true;

    private void Awake()
    {
        _instance = this;
        foreach (var audio in BGM)
        {
            audio.audioClip.LoadAudioData();
        }
    }

    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public void PlaySoundSFX(string soundName)
    {
        foreach (var audio in audios)
        {
            if(audio.AudioName == soundName)
            {
                audioSourceSFX.PlayOneShot(audio.audioClip);
            }
        }
    }

    private void Start()
    {
        audioSourceRadio = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if (!audioSourceRadio.isPlaying)
        {
            Debug.Log("Ganti Lagu");
            currClip++;
            List<Radio> prad = new List<Radio>();
            foreach (Radio audio in BGM)
            {
                if (audio.RadioFreq == CurrentRadio)
                    prad.Add(audio);
            }

            if (FirstTime)
            {
                currClip = UnityEngine.Random.Range(0, prad.Count - 1);
                FirstTime = false;
            }

            if (currClip > prad.Count - 1) currClip = 0;
            audioSourceRadio.clip = prad[currClip].audioClip;
            audioSourceRadio.Play();
        }
    }

    public void GantiChannel(int ch)
    {
        CurrentRadio = ch;
        for (int i = 0; i < BGM.Length - 1; i++)
        {
            if (audioSourceRadio.clip == BGM[i].audioClip)
            {
                if (BGM[i].RadioFreq == ch) return;

                List<Radio> prad = new List<Radio>();
                foreach (Radio audio in BGM)
                {
                    if (audio.RadioFreq == CurrentRadio)
                        prad.Add(audio);
                }

                if (FirstTime)
                {
                    currClip = UnityEngine.Random.Range(0, prad.Count - 1);
                    FirstTime = false;
                }

                if (currClip > prad.Count - 1) currClip = 0;
                audioSourceRadio.clip = prad[currClip].audioClip;
                audioSourceRadio.Play();
            }
        }
    }
}


[System.Serializable]
class Audios
{
    public string AudioName;
    public AudioClip audioClip;
}

[System.Serializable]
class Radio
{
    public string AudioName;
    public AudioClip audioClip;
    public int RadioFreq;
}