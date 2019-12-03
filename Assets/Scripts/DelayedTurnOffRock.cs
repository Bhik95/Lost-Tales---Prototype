using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedTurnOffRock : MonoBehaviour
{
    public GameObject Collider;
    public GameObject ParticleEffect;
    public Animator Animator;
    public void TurnOff()
    {
        StartCoroutine(DelayedTurnOff());
    }

    IEnumerator DelayedTurnOff()
    {
        yield return new WaitForSeconds(.5f);

        Animator.enabled = true;
        Collider.SetActive(false);
        ParticleEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        //gameObject.SetActive(false);
    }
}
