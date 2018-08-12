using UnityEngine;

namespace Cattle.Animation
{
    public class ChildrenSpriteFlipper : BaseComponent
    {
        public bool flipX = true;
        public bool flipY = true;
        
        SpriteRenderer spriteRenderer;
        
        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            foreach (SpriteRenderer childSpriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                if (flipX)
                {
                    childSpriteRenderer.flipX = spriteRenderer.flipX;   
                }

                if (flipY)
                {
                    childSpriteRenderer.flipY = spriteRenderer.flipY;    
                }
            }
        }
    }
}