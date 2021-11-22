using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class ARPlacement : MonoBehaviour
{


    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;


    private GameObject spawnedObject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;


    // Start is called before the first frame update
    void Start()
    {

        aRRaycastManager = FindObjectOfType<ARRaycastManager>();


    }

    // need to update placement indicator, placement pose and spawn
    void Update()
    {


        if(spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
        }


        UpdatePlacementPose();
        UpdatePlacementIndicator();

    }


    void UpdatePlacementPose() {

        if (spawnedObject == null && placementPoseIsValid) {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        } else
        {
            placementIndicator.SetActive(false);

        }
    }


    void UpdatePlacementIndicator() {
        
        //0.5x, and 0.5y is basically the center of the screen
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid) {
            PlacementPose = hits[0].pose;
        } 

    }

    void ARPlaceObject() {
        Debug.Log("activate");

        spawnedObject = Instantiate(arObjectToSpawn, PlacementPose.position, PlacementPose.rotation);
        spawnedObject.SetActive(true);
    }
    


}
