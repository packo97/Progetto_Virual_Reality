using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private GameObject _imageLivesContenitor;

    [SerializeField] private Slider _stickTimeSlider;
    
    private int lives;
    
    void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_DIE, OnPlayerDie);
        Messenger.AddListener(GameEvent.ON_STICK_TIME, OnStickTime);
        Messenger.AddListener(GameEvent.OFF_STICK_TIME, OffStickTime);
        Messenger.AddListener(GameEvent.DECREASE_STICK_TIME, DecreaseStickTime);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_DIE, OnPlayerDie);
        Messenger.RemoveListener(GameEvent.ON_STICK_TIME, OnStickTime);
        Messenger.RemoveListener(GameEvent.OFF_STICK_TIME, OffStickTime);
        Messenger.RemoveListener(GameEvent.DECREASE_STICK_TIME, DecreaseStickTime);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu.Close();
        _stickTimeSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameEvent.isPaused)
                _pauseMenu.Open();
            else if (GameEvent.isPaused)
                _pauseMenu.Close();
        }
    }

    private void OnPlayerDie()
    { 
        /*
         * Prendo i figli del gameobject che contiene le immagini delle vite del player.
         * Prendo l'indice dell'ultima posizione dell'array dei figli.
         * Disattivo l'ultimo figlio.
         * 
         */
        Image[] livesImages = _imageLivesContenitor.GetComponentsInChildren<Image>() ;
        int lastIndex = livesImages.Length - 1;
        livesImages[lastIndex].gameObject.SetActive(false);
    }

    private void OnStickTime()
    {
        /*
         * Attivo lo slider dello stick time.
         * Imposto il valore dello slider a 80.
         * 
         */
        _stickTimeSlider.gameObject.SetActive(true);
        _stickTimeSlider.value = 80;
    }

    private void OffStickTime()
    {
        /*
         * Disattivo lo slider per lo stick time.
         * 
         */
        _stickTimeSlider.gameObject.SetActive(false);
    }

    private void DecreaseStickTime()
    {
        /*
         * Diminuisco il valore dello slider di 1.
         * 
         */
        _stickTimeSlider.value -= 1;
    }
 
}