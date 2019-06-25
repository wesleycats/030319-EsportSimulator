using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CheckMute : MonoBehaviour
{
	AudioSource audioPlayer;
	Image image;

	public UIManager uiManager;

	private void Awake()
	{
		audioPlayer = FindObjectOfType<AudioSource>();
		audioPlayer.GetComponent<AudioManager>().SendMute += Mute;
	}

	private void Start()
	{
		Mute(audioPlayer.mute);
	}

	public void Mute(bool muted)
	{
		image = GetComponent<Image>();

		if (muted)
			image.sprite = uiManager.mutedSprite;
		else
			image.sprite = uiManager.unMutedSprite;
	}
}
