using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float delayBeforeSceneChange = 1f;
    [SerializeField]AudioClip successAudio;
    [SerializeField]AudioClip gameOverAudio;
    
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem gameOverParticles;

    AudioSource myAudioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) return;
        switch (other.gameObject.tag)
        {
            case "Finish":
                //End game
                Debug.Log("End game");
                Success();                
                break;
            case "Friendly":
                Debug.Log("Hit a friendly");
                break;
            case "Fuel":
                Debug.Log("Picked up fuel");
                break;
            default:
                Debug.Log("lose hp");
                GameOver();
                break;
        }
    }

    void Success()
    {
        isTransitioning = true;
        successParticles.Play();
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(successAudio);
        
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextScene),delayBeforeSceneChange);
    }

    void GameOver()
    {
        isTransitioning = true;
        gameOverParticles.Play();
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(gameOverAudio);

        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadScene),delayBeforeSceneChange);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneBuildIndex: currentSceneIndex);
    }

    void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(sceneBuildIndex: nextScene );

    }


}
