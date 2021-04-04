using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Considering we want to handle our input differently for our left and right hands,
/// left being tied to volume input,
/// right being tied to frequency input
/// 
/// </summary>
public class ThereminVRControllerHandler : MonoBehaviour {

    [SerializeField]
    private ActionBasedController leftController;
    [SerializeField]
    private ActionBasedController rightController;



    float playInput;
    Oscillator oscillator;

    PitchClass minNote = new PitchClass(Notes.A, 0);
    PitchClass maxNote = new PitchClass(Notes.E, 2);

    float currentFrequency;
    float currentGain;

    [SerializeField] Vector3 lastLeftPos;
    [SerializeField] Vector3 lastRightPos;

    /// <summary>
    /// The position the user sets as a "low position", ideally
    /// where the upper arm is relaxed pointing downward, with the lower arm following suit,
    /// as to provide a calibration for our instrument
    /// </summary>
    public float lowPosition;
    /// <summary>
    /// The position the user sets as a "high position", ideally
    /// where the upper arm is relaxed pointing downward, with the lower arm pointed upward,
    /// as to provide a calibration for our instrument
    /// </summary>
    public float highPosition;

    private void Start() {

        // Set these in the inspector for now
        //leftController = GetComponent<ActionBasedController>();

        oscillator = GetComponent<Oscillator>();
        currentFrequency = minNote.frequency;
        currentGain = 0.5f;


    }

    private void FixedUpdate() {
        currentFrequency = GetFrequencyFromRightController();
        currentGain = GetVolumeFromLeftController();

        switch (playInput) {
            case 1f:
                oscillator.StartPlay(currentFrequency, currentGain);
                break;
            case 0f:
                oscillator.EndPlay();
                break;
            default:
                //oscillator.StartPlay(currentFrequency, currentGain);
                break;
        }

    }

    /// <summary>
    /// When we press our "play instrument" button
    /// </summary>
    /// <param name="obj"></param>
    public void OnPressPlay(InputAction.CallbackContext obj) {
        playInput = obj.ReadValue<float>();
    }

    /// <summary>
    /// Uses the Left Grip button
    /// </summary>
    /// <param name="obj"></param>
    public void SetLowPosition() {
        lowPosition = lastLeftPos.y;

    }

    /// <summary>
    /// Uses the Right Grip button
    /// </summary>
    /// <param name="obj"></param>
    public void SetHighPosition() {
        highPosition = lastRightPos.y;
    }
    
    /// <summary>
    /// Remaps our controller's position into a range of [0?] to 1
    /// </summary>
    /// <returns></returns>
    public float GetVolumeFromLeftController() {
        return Remap(lastLeftPos.y, lowPosition, highPosition, 0f, 1f);
    }

    /// <summary>
    /// Normalizes our controller position into a range of 0 to 1 (should we do -1 to 1?)
    /// </summary>
    /// <returns></returns>
    public float GetFrequencyFromRightController() {
        return Remap(lastRightPos.y, 0f, 1f, minNote.frequency, maxNote.frequency);
    }

    /// <summary>
    /// Input callback to set our last known position of our left controller
    /// </summary>
    /// <param name="context"></param>
    public void SetLeftControllerPosition(InputAction.CallbackContext context) {
        lastLeftPos = context.ReadValue<Vector3>();
    }

    /// <summary>
    /// Input callback to set the last known position of our right controller
    /// </summary>
    /// <param name="context"></param>
    public void SetRightControllerPosition(InputAction.CallbackContext context) {

        lastRightPos = context.ReadValue<Vector3>();

    }

    /// <summary>
    /// Remaps a value from one range to the other
    /// </summary>
    /// <param name="value">The value we're re-mapping</param>
    /// <param name="from1">Original lower range value</param>
    /// <param name="to1">Original higher range value</param>
    /// <param name="from2">New lower range value</param>
    /// <param name="to2">New higher range value</param>
    /// <returns></returns>
    public float Remap(float value, float from1, float to1, float from2, float to2) {
        value = (value - from1) / (to1 - from1) * (to2 - from2) + from2;

        return value;
    }

}
