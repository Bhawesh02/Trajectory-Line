using System;
using UnityEngine;

public class Cannon : MonoBehaviour {
    [SerializeField] private TrajectoryProjection m_trajectoryProjection;
    [SerializeField] private Ball m_ballPrefab;
    [SerializeField] private float m_force = 20;
    [SerializeField] private Transform m_ballSpawn;

    [SerializeField]private bool m_isMoved;

    private void Start()
    {
        m_trajectoryProjection.SimulateTrajectory(m_ballPrefab, m_ballSpawn.position, m_ballSpawn.forward * m_force);
    }

    private void Update()
    {
        //m_isMoved = false;
        HandelRotation();
        HandelMovement();
       
    }

    private void FixedUpdate()
    {
        if (m_isMoved)
        {
            m_trajectoryProjection.SimulateTrajectory(m_ballPrefab, m_ballSpawn.position, m_ballSpawn.forward * m_force);
        }
    }

    private void HandelMovement()
    {
      
    }

    private void HandelRotation()
    {
      
    }
}