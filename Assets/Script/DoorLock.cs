
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLock : MonoBehaviour
{

    Animator m_Animator;
    int isOpenHash;
    bool isClosed = true;

    public Material newMat;
    public Material defaultMat;


    void Awake()
    {
        // Cache Animator Component
        m_Animator = GetComponent<Animator>();
        isOpenHash = Animator.StringToHash("open");
    }

    public void Update()
    {
        // Play Open Door Animation
        //potential issue: touch too long time, and update too quick
        //this is due to one button tap and unpate func
        //if(Input.GetKey(KeyCode.A))
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            if (isClosed)
            {
                //open it
                m_Animator.SetBool(isOpenHash, true);
                isClosed = false;
                //RenderSettings.skybox = newMat;
                //SceneManager.LoadScene("waterWorld", LoadSceneMode.Additive);

            }
            else
            {
                //close it
                m_Animator.SetBool(isOpenHash, false);
                isClosed = true;
                //RenderSettings.skybox = defaultMat;
                //SceneManager.UnloadScene("waterWorld");
            }

        }

    }

}
