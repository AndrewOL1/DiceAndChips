using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowDie : MonoBehaviour
{
    #region Variabls
        [Header("PowerSettings")]
        [Space(5)]
        [Tooltip("The max force the die can be thrown")]
        [SerializeField]
        float maxForce;
        [Tooltip("The min force the die can be thrown")]
        [SerializeField]
        float minForce;
        [Tooltip("The rate of change the mouse position effects the power")]
        [SerializeField]
        float rateOfPowerChange;
        [Tooltip("The force of the throw")]
        [SerializeField]
        float force;
        [Tooltip("Have we thrown")]
        [SerializeField]
        bool fired;
    
    
    [Header("AimingSettings")]
    [Space(5)]
    [Tooltip("How far you can aim horizontaly to oneside")]
    [SerializeField]float aimYClamp;
    [Tooltip("How far you can aim vertcaly to oneside")]
    [SerializeField] float aimXClamp;
    [Tooltip("The rate of change the input aixs effects the power")]
    [SerializeField] float turnSpeed;
    

    Vector3 rotation;
    bool calcingPower=false;
    Rigidbody die;
    public bool camraDelay = false;
    public bool topDown = false;
    private Vector3 diePos;
    [SerializeField] GameObject dieObj;
    [SerializeField] int ThrowsAllowed;
    int ThrowsRemaining;
    Vector3 dieScale;
    userSettings userSettting;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        die = gameObject.transform.Find("Die").GetComponent<Rigidbody>();
        rotation = transform.eulerAngles;
        diePos=die.transform.position;
        ThrowsRemaining = ThrowsAllowed;
        dieScale= die.transform.localScale;
        userSettting=gameObject.transform.Find("Settings").GetComponent<userSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !fired)
        {
            Launch();
            fired = true;
            wipePrediction();
            CameraManager.Instance.SwitchToCamera("chase");
            ThrowsRemaining--;


        }
        else if(Input.GetKeyDown(KeyCode.Space) && fired && ThrowsRemaining != 0)
        {
            nextShot();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && fired && ThrowsRemaining == 0)
        {
            //userSettings.newScore() need to get the score
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !fired)
        {
            if (!camraDelay)
            {
                if (!topDown)
                {
                    CameraManager.Instance.SwitchToCamera("topDown");
                    StartCoroutine("CamDelay");
                    topDown = true;
                }
                else
                {
                    CameraManager.Instance.SwitchToCamera("aim");
                    StartCoroutine("CamDelay");
                    topDown = false;
                }
                camraDelay = true;
            }
        }
        if(rotation!=transform.eulerAngles && !fired) 
        {
            predict();
            rotation = transform.eulerAngles;
        } 
        aim();
        Power();
    }
     void Launch()
    {
        die.AddForce(transform.forward * force, ForceMode.Impulse);//test
        die.useGravity = true;

    }
    void aim()
    {
        if(!fired)
        {
            float currentYRotation = transform.eulerAngles.y;
            if (currentYRotation > 180f)
                currentYRotation -= 360f;

            float rotationAmountY = Input.GetAxis("Horizontal") * turnSpeed + currentYRotation;
                rotationAmountY = Mathf.Clamp(rotationAmountY, -aimYClamp, aimYClamp);

            float currentXRotation = transform.eulerAngles.x;
            if (currentXRotation > 180f)
                currentXRotation -= 360f;

            float rotationAmountX = Input.GetAxis("Vertical") * turnSpeed + currentXRotation;
            rotationAmountX = Mathf.Clamp(rotationAmountX, -aimXClamp, aimXClamp);

            transform.eulerAngles = new Vector3(rotationAmountX, rotationAmountY, 0);
            

        }
    }
    void Power()
    {
        if (Input.GetMouseButtonDown(0) && !fired && !calcingPower)
        {
            calcingPower = true;
            StartCoroutine("PowerCal");
        }
    }
    private IEnumerator PowerCal()
    {
        float pos = Input.mousePosition.y;
        while (!Input.GetMouseButtonUp(0))
        {

            if (pos > Input.mousePosition.y)
            {
                force -= rateOfPowerChange;
                force = Mathf.Clamp(force, minForce, maxForce);
                predict();
            }
            else if (pos < Input.mousePosition.y)
            {
                force += rateOfPowerChange;
                force = Mathf.Clamp(force, minForce, maxForce);
                predict();
            }
            pos = Input.mousePosition.y;
            yield return null;
        }
        calcingPower = false;
    }
    private IEnumerator CamDelay()
    {
            yield return new WaitForSeconds(0.2f);
            camraDelay = false;
    }
    void nextShot()
    {
        if (die.velocity.magnitude < 1)
        {
            die.transform.parent = null;
            GameObject newDie= Instantiate(dieObj, diePos, Quaternion.Euler(rotation));
            die = newDie.GetComponent<Rigidbody>();
            die.transform.parent = this.transform;
            die.useGravity = false;
            fired = false;
            die.transform.localScale = dieScale;
            CameraManager.Instance.SwitchToCamera("aim");
            CameraManager.Instance.changeTarget(die.transform);

        }
    }
    #region Predictions
    void predict()
    {
        TrajectorySim.Instance.Predict(die.gameObject, transform.position, transform.forward * force);
    }
    void wipePrediction()
    {
        transform.GetComponent<LineRenderer>().positionCount = 0;
    }

    #endregion
}
