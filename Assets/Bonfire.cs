using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : Interactible
{

    [SerializeField] private FMODUnity.StudioEventEmitter _interact_success;
    [SerializeField] private FMODUnity.StudioEventEmitter _interact_fail;

    [SerializeField] private GameObject _flame_fx_green;
    [SerializeField] private GameObject _flame_fx_yellow;
    [SerializeField] private GameObject _flame_fx_purple;

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
                    ParticleSystem ps = flamesDatas[i].GetComponent<ParticleSystem>();
                    ps.Stop();
                    Destroy(ps.gameObject, ps.main.duration);
                }
            }
        }
        else
        {
            _interact_fail.Play();
        }
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
