using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    Slider audioSlider;
    public AudioMixer master;

    public TMP_Text text;

    private void Start()
    {
        audioSlider = GetComponent<Slider>();
    }

    public void SetLevel(float value)
    {
        master.SetFloat("MasterVol", Mathf.Log10(value) * 26.5f);
    }

    private void Update()
    {
        float valueText = audioSlider.value * 100;
        text.text = "Maste volume: " + ((int)valueText).ToString();
    }
}