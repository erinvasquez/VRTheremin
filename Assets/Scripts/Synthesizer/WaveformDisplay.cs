using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveformDisplay : MonoBehaviour {
    TMP_Text text;
    Oscillator oscillator;

    private void Start() {

        text = GetComponent<TMP_Text>();
        oscillator = GameObject.Find("Theremin").GetComponent<Oscillator>();

    }

    private void Update() {

        text.text = "Wavform type: " + oscillator.GetWaveform();

    }

}
