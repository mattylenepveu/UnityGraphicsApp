using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class EndGame : MonoBehaviour
{
    PlayerController m_pc;
    public GameObject m_pacman;

	// Use this for initialization
	void Awake()
    {
        if (m_pacman)
        {
            m_pc = m_pacman.GetComponent<PlayerController>();
        }
    }
	
	// Update is called once per frame
	void Update() {}

    public void RestartGame()
    {
        EditorSceneManager.LoadScene(0);
    }

    public void RightArrow()
    {
        m_pc.m_currentLookVector = m_pc.m_rightVec;
    }

    public void LeftArrow()
    {
        m_pc.m_currentLookVector = m_pc.m_leftVec;
    }

    public void UpArrow()
    {
        m_pc.m_currentLookVector = m_pc.m_upVec;
    }

    public void DownArrow()
    {
        m_pc.m_currentLookVector = m_pc.m_downVec;
    }
}
