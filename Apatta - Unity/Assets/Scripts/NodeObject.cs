using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GoogleARCore;
public class NodeObject : MonoBehaviour {
    //Distance object of this wheel
    public DistanceObject distance;

    //Location of this wheel
    [SerializeField]
    public NodesPosition location;

    //Prefabs
    [SerializeField]
    public GameObject firstNodePrefab;
    [SerializeField]
    public GameObject secondNodePrefab;
    [SerializeField]
    public GameObject distanceTextPrefab;

    [SerializeField]
    public GameObject wheelPrefab;

    Anchor currentWheelAnchor;
    GameObject currentWheel;
    void Start()
    {
        //Set distance
        distance = new DistanceObject();
    }

    /// <summary>
    /// Function to add nodes
    /// </summary>
    /// <param name="position">Position of node in 3D space</param>
    /// <returns>Returns true if all wheel nodes are set, otherwise returns false</returns>
    public bool AddNode(Vector3 position)
    { 
        //Create anchor of this point
        Anchor nodeAnchor = Session.CreateAnchor(position, Quaternion.identity);
        GameObject nodeToAdd;

        //If this wheel is new and no nodes were added
        if (distance.nodesCount == 0)
        {
            //Instantiate our first node and apply anchor to it
            nodeToAdd = Instantiate(firstNodePrefab, position, Quaternion.identity, nodeAnchor.transform);
            distance.node1 = nodeToAdd;
            distance.node1Anchor = nodeAnchor;

            //Set nodes count to 1
            distance.nodesCount = 1;
        } 
        //Otherwise, if we have set first node
        else if(distance.nodesCount == 1)
        {
            //Instantiate second node and apply anchor
            nodeToAdd = Instantiate(secondNodePrefab, position, Quaternion.identity, nodeAnchor.transform);
            distance.node2 = nodeToAdd;
            distance.node2Anchor = nodeAnchor;

            //Instaniate distance text and anchor
            //Anchor distanceAnchor = Session.CreateAnchor(distance.getMiddlePoint(), Quaternion.identity);
            //distance.distanceText = Instantiate(distanceTextPrefab, distance.getMiddlePoint(), Quaternion.identity, distanceAnchor.transform);
            //distance.distanceTextAnchor = distanceAnchor;

            //Set nodes count to 2
            distance.nodesCount = 2;

            //Update distance object
            distance.Setup();
            distance.UpdateLine();

            //Instantiate wheel and set positions
            //Find angle using tangent function
            //float wheelRotationY = Mathf.Atan((distance.node2Anchor.transform.position.y - distance.node1Anchor.transform.position.y) /
            //                                  (distance.node2Anchor.transform.position.x - distance.node1Anchor.transform.position.x)) * Mathf.Rad2Deg;

            //Get cylinder radius
            //float radius = distance.GetDistance() / 2f;

            //Build position based on radius and midpoint
            //Vector3 wheelPosition = distance.getMiddlePoint();
            //wheelPosition.y = radius;

            //Build Quaternion degrees
            //Quaternion wheelRotation = Quaternion.Euler(90, wheelRotationY, 0);

            //Anchor wheelAnchor = Session.CreateAnchor(wheelPosition, wheelRotation);

            //Instantiate wheel and assign it to variable
            //currentWheel = Instantiate(wheelPrefab, wheelPosition, wheelRotation, wheelAnchor.transform);

            //Set scale
            //currentWheel.transform.localScale = new Vector3(radius * 2f, radius * 2f * 0.2f, radius * 2f);
        }

        //If all nodes are set - return true, otherwise return false
        if(distance.nodesCount == 2)
        {
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// Undo latest movement in this wheel
    /// <returns>Returns True if it did destroy something - False otherwise</returns>
    /// </summary>
    public bool Undo()
    {
        //If second node is set - destroy it
        if(distance.node2 != null)
        {
            Destroy(distance.node2);
            Destroy(distance.node2Anchor);
            Destroy(distance.distanceText);
            Destroy(distance.distanceTextAnchor);

            //Destroy(currentWheel);
            //Destroy(currentWheelAnchor);

            distance.nodesCount = 1;
            distance.node1Line.SetPosition(1, distance.node1Line.GetPosition(0));
            distance.node1Line.positionCount = 1;
            return true;
        } 
        //Otherwise - destroy first node
        else if(distance.node1 != null)
        {
            Destroy(distance.node1);
            Destroy(distance.node1Anchor);
            distance.nodesCount = 0;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Function that updates Distance and location of objects
    /// </summary>
    /// <param name="cameraLocation">Camera object</param>
    public void UpdateLines()
    {
        distance.UpdateLine();
    }
}

public enum NodesPosition
{
    RearLeft = 0,
    RearRight = 1,
    FrontRight = 2,
    FrontLeft = 3,
    Line = 4,
    Border = 5
}
