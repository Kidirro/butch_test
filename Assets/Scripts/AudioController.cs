using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController :BaseSingleton<AudioController>
{
   private List<AudioSource> _enabledAudioSource = new List<AudioSource>();

   public void TryPlayAudio(AudioClip audio)
   {
      var audioSource = TryGetFreeSource();
      StartCoroutine(PlayAudioProcess( audioSource, audio));
   }


   private AudioSource TryGetFreeSource()
   {
      if (_enabledAudioSource.Count == 0)
      {
         AudioSource audioSource = (AudioSource) Instantiate(new GameObject("AudioSource"), transform).AddComponent(typeof(AudioSource));
         _enabledAudioSource.Add(audioSource);
      }

      var source = _enabledAudioSource[^1];
      _enabledAudioSource.Remove(source);
      return source;
   }

   private IEnumerator PlayAudioProcess(AudioSource source, AudioClip clip)
   {
      source.clip = clip;
      source.loop = false;
      source.Play();

      yield return new WaitForSeconds(clip.length);
      
      source.Stop();
      _enabledAudioSource.Add(source);
   }
}
