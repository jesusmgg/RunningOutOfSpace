using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Cattle.Weapons
{
    public class TileDestroyingExplosion :  BaseComponent
    {
        public float selfDestructionDelay = 0.0f;
        
        void Start()
        {
            Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + selfDestructionDelay); 
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            Tilemap tilemap = other.gameObject.GetComponent<Tilemap>();

            Vector3 hitPosition = Vector3.zero;
            
            if (tilemap != null)
            {
                ContactPoint2D[] contacts = new ContactPoint2D[64];
                other.GetContacts(contacts);
                    
                foreach (ContactPoint2D hit in contacts)
                {
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                    StartCoroutine(DestroyTile(tilemap, hitPosition));
                }
            }
        }
        
        IEnumerator DestroyTile(Tilemap tilemap, Vector3 hitPosition)
        {
            yield return new WaitForFixedUpdate();
            tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            yield return null;
        }
    }
}