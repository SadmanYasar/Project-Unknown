using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] bool AllowSloMo;
    [SerializeField] float slowMoSpeed;
    // Start is called before the first frame update
    void Start()
    {
        AllowSloMo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
             DoSlowMo();
        }
       
    }

    public void DoSlowMo() {
        if ( AllowSloMo )
        {
            Time.timeScale = slowMoSpeed;
            Time.fixedDeltaTime = Time.timeScale * 1/75;
            AllowSloMo = false;
        } else {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 1/75 ;
            AllowSloMo = true;
            
        }

        
           
    }
}
