using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Keystone : MonoBehaviour
{
    bool activated;
    [SerializeField] List<GameObject> ObjectsToDeactivate;
    [SerializeField] List<GameObject> ObjectsToActivate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
        {
            return;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Lantern"))
        {
            activated = true;
            ObjectsToDeactivate.ForEach(a => a.SetActive(false));
            ObjectsToActivate.ForEach(a => a.SetActive(true));
        }
    }
}
