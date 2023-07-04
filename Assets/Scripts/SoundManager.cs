using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// ���� �Ŵ���
/// 
/// </summary>

public class SoundManager : MonoBehaviour
{
    // Sound�� �������ִ� ��ũ��Ʈ�� �ϳ��� �����ؾ��ϰ�
    // instance������Ƽ�� ���� ��𿡼��� �ҷ��������� �̱��� ���
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

    [SerializeField] private AudioClip titleBGMAudioClip;       // Ÿ��Ʋ BGM
    [SerializeField] private AudioClip inGameBGMAudioClip;      // �ΰ��� BGM
    [SerializeField] private AudioClip[] sfxAudioClips;         // ȿ������

    // ȿ���� ��ųʸ�
    // AudioClip�� Key,Value ���·� �����ϱ� ���� ��ųʸ� ���
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

    // ȿ���� ���
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

    // BGM ���
    public void PlayBGMSound(float volumn = 1f)
    {
        bgmPlayer.loop = true;
        bgmPlayer.volume = volumn * masterVolumnBGM;

        //���� ���� �´� BGM ���
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


