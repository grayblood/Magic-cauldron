using Autohand.Demo;
using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosionController : MonoBehaviour
{
    VisualEffect visualEffect;

    [SerializeField] float explosionRadius;

    [SerializeField] float explosionForce;

    private void Awake()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    public void PlayAnimation()
    {
        visualEffect.Play();
        Audio_Controller.PlaySound(Audio_Controller.Sounds.C_Explosion);
        Explode();
    }
    void Explode()
    {
        var hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (AutoHandPlayer.Instance.body == hit.attachedRigidbody)
            {
                AutoHandPlayer.Instance.DisableGrounding(0.05f);
                var dist = Vector3.Distance(hit.attachedRigidbody.position, transform.position);
                explosionForce *= 2;
                hit.attachedRigidbody.AddExplosionForce(explosionForce - explosionForce * (dist / explosionRadius), transform.position, explosionRadius);
                explosionForce /= 2;
            }
            if (hit.attachedRigidbody != null)
            {
                var dist = Vector3.Distance(hit.attachedRigidbody.position, transform.position);
                hit.attachedRigidbody.AddExplosionForce(explosionForce - explosionForce * (dist / explosionRadius), transform.position, explosionRadius);
            }
        }
    }
    public void StopAnimation()
    {
        visualEffect.Stop();
    }
}
