using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    public class Cell : MonoBehaviour
    {
        #region Exposed

        [SerializeField]
        private Material _alive;
        [SerializeField]
        private Material _dead;
        [Range(0, 1)]
        public int cellState;
        [SerializeField]
        private LayerMask m_cellLayer;
        public int m_nbCellulesVoisinesVivantes = 0;

        #endregion

        #region Unity API;

        private void Awake()
        {
            _cellRenderer = GetComponent<Renderer>();
            _cameraMain = Camera.main;
        }

        private void Update()
        {
            GetCell();
            ChangeCell();
        }

        #endregion

        #region Methods

        private void GetCell()
        {
            Ray ray = _cameraMain.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, m_cellLayer);

            if (hit.collider != null)
            {
                Vector3 hitPoint = new Vector3(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y), 0);

                if (hitPoint.x == transform.position.x && hitPoint.y == transform.position.y && Input.GetMouseButton(0))
                {
                    cellState = (cellState == 1) ? 0 : 1;
                }
            }
        }

        private void ChangeCell()
        {
            if (cellState == 1)
            {
                _cellRenderer.material = _alive;
            }
            if (cellState < 1)
            {
                _cellRenderer.material = _dead;
            }
        }
        #endregion

        #region

        private Renderer _cellRenderer;
        private Camera _cameraMain;

        #endregion
    }
}
