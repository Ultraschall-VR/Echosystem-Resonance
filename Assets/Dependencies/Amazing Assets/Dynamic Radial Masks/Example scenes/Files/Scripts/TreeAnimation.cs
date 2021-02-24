using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AmazingAssets
{
    namespace DynamicRadialMasks
    {
        public class TreeAnimation : MonoBehaviour
        {
            public DRMController drmController;

            new Animation animation;
            bool animationHasPlayed;


            void Start()
            {
                animation = GetComponent<Animation>();
            }

            void Update()
            {
                float maskValue = drmController.GetMaskValue(transform.position);


                if(maskValue > 0.4 && maskValue < 0.6f)  
                {
                    if (animation.isPlaying == false && animationHasPlayed == false)
                    {
                        animation.Play();

                        animationHasPlayed = true;
                    }
                }
                else
                {
                    animationHasPlayed = false;
                }
            }
        }
    }
}