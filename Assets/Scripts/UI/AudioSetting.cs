using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    public Slider slider;
    private AudioCtrl ctrl;
    void Start()
    {
        ctrl = AudioCtrl.Instance;
        slider.value = ctrl.audioSource.volume;
        slider.onValueChanged.AddListener(VolumeChanged);
    }

    private void VolumeChanged(float arg0)
    {
        if (ctrl != null)
        {
            ctrl.audioSource.volume = arg0;
        }
    }
}
