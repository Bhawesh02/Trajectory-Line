using System;
using UnityEngine;

public class Cannon : MonoBehaviour {
    [SerializeField] private TrajectoryProjection m_trajectoryProjection;
    [SerializeField] private Ball m_ballPrefab;
    [SerializeField] private float m_force = 20;
    [SerializeField] private Transform m_ballSpawn;
    [SerializeField] private float m_maxRotationAngle;
    [SerializeField] private float m_rotationSpeed;

    
    private bool m_isMoved;
    private float m_verticaAxisInput;
    private float m_horizontalAxisInput;
    private Vector3 m_turnAngle;
    private float m_initialRotation;

    private void Start()
    {
        m_trajectoryProjection.SimulateTrajectory(m_ballPrefab, m_ballSpawn.position, m_ballSpawn.forward * m_force);
        m_initialRotation = transform.localEulerAngles.y;
    }

    private void Update()
    {
        m_isMoved = false;
        m_verticaAxisInput = Input.GetAxisRaw("Vertical");
        m_horizontalAxisInput = Input.GetAxisRaw("Horizontal");
        HandelRotation();
        HandelMovement();
        FireBall();
    }

    private void FireBall()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ball ball = Instantiate(m_ballPrefab, m_ballSpawn.position, Quaternion.identity);
            ball.Init(m_ballSpawn.forward * m_force, false);
        }
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
        if (Mathf.Approximately(m_verticaAxisInput, 0f))
        {
            return;
        }
        m_isMoved = true;
        
    }

    private void HandelRotation()
    {
        if (Mathf.Approximately(m_horizontalAxisInput, 0f))
        {
            return;
        }
        m_isMoved = true;
        m_turnAngle = transform.localEulerAngles;
        m_turnAngle.y = Mathf.Lerp(m_turnAngle.y, 
            m_maxRotationAngle * m_horizontalAxisInput + m_initialRotation,
            m_rotationSpeed * Time.deltaTime);
        transform.localEulerAngles = m_turnAngle;
    }
}