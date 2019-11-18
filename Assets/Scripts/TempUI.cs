using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempUI : MonoBehaviour
{

    [SerializeField] private Text _interactText;
    private int _text_show_count = 0;

    private static TempUI _instance;

    public static TempUI Instance
    {
        get
        {
            if(_instance == null){
                FindObjectOfType<TempUI>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("There's already an instance of TempUI: Singleton broken.");
            return;
        }
        _instance = this;
    }

    public void ShowInteractText()
    {
        _text_show_count++;
        _interactText.enabled = true;
    }

    public void HideInteractText()
    {
        _text_show_count--;
        if (_text_show_count == 0)
            _interactText.enabled = false;
    }

}
