using ChartAndGraph;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;
    //public GameObject mainprefab;
    public GameObject Canvas;
    public ARPlaneManager planeManager;
    private int placedPrefabCount;

    public GameObject PlacedPrefab
    {
        get 
        {
            return placedPrefab;
        }
        set 
        {
            placedPrefab = value;
        }
    }

    private ARRaycastManager arRaycastManager;

    void Awake() 
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }
    private void Start()
    {
        placedPrefab.SetActive(false);
        Canvas.SetActive(false);
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                bool isOverUI = touchPosition.IsPointOverUIObject();

                if (!isOverUI && arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = hits[0].pose;
                    if(placedPrefabCount <1)
                    {
                        placedPrefab.transform.position = hitPose.position;
                        placedPrefab.transform.rotation = hitPose.rotation;
                        placedPrefab.SetActive(true);
                        Canvas.SetActive(true);                                 
                        placedPrefabCount++;
                    }
                }
            }
        }
        if (placedPrefab.activeInHierarchy)
        {
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }
    }

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}
