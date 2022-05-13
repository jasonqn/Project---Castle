using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    //calls animation when scene transition occurs
    public Animator anim;

    //string variable to hold previous scene - set to "City" as default in case errors encountered
    string previousScene = "City";

    //reference to DDOL script
    DDOL ddol;

    private void Start()
    {
        ddol = GetComponentInParent<DDOL>();
    }

    public IEnumerator BattleScene()
    {
        //saves the previous scene string variable to be current active scene
        previousScene = SceneManager.GetActiveScene().name;
        //tells DDOL we've entered combat
        ddol.InCombat();
        //triggers fade to black animation
        anim.SetBool("FadeToBlack", true);
        //Coroutine called to wait execution
        yield return new WaitForSeconds(2f);
        //transitions out from black
        anim.SetBool("FadeToBlack", false);
        //loads the scene "BattleScene"
        SceneManager.LoadScene("BattleScene");
        //triggers animation
        anim.SetTrigger("FadeFromBlack");
    }

    public IEnumerator PreviousScene()
    {
        //triggers fade to black animation
        anim.SetBool("FadeToBlack", true);
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(previousScene);
        //transitions out from black
        anim.SetBool("FadeToBlack", false);
        //triggers animation
        anim.SetTrigger("FadeFromBlack");
    }    
    
    public IEnumerator NextScene(int nextScene)
    {
        anim.SetBool("FadeToBlack", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextScene);
        //transitions out from black
        anim.SetBool("FadeToBlack", false);
        //triggers animation
        anim.SetTrigger("FadeFromBlack");
    }
}
