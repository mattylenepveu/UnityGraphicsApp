using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMesh))]
public class AI : MonoBehaviour
{
    public Transform m_destination1;
    public Transform m_destination2;
    public Transform m_destination3;
    public Transform m_destination4;

    NavMeshAgent m_agent;

	// Use this for initialization
	void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_destination1.position = m_agent.destination;
	}
	
	// Update is called once per frame
	void Update()
    {
		if (Vector3.Distance(m_destination1.position, transform.position) < 0.1f)
        {
            m_destination2.position = m_agent.destination;
        }

        if (Vector3.Distance(m_destination2.position, transform.position) < 0.1f)
        {
            m_destination3.position = m_agent.destination;
        }

        if (Vector3.Distance(m_destination3.position, transform.position) < 0.1f)
        {
            m_destination4.position = m_agent.destination;
        }

        if (Vector3.Distance(m_destination4.position, transform.position) < 0.1f)
        {
            m_destination1.position = m_agent.destination;
        }
    }
}
