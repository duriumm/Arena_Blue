using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlickerLamp : MonoBehaviour
{
    Light2D torch2dLightComponent;

    private float minOuterRadius;
    private float maxOuterRadius;

    private float minInnerRadius;
    private float maxInnerRadius;

    private float minIntensity;
    private float maxIntensity;




    void Start()
    {

        //fireParticleEffect = this.gameObject.GetComponent<ParticleSystem>();
        torch2dLightComponent = this.gameObject.GetComponent<Light2D>();
        minOuterRadius = torch2dLightComponent.pointLightOuterRadius - 0.05f;
        maxOuterRadius = torch2dLightComponent.pointLightOuterRadius + 0.05f;

        minInnerRadius = torch2dLightComponent.pointLightInnerRadius - 0.05f;
        maxInnerRadius = torch2dLightComponent.pointLightInnerRadius + 0.05f;

        minIntensity = torch2dLightComponent.intensity - 0.02f;
        maxIntensity = torch2dLightComponent.intensity + 0.02f;



        InvokeRepeating("Flicker", 0.0f, 0.2f);
    }

    public void Flicker()
    {

        // This random number generator is to make the light radius flicker
        float rndNumber = Random.Range(minOuterRadius, maxOuterRadius);
        torch2dLightComponent.pointLightOuterRadius = rndNumber;
        float rndNumber2 = Random.Range(minInnerRadius, maxInnerRadius);
        torch2dLightComponent.pointLightInnerRadius = rndNumber2;
        float rndNumber3 = Random.Range(minIntensity, maxIntensity);
        torch2dLightComponent.intensity = rndNumber3;

    }

    //private void StopFlicker()
    //{
    //    CancelInvoke();
    //    fireParticleEffect.Stop();
    //    torch2dLightComponent.enabled = false;
    //    isTorchActive = false;
    //}




}
