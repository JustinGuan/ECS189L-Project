using Embers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/**
 * Adapted from "Indroduction to AUDIO in Unity" by Brackeys:
 * https://www.youtube.com/watch?v=6OT43pvUyfY
 */

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup musicMixerGroup;

    [SerializeField]
    private AudioMixerGroup sfxMixerGroup;

    [SerializeField]
    private List<Sound> musicTracks;

    [SerializeField]
    private List<Sound> sfxClips;

    [SerializeField]
    private Sound nearSound;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float nearDistance = 5f;

    private Sound trackPlaying;
    private Sound trackFading;
    private Sound sfxPlaying;
    private bool isNearSoundPlaying;

    void Awake()
    {
        foreach (var track in musicTracks)
        {
            track.source = gameObject.AddComponent<AudioSource>();
            track.source.clip = track.clip;
            track.source.volume = track.volume;
            track.source.pitch = track.pitch;
            track.source.loop = track.loop;
            track.source.outputAudioMixerGroup = musicMixerGroup;
        }

        foreach (var clip in sfxClips)
        {
            clip.source = gameObject.AddComponent<AudioSource>();
            clip.source.clip = clip.clip;
            clip.source.volume = clip.volume;
            clip.source.pitch = clip.pitch;
            clip.source.loop = clip.loop;
            clip.source.outputAudioMixerGroup = sfxMixerGroup;
        }
    }

    public void PlayMusicTrack(string title)
    {
        var track = musicTracks.Find(t => t.name == title);

        if (track == null)
        {
            Debug.Log("Sound track not found: " + title);
            return;
        }

        track.source.Play();

        if (trackPlaying != null)
        {
            trackPlaying.source.Stop();
        }

        trackPlaying = track;
    }

    public void PlaySoundEffect(string title)
    {
        var track = sfxClips.Find(t => t.name == title);

        if (track == null)
        {
            Debug.Log("Sound track not found: " + title);
            return;
        }

        track.source.Play();
    }

    public void StopSoundEffect(string title)
    {
        var track = sfxClips.Find(t => t.name == title);

        if (track == null)
        {
            Debug.Log("Sound track not found: " + title);
            return;
        }

        track.source.Stop();
    }

}
