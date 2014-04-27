using UnityEngine;
using System.Collections;

public class BardScript : MonoBehaviour 
{
	private static BardScript Main = null;
	public AudioClip[] soundClips;
	public AudioClip[] bgMusic;

	void Awake () 
	{
		if (Main == null)
		{
			Main = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
			Destroy(this.gameObject);
	}

	void OnDestroy()
	{
		if (Main == this)
			Main = null;
	}

	public void PlayClipByName(string name)
	{
		foreach(AudioClip clip in soundClips)
		{
			if (clip.name == name)
			{
				audio.PlayOneShot(clip);
				return;
			}
		}
		throw new UnityException("PlayClipByName: A sound clip does not exist with name " + name + "!");
	}

	public void PlayClipByIndex(int index)
	{
		if (index >= soundClips.Length || index < 0)
			throw new UnityException("PlayClipByIndex: Index out of range!");

		audio.PlayOneShot(soundClips[index]);
	}

	public void PlayAudioClip(AudioClip clip)
	{
		audio.PlayOneShot(clip);
	}

	public void PlayBGMusicByName(string name, bool looped = true)
	{
		foreach (AudioClip clip in bgMusic)
		{
			if (clip.name == name)
			{
				audio.Stop();
				audio.clip = clip;
				audio.loop = looped;
				audio.Play();
				return;
			}
		}
		throw new UnityException("PlayBGMusicByName: A sound clip does not exist with name " + name + "!");
	}

	public void PlayBGMusicByIndex(int index, bool looped = true)
	{
		if (index >= bgMusic.Length || index < 0)
			throw new UnityException("PlayBGMusicByIndex: Index out of range!");

		audio.Stop();
		audio.clip = bgMusic[index];
		audio.loop = looped;
		audio.Play();
	}

	public void PlayBGMusic(AudioClip clip, bool looped = true)
	{
		audio.Stop();
		audio.clip = clip;
		audio.loop = looped;
		audio.Play();
	}

	public void StopBGMusic()
	{
		audio.Stop();
	}
}
