using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    Cinemachine.CinemachineVirtualCamera vCM;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        vCM = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if(isCurrentTurn)
        {
            vCM.Follow = this.gameObject.transform;
            vCM.LookAt = this.gameObject.transform;
        }
    }

    public override void TakeDamage(float damage)
    {
        transform.Find("Blood").GetComponent<ParticleSystem>().Play();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(transform.Find("Blood").GetComponent<ParticleSystem>());
            battleSystem.gameText.text = $"{unitName} has died";
            battleSystem.RemoveUnit(this);
            StartCoroutine(RestartGame());
            transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
