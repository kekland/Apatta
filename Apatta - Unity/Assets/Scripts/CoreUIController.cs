using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreUIController : MonoBehaviour {

    /*
     * Main Canvas
    */

    //Canvas object itself
    [SerializeField]
    private Canvas MainCanvas;

    //Nodes that are on top of screen
    [SerializeField]
    private Image[] nodes;

    //Lines between nodes
    [SerializeField]
    private Image[] nodeLines;

    //FAB (Floating Action Button) used to add points
    [SerializeField]
    private Button addPointImage;

    //FAB to go to next wheel
    [SerializeField]
    private Button goNextImage;

    //FAB to undo previous changes
    [SerializeField]
    private Button undoImage;

    //Text that is on ActionBar and shows title
    [SerializeField]
    private Text title;

    /*
     * Scheme Canvas
    */

    //Scheme Canvas itself
    [SerializeField]
    private Canvas SchemeCanvas;

    //4 text that will show distances between wheel and ramp / line
    [SerializeField]
    private Text RearLeft;

    [SerializeField]
    private Text RearRight;

    [SerializeField]
    private Text TopRight;

    [SerializeField]
    private Text TopLeft;

    [SerializeField]
    private RectTransform WheelFrontLeft;

    [SerializeField]
    private RectTransform WheelFrontRight;

    [SerializeField]
    private RectTransform WheelRearRight;

    [SerializeField]
    private RectTransform WheelRearLeft;

    [SerializeField]
    private RectTransform carHolder;

    //Colors object
    private CoreColors colors = new CoreColors();

    /// <summary>
    /// Update UI - precisely, top bar
    /// </summary>
    /// <param name="finishedWheels">Amount of wheels that are finished</param>
    public void UpdateUserInterface(int finishedWheels)
    {
        // UI example :
        //               O - O - O - O
        // Node indexes: 0   1   2   3
        // Line indexes:   0   1   2

        // Iterate through finished wheels and paint nodes accordingly
        for (int i = 0; i < finishedWheels; i++)
        {
            nodes[i].color = colors.uiNodeColors[0];
        }

        // Iterate through unfinished wheels and paint nodes accordingly
        for (int i = finishedWheels; i < 6; i++)
        {
            nodes[i].color = colors.uiNodeColors[1];
        }

        // Iterate through wheels and paint lines accordingly
        for (int i = 0; i < 5; i++)
        {
            if(i < finishedWheels - 1)
            {
                nodeLines[i].color = colors.uiLineColors[0];
            }
            else
            {
                nodeLines[i].color = colors.uiLineColors[1];
            }
        }

        //Iterate through finished wheels to get title
        string titleString = "";
        switch(finishedWheels)
        {
            case 0: titleString = "Rear Left wheel"; break;
            case 1: titleString = "Rear Right wheel"; break;
            case 2: titleString = "Front Right wheel"; break;
            case 3: titleString = "Front Left wheel"; break;
            case 4: titleString = "Line"; break;
            case 5: titleString = "Border"; break;
            case 6: titleString = "Finished"; break;
        }

        //Set title
        title.text = titleString;


        //Update buttons state
        if (finishedWheels == 6)
        {
            //Everything is finished and waiting for user confirmation
            SetNextButtonState(true);
        }
        else if (finishedWheels < 6)
        {
            //Start next wheel
            SetNextButtonState(false);
        }
    }

    /// <summary>
    /// Set state of GoNext button
    /// </summary>
    /// <param name="state">State - True if interactable, False otherwise</param>
    public void SetNextButtonState(bool state)
    {
        //Set interactability
        goNextImage.interactable = state;
    }

    /// <summary>
    /// Set state of Undo button
    /// </summary>
    /// <param name="state">State - True if interactable, False otherwise</param>
    public void SetUndoButtonState(bool state)
    {
        undoImage.interactable = state;
    }

    /// <summary>
    /// Function that sets distance text in SchemeCanvas
    /// </summary>
    /// <param name="scheme">Scheme that is used currently</param>
    public void SetScheme(Scheme scheme)
    {
        RearLeft.text = DistanceToString(scheme.distanceRearLeft);
        RearRight.text = DistanceToString(scheme.distanceRearRight);
        TopRight.text = DistanceToString(scheme.distanceFrontRight);
        TopLeft.text = DistanceToString(scheme.distanceFrontLeft);

        float rearRotationMid = scheme.rearRightWheelRotation + scheme.rearLeftWheelRotation;
        rearRotationMid /= 2f;

        WheelRearLeft.rotation = Quaternion.Euler(0, 0, 0);
        WheelRearRight.rotation = Quaternion.Euler(0, 0, 0);
        WheelFrontRight.rotation = Quaternion.Euler(0, 0, scheme.frontRightWheelRotation - rearRotationMid);
        WheelFrontLeft.rotation = Quaternion.Euler(0, 0, scheme.frontLeftWheelRotation - rearRotationMid);
    }

    string DistanceToString(float distance)
    {
        if (distance > 1f)
        {
            return Math.Round(distance, 2).ToString() + "m";
        }
        else
        {
            return Math.Round(distance * 100f, 2).ToString() + "cm";
        }
    }

    /// <summary>
    /// Function that switches current canvas to scheme canvas
    /// </summary>
    public void SwitchToSchemeCanvas()
    {
        SchemeCanvas.GetComponent<Animation>().Play("SchemeCanvasSlideIn");
    }

    /// <summary>
    /// Function that switches current canvas to main canvas
    /// </summary>
    public void SwitchToMainCanvas()
    {
        SchemeCanvas.GetComponent<Animation>().Play("SchemeCanvasSlideOut");
    }

    
}
