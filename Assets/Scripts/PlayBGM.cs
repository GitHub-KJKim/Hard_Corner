using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGMSound(0.5f);

        SoundManager.Instance.PlayIDLESound("Operate_Vacuum", 0.25f);
    }

    public void ResumeIDLESound()
    {
        SoundManager.Instance.PlayIDLESound("Operate_Vacuum", 0.25f);
    }
}
