using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public GameObject Modal;
    public Button BtnYes;
    public Button BtnNao;

    // Use this for initialization
    void Start () {

        DontDestroyOnLoad(gameObject.transform.parent.gameObject);

		transform.GetComponent<Button>().onClick.AddListener(() => { Modal.SetActive(true); });
        BtnYes.onClick.AddListener(Application.Quit);
        BtnNao.onClick.AddListener(() => { Modal.SetActive(false); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
