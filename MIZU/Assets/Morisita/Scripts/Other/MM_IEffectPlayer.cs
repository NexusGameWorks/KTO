using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class MM_IEffectPlayer : MonoBehaviour
{
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    public ParticleSystem _particle;
    // �G�t�F�N�g���o���ꏊ
    protected Transform _particleTransform;

    virtual public void Play(float time)
    {
        ParticleInstantiate(_particleTransform,time);
    }
    virtual public void Play()
    {
        ParticleInstantiate(_particleTransform);
    }

    virtual async public void ParticleInstantiate(Transform tf)
    {
        var token = this.GetCancellationTokenOnDestroy();
        ParticleSystem particle = Instantiate(_particle, tf);

        float lifetime = particle.main.startLifetimeMultiplier;
        particle.Play();

        await UniTask.Delay(TimeSpan.FromSeconds(lifetime),cancellationToken :token);

        Destroy(particle.gameObject);

    }
    virtual async public void ParticleInstantiate(Transform tf, float time)
    {
        var token = this.GetCancellationTokenOnDestroy();
        ParticleSystem particle = Instantiate(_particle, tf);

        float lifetime = time;

        particle.Play();

        await UniTask.Delay(TimeSpan.FromSeconds(lifetime), cancellationToken: token);

        Destroy(particle);

    }

    public void SetParticleTransform(Transform tf)
    {
        _particleTransform = tf;
    }
   
    public void SameParentObjectRotation()
    {
        _particleTransform.rotation = this.transform.rotation;
    }

    public void SameParentObjectScale()
    {
        _particleTransform.localScale = this.transform.localScale;
    }
}



