using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnEnable : MonoBehaviour
{

    [SerializeField] private string _scene_name;

    private void OnEnable()
    {
        SceneManager.LoadScene(_scene_name);
    }
}
