using Cinemachine;
using UnityEngine;

namespace Cattle
{
    public class CinemachineConfinerFinder : MonoBehaviour
    {
        void Start()
        {
            PolygonCollider2D collider2D = FindObjectOfType<PolygonCollider2D>();

            if (collider2D != null)
            {
                GetComponent<CinemachineConfiner>().m_BoundingShape2D = collider2D;
            }
        }
    }
}