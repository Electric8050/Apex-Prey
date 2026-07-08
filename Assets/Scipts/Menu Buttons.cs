using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

   public class menuButtons : MonoBehaviour
   {
       public void StartGame()
       {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
       }


       public void Quit()
       {
           Application.Quit();

#if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
#endif
       }    
        public void Credits()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        public void Back()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }

        public void Game()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        }
}
