using UnityEngine;

namespace Cattle.Animation
{
    public class ChildrenSpritePainter : BaseComponent
    {
        public bool propagateColor = true;
        
        SpriteRenderer spriteRenderer;
        
        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            foreach (SpriteRenderer childSpriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                if (propagateColor)
                {
                    childSpriteRenderer.color = spriteRenderer.color;
                }
            }
        }
    }
}