using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Helper
{
    //esta classe é responsável basicamente por transportar os objetos da cena inicial para a memoria "static", 
    public class GameHelper : MonoBehaviour
    {
        public Sprite BackSprite;
        public List<Sprite> FrontSprite;
        public Sprite LevelBlockedSprite;
        public GameObject LevelPrefab;
        public GameObject CardPrefab;

        public void Start()
        {
            GameState.FrontSprite = FrontSprite;
            GameState.BackSprite = BackSprite;
            GameState.LevelBlockedSprite = LevelBlockedSprite;
            GameState.LevelPrefab = LevelPrefab;
            GameState.CardPrefab = CardPrefab;
        }
    }

    public static class GHelper
    {
        private static readonly System.Random Rng = new System.Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<T> Duplicate<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 0)
            {
                n--;
                list.Add(list[n]);
            }

            return list.ToList();
        }
    }
}
