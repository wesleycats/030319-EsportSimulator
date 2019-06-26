using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CheckMute : MonoBehaviour
{
	AudioSource audioPlayer;
	Image image;

	public UIManager uiManager;

	private void OnEnable()
	{
		audioPlayer = FindObjectOfType<AudioSource>();
		audioPlayer.GetComponent<AudioManager>().SendMute += Mute;
		image = GetComponent<Image>();
		Mute(audioPlayer.mute);
	}

	private void OnDisable()
	{
		audioPlayer.GetComponent<AudioManager>().SendMute -= Mute;
	}

	public void Mute()
	{
		audioPlayer.GetComponent<AudioManager>().Mute();
	}

	public void Mute(bool muted)
	{
		if (muted)
			image.sprite = uiManager.mutedSprite;
		else
			image.sprite = uiManager.unMutedSprite;
	}
}
