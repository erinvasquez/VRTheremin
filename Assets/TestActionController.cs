using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class TestActionController : MonoBehaviour {

    private ActionBasedController controller;
    
    
    
    float playInput;
    Oscillator oscillator;

    PitchClass minNote = new PitchClass(Notes.A, 0);
    PitchClass maxNote = new PitchClass(Notes.E, 2);

    float currentFrequency;
    float currentGain;

    [SerializeField]
    Vector3 lastControllerPosition;

    private void Start() {

        controller = GetComponent<ActionBasedController>();

        oscillator = GameObject.Find("Theremin").GetComponent<Oscillator>();
        currentFrequency = minNote.frequency;
        currentGain = 0.5f;

        //controller.selectAction.action.performed += MyAction;


    }

    private void FixedUpdate() {
        currentFrequency = GetFrequencyFromController();

        switch (playInput) {
            case 1f:
                oscillator.StartPlay(currentFrequency, currentGain);
                break;
            case 0f:
                oscillator.EndPlay();
                break;
            default:
                oscillator.StartPlay(currentFrequency, currentGain);
                break;
        }

    }


    public void OnPressPlay(InputAction.CallbackContext obj) {
        playInput = obj.ReadValue<float>();
    }

    /// <summary>
    /// Normalizes our controller position into a range of 0 to 1 (should we do -1 to 1?)
    /// </summary>
    /// <returns></returns>
    public float GetFrequencyFromController() {
        return Remap(lastControllerPosition.y, 0f, 1f, minNote.frequency, maxNote.frequency);
    }

    /// <summary>
    /// Input callback to set the last known position of our controller
    /// </summary>
    /// <param name="context"></param>
    public void SetControllerPosition(InputAction.CallbackContext context) {

        lastControllerPosition = context.ReadValue<Vector3>();

        
        

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
