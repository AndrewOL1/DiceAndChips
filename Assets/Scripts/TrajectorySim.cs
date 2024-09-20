using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectorySim : MonoBehaviourSingleton<TrajectorySim> 
{

    public GameObject obstacles;
    public int maxIterations;

    Scene currentScene;
    Scene predictionScene;

    PhysicsScene currentPhysicsScene;
    PhysicsScene predictionPhysicsScene;

    List<GameObject> ghostObjs = new List<GameObject>();

    LineRenderer lineRenderer;
    GameObject ghost;
    bool hitRigidbody = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics.simulationMode = SimulationMode.Script;

        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene();

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("Prediction", parameters);
        predictionPhysicsScene = predictionScene.GetPhysicsScene();

        lineRenderer = GetComponent<LineRenderer>();
        CopyAllObj();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentPhysicsScene.IsValid())
        {
            currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }
    public Scene GetCurrentScene()
    {
        return currentScene;
    }

    public void CopyAllObj()
    {
        foreach (Transform t in obstacles.transform)
        {
            if (t.gameObject.GetComponent<Collider>() != null)
            {
                GameObject ghostT = Instantiate(t.gameObject);
                ghostT.transform.position = t.transform.position;
                ghostT.transform.rotation = t.transform.rotation;
                Renderer ghostR = ghostT.GetComponent<Renderer>();
                if (ghostR)
                {
                    ghostR.enabled = false;
                }
                SceneManager.MoveGameObjectToScene(ghostT,predictionScene);
                ghostT.gameObject.GetComponent<ghostObj>().ghost = true;
                ghostObjs.Add(ghostT);  
            }
        }
    }

    void killObjs()
    {
        foreach (var o in ghostObjs)
        {
            Destroy(o);
        }
        ghostObjs.Clear();
    }

    public void Predict(GameObject die, Vector3 currentPos, Vector3 force)
    {
        if (currentPhysicsScene.IsValid() && predictionPhysicsScene.IsValid())
        {
            if (ghost == null)
            {
                ghost = Instantiate(die);
                SceneManager.MoveGameObjectToScene(ghost, predictionScene);
            }
            ghost.GetComponent<Rigidbody>().useGravity = true;
            ghost.transform.position = currentPos;
            ghost.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            lineRenderer.positionCount = 0;
            lineRenderer.positionCount = maxIterations;


            for (int i = 0; i < maxIterations; i++)
            {
                predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
                lineRenderer.SetPosition(i, ghost.transform.position);
                if (hitRigidbody)
                {
                    hitRigidbody=false;
                    lineRenderer.positionCount = i;
                    break;
                }
            }

            Destroy(ghost);
        }
    }
    public void Hit()
    {
        hitRigidbody = true;
    }

    void OnDestroy()
    {
        killObjs();
    }
}
