using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Fonte de áudio para reproduzir os sons
    [SerializeField] private AudioClip[] ballCollisionSounds; // Lista de sons para colisão com outras bolas (Tag: "Ball")
    [SerializeField] private AudioClip[] wallCollisionSounds; // Lista de sons para colisão com a parede (Tag: "Wall")

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ball")) {            // Toca um som da lista de colisão com outras bolas
            PlayRandomSound(ballCollisionSounds);
            return;
        }
        if (collision.gameObject.CompareTag("Wall")) { // Toca um som da lista de colisão com a parede
            PlayRandomSound(wallCollisionSounds);
            return;
        }
    }

    private void PlayRandomSound(AudioClip[] soundList)    {
        // Verifica se a lista de sons não está vazia
        if (soundList.Length > 0) {
            // Escolhe um som aleatório da lista e toca
            AudioClip randomSound = soundList[Random.Range(0, soundList.Length)];
            audioSource.PlayOneShot(randomSound);
        }
    }
}
