using Assets.Code.Helper;
using UnityEngine;

namespace Assets.Code.Model
{
    public class LevelSelectGrid : MonoBehaviour
    {
        public int MinLevel = 5;

        public void Start()
        {
            for (int i = 1; i <= MinLevel; i++)
            {
                GameState.LevelPrefab.GetComponent<LevelSelect>().Level = i;
                var p = GameState.LevelPrefab;
                Instantiate(p, this.transform);
            }
        }
	
        // Update is called once per frame
        void Update () {
		
        }
    }
}
