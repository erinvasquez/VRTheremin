using System.Collections;
using UnityEngine;

/// <summary>
/// Things to do:
/// - add, divide, multiply, average different waveforms together
/// - Add sliders/knobs to control multipliers to frequency/mix ratios
///   of different waveforms
/// - use rng to select new frequencies after certain periods of time
/// - make more sounds
/// </summary>
public class Oscillator : MonoBehaviour {

    /// <summary>
    /// In Hz, the current note being produced
    /// </summary>
    [SerializeField]
    double frequency = 440.0;

    /// <summary>
    /// Phase increment based on sampling frequency
    /// </summary>
    private double increment;

    /// <summary>
    /// Stays between 0 and 2 PI,
    /// phase for our sin wave
    /// 
    /// Used as a float value sometimes
    /// </summary>
    private double phase;

    /// <summary>
    /// Unity defaults to 48,000 
    /// </summary>
    private double sampling_frequency = 48000.0;

    /// <summary>
    /// How loud it is basically, but not really
    /// moves between -1 and 1?
    /// </summary>
    float gain;
    //float volume = 0.1f;

    [SerializeField]
    public Waveforms waveform = Waveforms.SinWave;
    Waveforms currentWaveform;

    public PitchClass currentPitch;

    //private float t = 0;

    private void Start() {

        // Get our frequency array calculated
        currentWaveform = waveform;
        currentPitch = new PitchClass(Notes.D, 4); // I like D4 as our default note, just cause
        frequency = currentPitch.GetETFrequency();

        //Debug.Log("Starting frequency: " + currentPitch.noteName.ToString() + currentPitch.octave + " " + currentPitch.frequency + "Hz");

    }

    private void OnValidate() {

        // Get our frequency array calculated
        currentWaveform = waveform;

    }

    /// <summary>
    /// Called when we click and hold,
    /// assures that we have the right frequency and gain(/volume?)
    /// </summary>
    /// <param name="freq"></param>
    /// <param name="g"></param>
    public void StartPlay(float freq, float g) {
        frequency = freq;
        gain = g;
    }

    /// <summary>
    /// Called when we've released our play button,
    /// sets our gain to 0 to "stop" our oscillator
    /// </summary>
    public void EndPlay() {
        gain = 0;
    }

    /// <summary>
    /// Set this oscillator's frequency
    /// </summary>
    /// <param name="f"></param>
    public void SetFrequency(float f) {
        frequency = f;
    }

    /// <summary>
    /// Return our oscillator's current frequency
    /// </summary>
    /// <returns></returns>
    public double GetFrequency() {
        return frequency;
    }


    /// <summary>
    /// Set our oscillator's gain
    /// </summary>
    /// <param name="g"></param>
    public void SetGain(float g) {
        gain = g;
    }

    /// <summary>
    /// Returns our osicllator's gain
    /// </summary>
    /// <returns></returns>
    public float GetGain() {
        return gain;
    }

    /// <summary>
    /// Get our gain in decibels
    /// </summary>
    /// <returns></returns>
    public float GetdB() {
        return 20f * Mathf.Log10((float) gain);
    }

    /// <summary>
    /// Set the waveform to be used by our oscillator
    /// </summary>
    /// <param name="form"></param>
    public void SetWaveform(int form) {
        SetWaveform((Waveforms) form);
    }

    /// <summary>
    /// Set the waveform to be used by our oscillator
    /// </summary>
    /// <param name="form"></param>
    public void SetWaveform(Waveforms form) {
        currentWaveform = form;
    }

    /// <summary>
    /// Get the waveform type this oscillator
    /// currently uses.
    /// </summary>
    /// <returns></returns>
    public Waveforms GetWaveform() {
        return currentWaveform;
    }

    /// <summary>
    /// Called on the audio thread, (not the main one)
    /// Unity uses this to "insert a custom filter
    /// into the audio DSP chain". I'm guessing that means
    /// we make audio happen.
    /// 
    /// "Called every time a chunk of audio is sent to the filter
    /// (~20ms or so depending on sample rate and platform), an array of floats
    /// from [-1f,1f]" "Can procedurally generate audio"
    /// </summary>
    /// <param name="data"></param>
    /// <param name="channels"></param>
    private void OnAudioFilterRead(float[] data, int channels) {

        // how much to increment the phase,
        // apparently just enough to move at the rate of our sampling frequency
        increment = frequency * 2 * Mathf.PI / sampling_frequency;

        for (int a = 0; a < data.Length; a += channels) {

            phase += increment;

            // Apply our frequency and phase to a certain waveform equation

            switch (currentWaveform) {
                case Waveforms.SinWave:
                    data[a] = GetSinWaveform();
                    break;

                case Waveforms.SquareWave:
                    data[a] = GetSquareWaveform();
                    break;

                case Waveforms.TriangleWave:
                    data[a] = GetTriangleWaveform();
                    break;
                case Waveforms.SawtoothWave:
                    data[a] = GetSawWaveform();
                    break;
                case Waveforms.HarshSawtoothWave:
                    data[a] = GetHarshSawWaveform();
                    break;
                default:
                    data[a] = 0;
                    break;
            }

            // if we have two channels...
            if (channels == 2) {
                data[a + 1] = data[a];
            }

            // Reset phase to 0 when it reaches 2 pi
            if (phase > (Mathf.PI * 2)) {
                phase = 0.0;
            }

        }

    }

    // Converts our frequency to angular velocity w (or omega)
    /// <summary>
    /// Get our frequency as an angular velocity
    /// </summary>
    /// <param name="freq"></param>
    /// <returns></returns>
    float AngularVelocity(float freq) {
        return freq * 2.0f * Mathf.PI;
    }

    /// <summary>
    /// Gets a sinewave
    /// </summary>
    /// <returns>A float</returns>
    float GetSinWaveform() {

        // phase is just set to 2 PI * frequency/sampling frequency

        return gain * Mathf.Sin((float) phase);
    }

    /// <summary>
    /// "Sounds like ann old nintendo"
    /// </summary>
    /// <returns></returns>
    float GetSquareWaveform() {

        if (gain * GetSinWaveform() >= 0 * gain) {
            return gain * 0.6f;
        } else {
            return -gain * 0.6f;
        }

    }

    /// <summary>
    /// "Squeaky and Quacky"
    /// Gets a triangle wave
    /// </summary>
    /// <returns></returns>
    float GetTriangleWaveform() {

        return gain * Mathf.PingPong((float) phase, 1.0f);

    }

    float GetSawWaveform() {
        // y = 2A/pi * (sigma n = 1 to s (-sin((n) * f * 2 * pi * x)) / n)
        // where S is the number of sin waves
        // A is amplitude
        // f is the frequency
        // OR

        double dOutput = 0.0;

        for (double n = 1.0; n < 100.0; n++) {
            dOutput += (GetSinWaveform() * frequency) / n;
        }

        return (float) dOutput * (2.0f / Mathf.PI);
    }

    float GetHarshSawWaveform() {
        // y= 2A/pi * (f * pi mod(1.0/f) - pi/2)

        return (2f / Mathf.PI) * ((float) frequency * (float) Mathf.PI * ((float) phase % (1f / (float) frequency) - ((float) Mathf.PI / 2f)));

    }

}
