using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class LookAtMouse : MonoBehaviour
{
    #region variables
    Vector2 mouseFinal;
    Vector2 smoothMouse;
    Vector2 targetDirection;
    Vector2 targetCharacterDirection;

    public Vector2 clampInDegrees = new Vector2(360f, 180f);
    public Vector2 sensitivity = new Vector2(0.1f, 0.1f);
    public Vector2 smoothing = new Vector2(1f, 1f);

    public bool lockCursor;
    public GameObject characterBody;
    #endregion
    
}
