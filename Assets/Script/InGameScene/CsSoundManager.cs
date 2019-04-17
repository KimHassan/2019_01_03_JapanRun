using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSoundManager : MonoBehaviour
{

    public static CsSoundManager instance;

    AudioSource myAudio;
    AudioClip attack;
    AudioClip damaged;
    AudioClip Bgm;
    AudioClip Click;
    AudioClip Wrong;
    // Start is called before the first frame update

    void Awake()
    {
        if (CsSoundManager.instance == null)
            CsSoundManager.instance = this;
     
       
       
    }

    void Start()
    {
        myAudio = gameObject.GetComponent<AudioSource>();
        attack = Resources.Load<AudioClip>("Sound/EffectSound/SwordSound1") as AudioClip;
        damaged = Resources.Load<AudioClip>("Sound/EffectSound/AttackSound1") as AudioClip;
        Wrong = Resources.Load<AudioClip>("Sound/EffectSound/Wrong") as AudioClip;

        Bgm = Resources.Load<AudioClip>("Sound/Bgm/Bgm1") as AudioClip;
        PlayBgm();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAttackSound()
    {
        
        myAudio.PlayOneShot(attack);
       
    }
    public void PlayDamagedSound()
    {
        myAudio.PlayOneShot(damaged);
    }
    public void PlayBgm()
    {
        myAudio.clip = Bgm;
        myAudio.Play();
    }
    public void PlayWrongSound()
    {
        myAudio.PlayOneShot(Wrong);
    }
    
}
