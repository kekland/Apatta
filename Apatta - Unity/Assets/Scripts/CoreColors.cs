using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreColors {

    //Plane color and iterator -
    //They are used when drawing plane
    int planeColorIterator = 0;
    public Color[] planeColors = new Color[] {
                new Color(1.0f, 1.0f, 1.0f),
                new Color(0.956f, 0.262f, 0.211f),
                new Color(0.913f, 0.117f, 0.388f),
                new Color(0.611f, 0.152f, 0.654f),
                new Color(0.403f, 0.227f, 0.717f),
                new Color(0.247f, 0.317f, 0.709f),
                new Color(0.129f, 0.588f, 0.952f),
                new Color(0.011f, 0.662f, 0.956f),
                new Color(0f, 0.737f, 0.831f),
                new Color(0f, 0.588f, 0.533f),
                new Color(0.298f, 0.686f, 0.313f),
                new Color(0.545f, 0.764f, 0.290f),
                new Color(0.803f, 0.862f, 0.223f),
                new Color(1.0f, 0.921f, 0.231f),
                new Color(1.0f, 0.756f, 0.027f)
            };

    //UI colors : 0 is deactivated, 1 is activated
    public Color[] uiNodeColors = new Color[]
    {
            new Color(79f / 255f, 115f / 255f, 188f / 255),
            new Color(173f / 255f, 196f / 255f, 242f / 255f)
    };

    public Color[] uiLineColors = new Color[]
    {
            new Color(108f / 255f, 138f / 255f, 199f / 255f),
            new Color(219f / 255f, 225f / 255f, 238f / 255f)
    };

    public Color GetPlaneColorNext()
    {
        //Get color from array
        Color color = planeColors[planeColorIterator];

        //Increment iterator, and if it is more than maximum value - revert back to zero
        planeColorIterator++;
        if(planeColorIterator == planeColors.Length)
        {
            planeColorIterator = 0;
        }

        return color;
    }
}
