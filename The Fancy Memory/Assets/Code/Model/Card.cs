using System;
using System.Collections;
using Assets.Code.Controller;
using Assets.Code.Helper;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Model
{
    public class Card : MonoBehaviour
    {
        public CardType Type;
        public Sprite Sprite;
        public bool ShowOnStart = true;
        private bool _firstShow = true;
        private bool _staticCard = false;

        public void Awake()
        {
            if (ShowOnStart)
            {
                ShowCard();
                StartCoroutine(Wait2ShowBack());
            }
        }

        public void ShowCard()
        {
            var img = transform.GetChild(1).transform.GetComponent<Image>();
            img.sprite = Sprite;

            img = transform.GetChild(0).transform.GetComponent<Image>();
            img.color = new Color(94 / 255f, 94 / 255f, 94 / 255f, 75 / 255f);
        }

        public void SuccessProcedure()
        {
            var img = transform.GetComponent<Image>();
            img.color = new Color(43 / 255f, 248 / 255f, 0 / 255f, 255 / 255f);

            _staticCard = true;
        }

        public void ShowBack()
        {
            var img = transform.GetChild(1).transform.GetComponent<Image>();
            img.sprite = GameState.BackSprite;

            img = transform.GetChild(0).transform.GetComponent<Image>();
            img.color = new Color(255 / 255f, 211 / 255f, 26 / 255f, 255 / 255f);
        }

        private void Click()
        {
            if(!_staticCard) Camera.main.GetComponent<GameController>().CardClicked(transform.GetComponent<Card>());
        }

        public IEnumerator Wait2ShowBack(int seconds = 2,Action action = null)
        {
            yield return new WaitForSeconds(seconds);
            ShowBack();
            if (_firstShow)
            {
                transform.GetComponent<Button>().onClick.AddListener(Click);
                _firstShow = false;
            }

            if (action != null)
                action();
        }
    }
}
