
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorLock : MonoBehaviour
{

    Animator m_Animator;
    int isOpenHash;
    bool isClosed = true;
    bool isPlayed = false;
    float runningTime = 0.0f;


    public float doorLockOpenCloseTime = 3.0f;
    public TextMeshProUGUI doorText;
    public AudioSource adventureMusic;
   

    void Awake()
    {
        // Cache Animator Component
        m_Animator = GetComponent<Animator>();
        isOpenHash = Animator.StringToHash("open");
        adventureMusic.volume = 0;
    }

    public void Update()
    {
        
        if (isPlayed)
        {
            runningTime += Time.deltaTime;
            float blend = runningTime / doorLockOpenCloseTime;
            adventureMusic.volume = Mathf.Lerp(0, 1, Mathf.Clamp(blend, 0, 1));
            //doorText.text = adventureMusic.volume.ToString();
         
        }
        
        else {
            runningTime -= Time.deltaTime;
            float blend = runningTime / doorLockOpenCloseTime;
            adventureMusic.volume = Mathf.Lerp(0, 1, Mathf.Clamp(blend, 0, 1));
            //doorText.text = adventureMusic.volume.ToString();
            if (adventureMusic.volume == 0.0f) {
                adventureMusic.Stop();
            }
        }
        


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
                doorText.text = "Doors Open";
                isPlayed = true;
                adventureMusic.Play();
                adventureMusic.volume = 0;
                runningTime = 0.0f;


            }
            else
            {
                //close it
                m_Animator.SetBool(isOpenHash, false);
                isClosed = true;
                doorText.text = "Doors Closed";
                isPlayed = false;
                
                runningTime = doorLockOpenCloseTime;

            }

        }

    }

}
