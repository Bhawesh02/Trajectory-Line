using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryProjection : MonoBehaviour
{
    [SerializeField] 
    private LineRenderer m_lineRenderer;
    [SerializeField] 
    private int m_maxPhysicsSimulationFrames;

    private Scene m_trajectoryProjectionScene;
    private Projectile m_ghostProjectile;
    

    public void SimulateTrajectory(Projectile projectile, Vector3 initialPosition, Vector3 initialVelocity)
    {
        if (!m_ghostProjectile)
        {
            m_trajectoryProjectionScene = TrajectoryProjectionSceneManager.Instance.TrajectoryProjectionScene;
            m_ghostProjectile = Instantiate(projectile, initialPosition, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(m_ghostProjectile.gameObject, m_trajectoryProjectionScene);
        }
        else
        {
            m_ghostProjectile.transform.position = initialPosition;
        }
        m_ghostProjectile.Init(initialVelocity, true);
        m_lineRenderer.positionCount = m_maxPhysicsSimulationFrames;
        for (int frameIndex = 0; frameIndex < m_maxPhysicsSimulationFrames; frameIndex++)
        {
            m_trajectoryProjectionScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
            m_lineRenderer.SetPosition(frameIndex, m_ghostProjectile.transform.position);
        }
    }
}
