using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ReferenceFiller : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private Volume volume;
    [SerializeField] private Button watchedAdButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Button pausedMenuContinuePlayingButton;


    private void Start()
    {
        //GameManager references and values
        GameManager.Instance.youDiedMenu = deathMenu;
        GameManager.Instance.pausedMenu = pausedMenu;
        GameManager.Instance.scoreText = scoreText;
        GameManager.Instance.highScoreText = highScoreText;
        volume.profile.TryGet(out GameManager.Instance.depthOfField);
        watchedAdButton.onClick.AddListener(AdManager.Instance.ShowRewardedAd);
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame("Main"));

        //AudioManager references and values
        musicVolumeSlider.value = AudioManager.Instance.musicVolume;
        AudioManager.Instance.musicVolumeSlider = musicVolumeSlider;
        pausedMenuContinuePlayingButton.onClick.AddListener(GameManager.Instance.StopOrStartGame);
        pausedMenuContinuePlayingButton.onClick.AddListener(() => GameManager.Instance.pausedMenu.SetActive(false));
    }
}
