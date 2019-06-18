using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
	[SerializeField] private float fadeSpeed = 0.1f;

	[Header("References")]
	public List<AudioClip> mainClips;
	public List<AudioClip> mainMenuClips;
	public List<AudioClip> tutorialClips;

	[SerializeField] private List<AudioClip> currentPlaylist;
	private AudioSource player;
	private float fadeSteps;
	private bool isFading;
	private bool fadeIn;
	private bool fadeOut;

	void Awake()
    {
		player = GetComponent<AudioSource>();
	
		if (FindObjectsOfType<AudioManager>().Length > 1) Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);
	}

	private void Update()
	{
		if (!player.isPlaying && player.clip)
		{
			Play(GetNextClip);
		}

		if (fadeIn)
		{
			if (player.volume < 1)
			{
				player.volume += Time.deltaTime * fadeSpeed;
			}
			else
			{
				fadeIn = false;
			}
		}

		if (fadeOut)
		{
			if (player.volume > 0)
			{
				player.volume -= Time.deltaTime * fadeSpeed;
			}
			else
			{
				fadeOut = false;
			}
		}
	}

	public void Play(AudioClip clip)
	{
		player.clip = clip;
		FadeIn();
		player.Play();
	}
		
	public void FadeIn()
	{
		fadeOut = false;
		player.volume = 0;
		fadeIn = true;
	}

	public void FadeOut()
	{
		fadeIn = false;
		player.volume = 1;
		fadeOut = true;
	}

	public AudioClip GetCurrentClip { get { return player.clip; } }
	public AudioClip GetNextClip {
		get
		{
			for (int i = 0; i < currentPlaylist.Count; i++)
			{
				if (currentPlaylist[i] == GetCurrentClip)
				{
					if (i >= currentPlaylist.Count)
						return currentPlaylist[0];
					else
						return currentPlaylist[i + 1];
				}
			}

			return null;
		}
	}
	public AudioClip GetPrevClip
	{
		get
		{
			for (int i = 0; i < currentPlaylist.Count; i++)
			{
				if (currentPlaylist[i] == GetCurrentClip)
				{
					if (i <= 0)
						return currentPlaylist[currentPlaylist.Count];
					else
						return currentPlaylist[i - 1];
				}
			}

			return null;
		}
	}
	public List<AudioClip> CurrentPlaylist {
		get
		{
			return currentPlaylist;
		}
		set
		{
			currentPlaylist = value;
			Play(currentPlaylist[0]);
		}
	}
}
