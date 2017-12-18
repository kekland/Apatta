


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;
using GoogleARCore;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class CoreController : MonoBehaviour
{
    
    //A first persion user camera
    [SerializeField]
    private Camera firstPersonCamera;

    //Various prefabs
    [SerializeField]
    private GameObject planePrefab;

    [SerializeField]
    private GameObject distanceFirstNodePrefab;

    [SerializeField]
    private GameObject distanceSecondNodePrefab;

    [SerializeField]
    private GameObject distanceTextPrefab;

    [SerializeField]
    private GameObject wheelPrefab;
    
    //Controllers
    [SerializeField]
    private CoreUIController uiController;

    //Wheel components - used to draw wheels
    private int currentWheelIndex = 0;
    private NodeObject[] wheels = new NodeObject[6];

    //Color object
    private CoreColors colors = new CoreColors();


    void Awake()
    {
        ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;
        ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
        ApplicationChrome.statusBarColor = 0xFFE66B00;
        Screen.fullScreen = false;
    }
    void Start()
    {
        //Initialize our GameObject to hold Wheel components
        GameObject wheelHolderTemp = new GameObject("Wheel collection");

        //Add wheel components to our temporary GameObject and fill our Wheels array
        for (int i = 0; i < 6; i++)
        {
            wheels[i] = wheelHolderTemp.AddComponent<NodeObject>();
            wheels[i].firstNodePrefab = distanceFirstNodePrefab;
            wheels[i].secondNodePrefab = distanceSecondNodePrefab;
            wheels[i].distanceTextPrefab = distanceTextPrefab;
            wheels[i].wheelPrefab = wheelPrefab;
        }

        //Set wheels location
        wheels[0].location = NodesPosition.RearLeft;
        wheels[1].location = NodesPosition.RearRight;
        wheels[2].location = NodesPosition.FrontRight;
        wheels[3].location = NodesPosition.FrontLeft;
        wheels[4].location = NodesPosition.Border;
        wheels[5].location = NodesPosition.Line;

        uiController.UpdateUserInterface(currentWheelIndex);
    }

    void Update()
    {
        //Update ARCore planes
        UpdatePlanes();
        
        //Check if user clicked
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !IsPointerOverUIObject() && currentWheelIndex < 6)
        {
            Touch touch = Input.GetTouch(0);

            //Send ray to check if we hit a plane
            TrackableHit hit;
            TrackableHitFlag flag = TrackableHitFlag.PlaneWithinBounds | TrackableHitFlag.PlaneWithinPolygon;

            if (Session.Raycast(firstPersonCamera.ScreenPointToRay(touch.position), flag, out hit))
            {
                //We hit a plane - add a wheel node
                //If this was a last node in this wheel - make Next button available
                if(wheels[currentWheelIndex].AddNode(hit.Point))
                {
                    uiController.SetNextButtonState(true);
                }
                uiController.SetUndoButtonState(true);
            }
        }

        //Update wheels
        foreach(NodeObject wheel in wheels)
        {
            wheel.UpdateLines();
        }

        Debug.Log(currentWheelIndex);
    }

    //These functions recieve events from UI
    #region UIEvents
    /// <summary>
    /// This function gets called if NextButton is clicked
    /// </summary>

    Scheme currentScheme;
    SchemeConstructor constructorOfScheme;
    public void NextButtonClickedEvent()
    {
        //Increment the index of current wheel
        currentWheelIndex++;

        if(currentWheelIndex > 6)
        {
            //Finished, reset index to 6 to avoid some problems
            currentWheelIndex = 6;
            
            //Switch canvases and set text
            uiController.SwitchToSchemeCanvas();

            constructorOfScheme = new SchemeConstructor();

            for(int i = 0; i < 6; i++)
            {
                Vector2 firstPosition = new Vector2(wheels[i].distance.node1Anchor.transform.position.x, 
                    wheels[i].distance.node1Anchor.transform.position.z);

                Vector2 secondPosition = new Vector2(wheels[i].distance.node2Anchor.transform.position.x,
                    wheels[i].distance.node2Anchor.transform.position.z);

                constructorOfScheme.SetEquation(i, firstPosition, secondPosition);

            }

            currentScheme = constructorOfScheme.Calculate();

            uiController.SetScheme(currentScheme);
            return;
        }

        //Update UI
        uiController.UpdateUserInterface(currentWheelIndex);
    }


    /// <summary>
    /// This function gets called if UndoButton is clicked
    /// </summary>
    public void UndoButtonClickedEvent()
    {
        //If everything is ready and user cancelled - set that user is on last step
        if(currentWheelIndex >= 6)
        {
            currentWheelIndex = 5;
        }

        //If we were not able to undo this current wheel - step backwards and undo other wheel
        if(!wheels[currentWheelIndex].Undo())
        {
            currentWheelIndex--;
            wheels[currentWheelIndex].Undo();
        }

        if(wheels[currentWheelIndex].distance.nodesCount == 0 && currentWheelIndex == 0)
        {
            uiController.SetUndoButtonState(false);
        }

        uiController.UpdateUserInterface(currentWheelIndex);
    }
    #endregion

    //These functions are essential for ARCore to work and they can be left untouched
    #region ARCore

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    private List<TrackedPlane> newPlanes = new List<TrackedPlane>();
    private List<TrackedPlane> allPlanes = new List<TrackedPlane>();
    void UpdatePlanes()
    {
        //Check errors and quit if errors are detected
        QuitOnConnectionErrors();
        if (Frame.TrackingState != FrameTrackingState.Tracking)
        {
            const int timeout = 15;
            Screen.sleepTimeout = timeout;
            return;
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        //Recieve planes and do colors / calculation stuff
        Frame.GetNewPlanes(ref newPlanes);

        for (int i = 0; i < newPlanes.Count; i++)
        {
            GameObject planeObject = Instantiate(planePrefab, Vector3.zero, Quaternion.identity, transform);

            planeObject.GetComponent<TrackedPlaneVisualizer>().SetTrackedPlane(newPlanes[i]);

            Debug.Log("Plane detected : " + i.ToString());
            planeObject.GetComponent<Renderer>().material.SetColor("_GridColor", colors.GetPlaneColorNext());
            planeObject.GetComponent<Renderer>().material.SetFloat("_UvRotation", Random.Range(0.0f, 360.0f));
        }

        Frame.GetAllPlanes(ref allPlanes);
    }

    private void QuitOnConnectionErrors()
    {
        // Do not update if ARCore is not tracking.
        if (Session.ConnectionState == SessionConnectionState.DeviceNotSupported)
        {
            ShowAndroidToastMessage("This device does not support ARCore.");
            Application.Quit();
        }
        else if (Session.ConnectionState == SessionConnectionState.UserRejectedNeededPermission)
        {
            ShowAndroidToastMessage("Camera permission is needed to run this application.");
            Application.Quit();
        }
        else if (Session.ConnectionState == SessionConnectionState.ConnectToServiceFailed)
        {
            ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
            Application.Quit();
        }
    }

    private static void ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                    message, 0);
                toastObject.Call("show");
            }));
        }
    }
    #endregion
}
