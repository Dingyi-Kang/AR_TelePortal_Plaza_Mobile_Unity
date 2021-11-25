using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalManager : MonoBehaviour
{

    public GameObject ARCamera;

    public GameObject Sponza;

    private Material[] Sponzamaterials;

    // Start is called before the first frame update
    void Start()
    {
        Sponzamaterials = Sponza.GetComponent<Renderer>().sharedMaterials;
    }

    // Update is called once per frame
    void OnTriggerStay(Collider collider)
    {


        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(ARCamera.transform.position);
        //negative means the camera is inside the building
        if (camPositionInPortalSpace.y < 0.5f) {

            //disable stencil test

            for (int i = 0; i < Sponzamaterials.Length; ++i) {

                Sponzamaterials[i].SetInt("_StencilComp", (int)CompareFunction.Always);
            }

        }

        else
        {

            //ebable estencil  
            for (int i = 0; i < Sponzamaterials.Length; ++i)
            {

                Sponzamaterials[i].SetInt("_StencilComp", (int)CompareFunction.Equal);
            }
        }


    }
}
