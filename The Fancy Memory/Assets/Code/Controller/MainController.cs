using Assets.Code.Helper;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Controller
{
    public class MainController : MonoBehaviour
    {
        public Button PlayButton;
        public InputField NickInput;
        public GridLayoutGroup Grid;

        public void Start ()
        {
            Grid.cellSize = new Vector2(Screen.width / 8, (Screen.height / 12));

            GameState.CurrentLevel = 0;
            PlayButton.onClick.AddListener(LevelSelect);

            if (!string.IsNullOrEmpty(GameState.UserName))
            {
                NickInput.text = GameState.UserName;
                PlayButton.onClick.Invoke();
            }
        }

        void LevelSelect()
        {
            GameState.UserName = NickInput.text;
        }

        public void Update ()
        {
            PlayButton.interactable = NickInput.text.Length > 5;
        }
    }
}
