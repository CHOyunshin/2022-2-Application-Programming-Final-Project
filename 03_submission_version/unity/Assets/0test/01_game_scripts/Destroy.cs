using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Destroy : MonoBehaviour
{

    [SerializeField] float HP = 100;
    public TMP_Text lifeText;
    public Slider hpBar;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("miss");
        if (other.CompareTag("Note"))
        {
            Note hh = other.GetComponent<Note>();
            if (!hh.hit)
            {
                HP -= 10;
                if(HP <= 0)
                {
                    SceneManager.LoadScene("start_scene2", LoadSceneMode.Single);
                }
                else
                {
                    lifeText.text = "HP: " + HP;
                    hpBar.value = HP;

                }
            }
            Destroy(other.gameObject);
            
        }
    }

}
