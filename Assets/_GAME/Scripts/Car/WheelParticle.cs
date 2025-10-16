/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using System.Collections;
using UnityEngine;

public class WheelParticle : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private Wheel _wheel;
    
    [SerializeField] private ParticleSystem _wheelBaseParticle;
    
    [SerializeField] private ParticleSystem _asphaltAndSnowParticle;
    [SerializeField] private ParticleSystem _gravelParticle;
    [SerializeField] private ParticleSystem _dirtParticle;

    private void Start()
    {
        StartCoroutine(CheckParticleCor());
    }

    public void UpdateParticle()
    {
        if (_car.GetCurrentSpeed() < 5)
        {
            if (_dirtParticle != null)
                _dirtParticle.Stop();
            
            if (_wheelBaseParticle!= null)
                _wheelBaseParticle.Stop();
        }
        else
        {
            if (!_wheel._isDriveTire) return;
            
            if (_wheelBaseParticle!= null)
                _wheelBaseParticle.Play();
            
            switch (_wheel.CurrentSurface)
            {
                case SurfaceType.Asphalt:
                    break;
                
                case SurfaceType.Gravel:
                    break;
                
                case SurfaceType.Dirt:
                    if (_dirtParticle != null)
                        _dirtParticle.Play();
                    break;
                
                case SurfaceType.Snow:
                    break;
                
                case SurfaceType.Ice:
                    break;
                
                case SurfaceType.Other:
                    break;
            }
        }
    }

    public IEnumerator CheckParticleCor()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            UpdateParticle();
        }
    }
}
