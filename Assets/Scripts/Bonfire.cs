﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bonfire : Interactible
{

    [SerializeField] private FMODUnity.StudioEventEmitter _interact_success;
    [SerializeField] private FMODUnity.StudioEventEmitter _interact_fail;
    [SerializeField] private FMODUnity.StudioEventEmitter _three_flames_sound;

    [SerializeField] private GameObject _flame_fx_green;
    [SerializeField] private GameObject _flame_fx_yellow;
    [SerializeField] private GameObject _flame_fx_purple;
    [SerializeField] private GameObject _flame_fx_final;

    [SerializeField] private Animator _paint_animator;
    [SerializeField] private Animator _music_animator;

    [SerializeField] private BlackScreen _black_screen;
    [SerializeField] private string _final_scene_name = "FinalScene";

    private int _n_flames = 0;

    public event Action OnThreeFlamesCollected;

    protected override void OnActivate()
    {
        if (PlayerStatus.Instance.HasBigFlame)
        {
            _interact_success.Play();
            var flamesDatas = PlayerStatus.Instance.gameObject.GetComponentsInChildren<SacredFlameData>();

            if(flamesDatas != null)
            {
                for(int i = 0; i < flamesDatas.Length; i++)
                {
                    switch (flamesDatas[i].FlameType)
                    {
                        case FlameType.GREEN:
                            _flame_fx_green.SetActive(true);
                            break;
                        case FlameType.YELLOW:
                            _flame_fx_yellow.SetActive(true);
                            break;
                        case FlameType.PURPLE:
                            _flame_fx_purple.SetActive(true);
                            break;

                    }
                    _n_flames++;
                    Camera.main.GetComponent<CameraShaker>().AddTrauma(.25f);
                    ParticleSystem ps = flamesDatas[i].GetComponent<ParticleSystem>();
                    ps.Stop();
                    Destroy(ps.gameObject, ps.main.duration);

                    if (_n_flames == 3)
                    {
                        OnThreeFlamesCollected?.Invoke();
                        _flame_fx_final.SetActive(true);
                        _three_flames_sound.Play();

                        StartCoroutine(TransitionToFinalScene(3f, 3f));
                    }
                }
            }

            PlayerStatus.Instance.HasBigFlame = false;
        }
        else
        {
            _interact_fail.Play();
        }

        _paint_animator.SetInteger("FlamesLit", _n_flames);
        if(_music_animator)
            _music_animator.SetInteger("FlamesLit", _n_flames);
    }

    private IEnumerator TransitionToFinalScene(float delayBlackScr, float delaySceneLoading)
    {
        float timer = delayBlackScr;
        while(timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        _black_screen.SetVisible(true);
        timer = delaySceneLoading;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(_final_scene_name);
    }

    protected override void OnPlayerFar()
    {
        //throw new System.NotImplementedException();
    }

    protected override void OnPlayerNearby()
    {
        //throw new System.NotImplementedException();
    }
}
