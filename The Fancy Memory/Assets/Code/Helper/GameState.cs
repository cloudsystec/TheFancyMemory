using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Code.Helper
{
    public static class GameState
    {
        public static string UserName;

        public static Sprite BackSprite;
        public static List<Sprite> FrontSprite;
        public static Sprite LevelBlockedSprite;
        public static GameObject LevelPrefab;
        public static GameObject CardPrefab;
        public static int CurrentLevel = 1;
        public static int MaxCards = 0;
        public static int LevelMaxCombinations = 0;

        /***
            * sistema simples de Score, com sistema regressivo de punição
            * este sistema de pontuação foi formulado para que quanto mais cedo o player errar mais pontos serão descontados,
            * erros mais tardios descontam menos pontos, sendo cumulativos, e não por "erro"
            */
        public static int LevelCurrentScore = 0;
        public static int LevelMaxScore = 0;
        public static int LevelStepScore = 50;

        /***
            * leveis ordenado na lista, 1 lina = 1 level, no caso de mais leveis aumentar a lista
            * primeiro indice, é o numero da fase
            * segundo indice, é o indice numero de cartas no level, deve ser PAR
            * terceiro indice, é o indice numeros de animais no level, tendo como limite a metade do primeiro indice
            * Para adicionar um level basta adicionar uma linha neste dicionario
            */
        public static Dictionary<int, Dictionary<int, int>> Levels = new Dictionary<int, Dictionary<int, int>>
        {
            // primeira fase com 8 cartas e 4 animais, geralmente será sempre a metade, contudo pode conter menos animais, para fazer mais de 1 par de cada.
            { 1, new Dictionary<int, int> { { 8, 4 } } }
        };
        

        public static void LoadLevel(int i)
        {
            CurrentLevel = i;
        }

        public static bool IsLevelEnabled(int level)
        {
            return level <= Levels.Count;
        }

        public static KeyValuePair<int, int> GetCurrentLevel()
        {
            return Levels[CurrentLevel].FirstOrDefault();
        }
    }
}
