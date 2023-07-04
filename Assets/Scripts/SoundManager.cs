using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// 사운드 매니져
/// 
/// </summary>

public class SoundManager : MonoBehaviour
{
    // Sound를 관리해주는 스크립트는 하나만 존재해야하고
    // instance프로퍼티로 언제 어디에서나 불러오기위해 싱글톤 사용
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    }

    private AudioSource bgmPlayer;                              // BGM
    private AudioSource sfxPlayer;                              // SFX
    private AudioSource idlePlayer;

    public float masterVolumnBGM = 1f;
    public float masterVolumnSFX = 1f;

    [SerializeField] private AudioClip titleBGMAudioClip;       // 타이틀 BGM
    [SerializeField] private AudioClip inGameBGMAudioClip;      // 인게임 BGM
    [SerializeField] private AudioClip[] sfxAudioClips;         // 효과음들

    // 효과음 딕셔너리
    // AudioClip을 Key,Value 형태로 관리하기 위해 딕셔너리 사용
    Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        bgmPlayer = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>();
        sfxPlayer = GameObject.Find("SFXSoundPlayer").GetComponent<AudioSource>();
        idlePlayer = GameObject.Find("IDLESoundPlayer").GetComponent<AudioSource>();

        foreach (AudioClip audioClip in sfxAudioClips)
        {
            audioClipsDic.Add(audioClip.name, audioClip);
        }
    }

    // 효과음 재생
    public void PlaySFXSound(string name, float volumn = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }

        if (sfxPlayer.isPlaying)
        {
            return;
        }
        else
        {
            sfxPlayer.PlayOneShot(audioClipsDic[name], volumn * masterVolumnSFX);
        }
    }

    public void PlayIDLESound(string name, float volumn = 1f)
    {
        if (SceneManager.GetActiveScene().name != "0_Title")
        {
            idlePlayer.loop = true;
            idlePlayer.volume = volumn * masterVolumnSFX;

            if (audioClipsDic.ContainsKey(name) == false)
            {
                Debug.Log(name + " is not Contained audioClipsDic");
                return;
            }

            if (idlePlayer.isPlaying)
            {
                return;
            }
            else
            {
                idlePlayer.clip = audioClipsDic[name];
                idlePlayer.Play();
            }
        }
    }

    // BGM 재생
    public void PlayBGMSound(float volumn = 1f)
    {
        bgmPlayer.loop = true;
        bgmPlayer.volume = volumn * masterVolumnBGM;

        //현재 씬에 맞는 BGM 재생
        if (SceneManager.GetActiveScene().name == "0_Title")
        {
            bgmPlayer.clip = titleBGMAudioClip;
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.clip = inGameBGMAudioClip;
            bgmPlayer.Play();
        }
    }

    public void PauseBGMSound()
    {
        bgmPlayer.Pause();
    }

    public void UnPauseBGMSound()
    {
        bgmPlayer.UnPause();
    }

    public void PauseIDLESound()
    {
        idlePlayer.Stop();
    }

    public void StopSFXSound()
    {
        sfxPlayer.Stop();
    }

    //public void RechargedComplete()
    //{
    //    StartCoroutine(WaitforSinglePlay());
    //}

    //private IEnumerator WaitforSinglePlay()
    //{
    //    while (true)
    //    {
    //        yield return null;

    //        if (IsCharged == false) yield break;

    //        if (batteryGauge >= 100.0f) break;
    //    }

    //    SoundManager.Instance.PlaySFXSound("Recharged_Complete", 1);
    //}
}


