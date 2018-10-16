using Assets.Code.Helper;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Model
{
    public class LevelSelect : MonoBehaviour
    {
        public int Level = 0;
        private bool _enabled;
        
        public void Awake ()
        {
            _enabled = GameState.IsLevelEnabled(Level);
            ChangeActiveState();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void ChangeActiveState()
        {
            if (_enabled)
            {
                transform.GetChild(0).gameObject.GetComponent<Text>().text = Level.ToString();
                GetComponent<Button>().onClick.AddListener(SelectLevel);
            }

            transform.GetChild(0).gameObject.SetActive(_enabled);
            transform.GetChild(1).gameObject.SetActive(!_enabled);
        }

        public void SelectLevel()
        {
            GameState.LoadLevel(Level);
            SceneManager.LoadScene("Game");
        }
    }
}
