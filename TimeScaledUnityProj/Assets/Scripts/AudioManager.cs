using UnityEngine;
using System.Collections;

public static class AudioManager
{
	private static BardScript bardInstance = null;
	private static BardScript Bard { 
		get 
		{
			if(bardInstance == null)
			{
				GameObject temp = GameObject.Find("Bard");
				if (temp)
				{
					bardInstance = temp.GetComponent<BardScript>();
				}
				else
				{
					temp = MonoBehaviour.Instantiate(Resources.Load("Bard"), Vector3.zero, Quaternion.identity) as GameObject;
					temp.name = "Bard";
					bardInstance = temp.GetComponent<BardScript>();
				}
			}
			return bardInstance;
		}
	}

	public static bool IsPlaying { get { return Bard.IsPlaying; } }

	public static void PlayClipByName(string name)
	{
		Bard.PlayClipByName(name);
	}

	public static void PlayClipByIndex(int index)
	{
		Bard.PlayClipByIndex(index);
	}

	public static void PlayAudioClip(AudioClip clip)
	{
		Bard.PlayAudioClip(clip);
	}

	public static void PlayBGMusicByName(string name, bool looped = true)
	{
		Bard.PlayBGMusicByName(name, looped);
	}

	public static void PlayBGMusicByIndex(int index, bool looped = true)
	{
		Bard.PlayBGMusicByIndex(index, looped);
	}

	public static void PlayBGMusic(AudioClip clip, bool looped = true)
	{
		Bard.PlayBGMusic(clip, looped);
	}

	public static void StopBGMusic()
	{
		Bard.StopBGMusic();
	}
}
