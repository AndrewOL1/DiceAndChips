using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDie : MonoBehaviour
{
    #region Variabls
    [SerializeField]
    float MaxForce;
    [SerializeField]
    float maxHoldTime;
    [SerializeField]
    int force;
    Rigidbody die;
    [SerializeField]
    bool fired;
    [SerializeField]float turnSpeed, aimXClamp, aimYClamp;
    Vector3 rotation;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        die = gameObject.transform.Find("Die").GetComponent<Rigidbody>();
        rotation = transform.eulerAngles;
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
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && !fired)
        {
            CameraManager.Instance.SwitchToCamera("topDown");
        }
        if(rotation!=transform.eulerAngles && !fired) 
        {
            predict();
            rotation = transform.eulerAngles;
        } 
        aim();
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
    void predict()
    {
        TrajectorySim.Instance.Predict(die.gameObject, transform.position, transform.forward * force);
    }
    void wipePrediction()
    {
        transform.GetComponent<LineRenderer>().positionCount = 0;
    }

    #region Variabls

    #endregion
}
