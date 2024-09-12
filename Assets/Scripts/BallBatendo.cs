using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBatendo : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Fonte de áudio para reproduzir os sons
    [SerializeField] private AudioClip[] ballCollisionSounds; // Lista de sons para colisão com outras bolas (Tag: "Ball")
    [SerializeField] private AudioClip[] wallCollisionSounds; // Lista de sons para colisão com a parede (Tag: "Wall")

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Toca um som da lista de colisão com outras bolas
            PlayRandomSound(ballCollisionSounds);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Toca um som da lista de colisão com a parede
            PlayRandomSound(wallCollisionSounds);
        }
    }

    private void PlayRandomSound(AudioClip[] soundList)
    {
        // Verifica se a lista de sons não está vazia
        if (soundList.Length > 0)
        {
            // Escolhe um som aleatório da lista
            AudioClip randomSound = soundList[Random.Range(0, soundList.Length)];
            // Toca o som escolhido
            audioSource.PlayOneShot(randomSound);
        }
    }
}
