using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;
public class DistanceObject {

    public GameObject node1;
    public GameObject node2;
    public GameObject distanceText;

    public Text distanceTextComponent;

    public Anchor node1Anchor;
    public Anchor node2Anchor;
     
    public Anchor distanceTextAnchor;

    public LineRenderer node1Line;

    public int nodesCount = 0;

    public Vector3 getMiddlePoint()
    {
        return (node1.transform.position + node2.transform.position) / 2f;
    }

    public void Setup()
    {
        //distanceTextComponent = distanceText.GetComponentInChildren<Text>();
        node1Line = node1.GetComponent<LineRenderer>();
    }

    public void UpdateLine()
    {
        if(node1 == null || node2 == null || node1Line == null)
        {
            return;
        }

        node1Line.positionCount = 2;

        node1.transform.position = node1Anchor.transform.position;
        node2.transform.position = node2Anchor.transform.position;

        node1Line.SetPosition(0, node1Anchor.transform.position);
        node1Line.SetPosition(1, node2Anchor.transform.position);
        

        //distanceTextAnchor.transform.position = getMiddlePoint();
        Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);

        if (GetDistance() < 1f)
        {
            scale *= GetDistance();
        }

        node1.transform.localScale = scale;
        node2.transform.localScale = scale;
    }


    public float GetDistance()
    {
        if(node1Line == null)
        {
            return -1;
        }

        return Vector3.Distance(node1Anchor.transform.position, node2Anchor.transform.position);
    }
}
