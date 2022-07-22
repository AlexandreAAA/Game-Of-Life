using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    public class CellGrid : MonoBehaviour
    {
        #region Exposed

        public int m_width;
        public int m_height;
        public GameObject[,] m_gridCell;
        [SerializeField]
        private GameObject _cell;

        #endregion

        #region Init

        private void Awake()
        {
            m_gridCell = new GameObject[m_width, m_height];
        }

        private void Start()
        {
            for (int i = 0; i < m_width; i++)
            {
                for (int j = 0; j < m_height; j++)
                {
                    m_gridCell[i, j] = Instantiate(_cell, new Vector3(i, j, 0), Quaternion.identity, transform);
                }
            }

            Time.timeScale = 0;
        }

        #endregion

        #region Unity API

        private void Update()
        {
            for (int i = 0; i < m_width; i++)
            {
                for (int j = 0; j < m_height; j++)
                {
                    CelluleAdjacenteVivante(i, j);
                    m_gridCell[i, j].GetComponent<Cell>().m_nbCellulesVoisinesVivantes = nbCellulesVivantes;
                }
            }

            if (Time.timeScale == 1)
            {
                for (int i = 0; i < m_width; i++)
                {
                    for (int j = 0; j < m_height; j++)
                    {
                        RulesApplication(i, j);
                    }
                }
            }

            if (Input.GetButtonDown("Jump") && Time.timeScale == 0)
            {
                Time.timeScale = 1;
                Debug.Log("Pas pause");
            }
            else if (Time.timeScale == 1 && Input.GetButtonDown("Jump"))
            {
                Time.timeScale = 0;
                Debug.Log("pause");
            }
        }

        #endregion

        #region Main Method

        private bool IsHere(int i, int j)
        {
            return i >= 0 && i < m_width && j >= 0 && j < m_height;
        }

        private bool IsAlive(int i, int j)
        {
            return IsHere(i, j) && m_gridCell[i, j].GetComponent<Cell>().cellState == 1;
        }

        private int CelluleAdjacenteVivante(int i, int j)
        {
            nbCellulesVivantes = 0;

            if (IsAlive(i - 1, j - 1))
            {
                nbCellulesVivantes++;
            }

            if (IsAlive(i - 1, j))
            {
                nbCellulesVivantes++;
            }

            if (IsAlive(i - 1, j + 1))
            {
                nbCellulesVivantes++;
            }

            if (IsAlive(i, j - 1))
            {
                nbCellulesVivantes++;
            }

            if (IsAlive(i, j + 1))
            {
                nbCellulesVivantes++;
            }

            if (IsAlive(i + 1, j - 1))
            {
                nbCellulesVivantes++;
            }

            if (IsAlive(i + 1, j))
            {
                nbCellulesVivantes++;
            }

            if (IsAlive(i + 1, j + 1))
            {
                nbCellulesVivantes++;
            }

            return nbCellulesVivantes;
        }

        private void RulesApplication(int i, int j)
        {
            if (m_gridCell[i, j].GetComponent<Cell>().m_nbCellulesVoisinesVivantes == 3)
            {
                m_gridCell[i, j].GetComponent<Cell>().cellState = 1;
            }
            else if (m_gridCell[i, j].GetComponent<Cell>().m_nbCellulesVoisinesVivantes < 2 || m_gridCell[i, j].GetComponent<Cell>().m_nbCellulesVoisinesVivantes > 3)
            {
                m_gridCell[i, j].GetComponent<Cell>().cellState = 0;
            }
        }

        #endregion

        #region privates

        private int nbCellulesVivantes;

        #endregion
    }
}
