using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "A set of all pitches that a whole
/// number of octaves apart"
/// </summary>
[SerializeField]
public class PitchClass {

    /// <summary>
    /// Our note's letter name, ex.
    /// "DSharp" or "F"
    /// </summary>
    public Notes noteName;

    /// <summary>
    /// Current octave for this note
    /// </summary>
    public int octave;
    
    /// <summary>
    /// The frequency for this pitch at the current octave
    /// </summary>
    public float frequency;

    public PitchClass(Notes name, int octaveNumber) {
        noteName = name;
        octave = octaveNumber;


        // 7 note names
        // 0 to 8 octaves

        frequency = GetETFrequency();
    }

    public bool IsGreaterThan(PitchClass pitch) {

        // if our octave is greater, the frequency is greater
        if (octave > pitch.octave) {
            return true;
        } else if (octave == pitch.octave) {
            // else if the octave is the same,
            // we're only greater if our note name is

            if (noteName > pitch.noteName) {
                return true;
            }

        }

        // If our octave is less than our observed one, we're definitely not greater

        return false;
    }

    /// <summary>
    /// to calculate our Equal Temperament frequency,
    /// we need to ifnd how many half steps from A4
    /// we are
    /// </summary>
    /// <returns></returns>
    public HalfStepsFromA4 GetHalfStepsFromA4() {

        return (HalfStepsFromA4) System.Enum.Parse(typeof (HalfStepsFromA4), noteName.ToString().ToUpper() + octave.ToString());

    }

    /// <summary>
    /// Gets equal temprament frequency for this pitch
    /// with an octave of 0
    /// </summary>
    /// <param name="steps">Number of half steps away from A4</param>
    /// <returns></returns>
    public float GetETFrequency() {
        int aForForty = 440;

        float a = Mathf.Pow(2f, (1f / 12f));


        return (float) aForForty * Mathf.Pow(a, (float) GetHalfStepsFromA4());
    }

    /// <summary>
    /// Get the MIDI note number
    /// </summary>
    /// <returns></returns>
    public float GetP() {
        return 9 + (12 * Mathf.Log(frequency, 2f) / 440f);
    }

    public string GetName() {
        return noteName.ToString();
    }

    public override string ToString() {


        return noteName.ToString() + octave + " " + frequency + " Hz";
    }

}
