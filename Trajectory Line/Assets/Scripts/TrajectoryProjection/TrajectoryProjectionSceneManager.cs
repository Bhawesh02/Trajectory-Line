using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryProjectionSceneManager : MonoBehaviour
{
    #region SingeltonVariable

    private static TrajectoryProjectionSceneManager m_instance;
    public static TrajectoryProjectionSceneManager Instance => m_instance;
    
    #endregion

    private const string SCENE_NAME = "Trajectory Projection Scene";

    [SerializeField] 
    private List<Transform> m_trajectoryCollisionTransforms;
    
    private Scene m_trajectoryProjectionScene;
    
    public Scene TrajectoryProjectionScene => m_trajectoryProjectionScene;
        
    private void Awake()
    {
        #region Singelton

        if (m_instance)
        {
            Destroy(gameObject);
            return;
        }
        m_instance = this;

        #endregion

        CreatePhysicsScene();
    }
    
    private void CreatePhysicsScene()
    {
        m_trajectoryProjectionScene = SceneManager.CreateScene(SCENE_NAME,
            new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        GameObject ghostObj;
        for (int objectIndex = 0; objectIndex < m_trajectoryCollisionTransforms.Count; objectIndex++)
        {
            Transform trajectoryCollisionTransform = m_trajectoryCollisionTransforms[objectIndex];
            ghostObj = Instantiate(trajectoryCollisionTransform.gameObject,
                trajectoryCollisionTransform.transform.position, trajectoryCollisionTransform.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, m_trajectoryProjectionScene);
        }
    }
    
}
