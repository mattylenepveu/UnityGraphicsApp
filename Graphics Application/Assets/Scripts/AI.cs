using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMesh))]
public class AI : MonoBehaviour
{
    /*public Transform m_destination1;
    public Transform m_destination2;
    public Transform m_destination3;
    public Transform m_destination4;*/

    NavMeshAgent m_agent;

    private Vector3 m_rightVec;
    private Vector3 m_leftVec;
    private Vector3 m_upVec;
    private Vector3 m_downVec;

    // Use this for initialization
    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();

        m_rightVec = new Vector3(0, 90, 0);
        m_leftVec = new Vector3(0, -90, 0);
        m_upVec = new Vector3(0, 0, 0);
        m_downVec = new Vector3(0, 180, 0);
    }
	
	// Update is called once per frame
	void Update()
    {
		/*if (Vector3.Distance(m_destination1.position, transform.position) < 0.1f)
        {
            m_destination2.position = m_agent.destination;
            Debug.Log("IT WORKS");
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
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Destination 1")
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_downVec));
        }

        if (other.name == "Destination 2")
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_leftVec));
        }

        if (other.name == "Destination 3")
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_upVec));
        }

        if (other.name == "Destination 4")
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_rightVec));
        }
    }
}
