/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

using UnityEngine;

public class CarParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _exhaustParticle;

    private void OnEnable()
    {
        CarEventBus.OnExhaust += ExhaustParticle;
    }

    private void OnDisable()
    {
        CarEventBus.OnExhaust -= ExhaustParticle;
    }

    public void ExhaustParticle()
    {
        _exhaustParticle.Play();
    }
}
