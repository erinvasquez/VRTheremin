using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThereminPlayer : MonoBehaviour {

    float playInput;
    Oscillator oscillator;

    [SerializeField]
    Vector2 lastMousePositionPixel;
    [SerializeField]
    Vector2 lastMousePositionPercent;

    public PitchClass minimumNote = new PitchClass(Notes.A, 0);
    public PitchClass maximumNote = new PitchClass(Notes.E, 2);

    float currentFrequency;
    float currentGain;

    RectTransform playArea;
    //bool insidePlayArea = false;

    private void Start() {
        oscillator = GetComponent<Oscillator>();
        //playArea = GameObject.Find("Play Area").GetComponent<RectTransform>();
    }

    private void FixedUpdate() {
        currentFrequency = GetFrequencyFromMouse();
        currentGain = GetVolumeFromMouse();
    }

    public void OnPressPlay(InputAction.CallbackContext context) {

        playInput = context.ReadValue<float>();

        if (RectTransformUtility.RectangleContainsScreenPoint(playArea, lastMousePositionPixel)) {

            switch (playInput) {
                case 1f:
                    oscillator.StartPlay(currentFrequency, currentGain);
                    break;
                case 0f:
                    oscillator.EndPlay();
                    break;
            }

        } else if (playInput == 0) {
            oscillator.EndPlay();
        }

    }

    public void OnAim(InputAction.CallbackContext context) {

        lastMousePositionPixel = context.ReadValue<Vector2>();
        lastMousePositionPercent = new Vector2(lastMousePositionPixel.x / Screen.width, lastMousePositionPixel.y / Screen.height);

        if (RectTransformUtility.RectangleContainsScreenPoint(playArea, lastMousePositionPixel)) {

            switch (playInput) {
                case 1f:
                    oscillator.StartPlay(currentFrequency, currentGain);
                    break;
                case 0f:
                    oscillator.EndPlay();
                    break;
            }

        } else if (playInput == 0) {
            oscillator.EndPlay();
        }

    }

    public PitchClass GetMinNote() {
        return minimumNote;
    }

    public void SetMinNote(PitchClass note) {
        minimumNote = note;
    }




    public PitchClass GetMaxNote() {
        return maximumNote;
    }

    public void SetMaxNote(PitchClass note) {
        maximumNote = note;
    }

    /// <summary>
    /// Maps our minimum and maximum frequencies to our screen's height,
    /// so our mouse returns a percentage of how high up the screen it is
    /// </summary>
    /// <returns></returns>
    public float GetFrequencyFromMouse() {


        Vector3[] corners = new Vector3[4];
        playArea.GetWorldCorners(corners);


        float maxY = corners[1].y;
        float minY = corners[0].y;

        float normalizedY = Remap(lastMousePositionPixel.y, minY, maxY, 0f, 1f);


        return Remap(normalizedY, 0f, 1f, minimumNote.frequency, maximumNote.frequency);
    }

    /// <summary>
    /// Maps our mouse position to a volume value we can use later
    /// </summary>
    /// <returns></returns>
    public float GetVolumeFromMouse() {

        Vector3[] corners = new Vector3[4];
        playArea.GetWorldCorners(corners);


        float maxX = corners[2].x;
        float minX = corners[0].x;

        float normalizedX = Remap(lastMousePositionPixel.x, minX, maxX, 0f, 1f);

        return normalizedX;
    }

    /// <summary>
    /// Used to remap between two ranges
    /// </summary>
    /// <param name="value"></param>
    /// <param name="from1"></param>
    /// <param name="to1"></param>
    /// <param name="from2"></param>
    /// <param name="to2"></param>
    /// <returns></returns>
    public float Remap(float value, float from1, float to1, float from2, float to2) {
        value = (value - from1) / (to1 - from1) * (to2 - from2) + from2;

        return value;
    }

}
