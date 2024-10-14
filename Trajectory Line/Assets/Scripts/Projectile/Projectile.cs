using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private Renderer m_renderer;

    public void Init(Vector3 initVelocity, bool isGhost)
    {
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.AddForce(initVelocity, ForceMode.Impulse);
        if (isGhost)
        {
            m_renderer.enabled = false;
        }
    }
}