using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasWelcomeController : MonoBehaviour {

    [SerializeField]
    private Canvas WelcomeCanvas;

    [SerializeField]
    private Canvas CarsCanvas;

    public void OpenNextCanvas()
    {
        //WelcomeCanvas.GetComponent<Animation>().Play("WelcomeCanvasSlideOut");
        CarsCanvas.GetComponent<Animation>().Play("CarsCanvasSlideIn");
    }
}
