using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Helper;
using Assets.Code.Model;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Controller
{
    public class GameController : MonoBehaviour
    {
        public GridLayoutGroup Grid;
        private Card _recentCard = null;
        private Card _currentCard = null;
        private int _levelErrorStep = 0;
        public GameObject Score;
        public GameObject Win;

        /// <summary>
        /// Este método deveria estar dentro de uma camada de services, para esta unica chamada coloquei aqui
        /// </summary>
        /// <returns></returns>
        IEnumerator SendScore()
        {
            var formData =
                new List<IMultipartFormSection>
                {
                    new MultipartFormDataSection("Score="+GameState.LevelCurrentScore+"&UserName="+GameState.UserName)
                };

            UnityWebRequest www = UnityWebRequest.Post("https://us-central1-huddle-team.cloudfunctions.net/api/memory/daniel.espindola.l@hotmail.com", formData);
            yield return www.SendWebRequest();

            //if (www.isNetworkError || www.isHttpError)
            //{
            //    Debug.Log(www.error);
            //}
            //else
            //{
            //    Debug.Log("Form upload complete!");
            //}
        }

        public void Start()
        {
            Grid.cellSize = new Vector2(Screen.width / 5, (Screen.height / 8));
            Win.transform.GetChild(2).transform.GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Main");
            });
            Init();
        }

        public void Update()
        {
            Score.transform.GetComponent<Text>().text = GameState.LevelCurrentScore.ToString();
        }

        private void Init()
        {
            var lvl = GameState.GetCurrentLevel();
            GameState.LevelMaxCombinations = lvl.Key / 2;
            GameState.LevelCurrentScore = 0;

            var animals = GameState.FrontSprite.Take(lvl.Value).Select((x, i) => new { Type = i + 1, Sprite = x }).ToList().Duplicate();
            animals.Shuffle();

            for (var i = 0; i < lvl.Key; i++)
            {
                var card = GameState.CardPrefab.GetComponent<Card>();
                card.Type = (CardType)animals[i].Type;
                card.Sprite = animals[i].Sprite;

                Instantiate(card.gameObject, Grid.transform);
            }

            //para cada combinação temos "N" pontos, sistema simples de pontuação
            GameState.LevelMaxScore = GameState.LevelMaxCombinations * GameState.LevelStepScore;
        }

        //evento disparado pela classe Card
        private bool _clickedAlready = false;
        public void CardClicked(Card card)
        {
            //previne cliques não desejados
            if (_clickedAlready) return;

            _currentCard = card;
            _currentCard.ShowCard();

            //primeira carta
            if (_recentCard == null) _recentCard = _currentCard;

            //segunda carta
            else
            {
                _clickedAlready = true;
                if (_recentCard.Type == _currentCard.Type)
                    RigthCombination();
                else
                    WrongCombination();
            }

            //voce venceu
            if (GameState.LevelMaxCombinations == 0)
            {
                WinProcedure();
            }
        }

        private void WinProcedure()
        {
            Win.transform.GetChild(0).transform.GetComponent<Text>().text = GameState.UserName;
            Win.transform.GetChild(1).transform.GetComponent<Text>().text = GameState.LevelCurrentScore + "/" + GameState.LevelMaxScore;
            Win.SetActive(true);

            StartCoroutine(SendScore());
        }

        private void RigthCombination()
        {
            _currentCard.SuccessProcedure();
            _recentCard.SuccessProcedure();

            //Prevenir divisão por zero
            GameState.LevelCurrentScore += GameState.LevelStepScore / (_levelErrorStep + 1);

            GameState.LevelMaxCombinations--;
            ClearRotine();
        }

        private void WrongCombination()
        {
            StartCoroutine(_currentCard.Wait2ShowBack(1, ClearRotine));
            StartCoroutine(_recentCard.Wait2ShowBack(1, ClearRotine));
            _levelErrorStep++;
        }

        public void ClearRotine()
        {
            _clickedAlready = false;
            _recentCard = null;
            _currentCard = null;
        }
    }
}
