using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] Button onExitButton;
    [SerializeField] GameObject SkillPanel;
    [SerializeField] Image hpImage;
    [SerializeField] Image exImage;

    private void Awake()
    {
        SkillPanel.SetActive(false);
        onExitButton.onClick.AddListener(() => { SkillPanel.SetActive(false); });
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SkillPanel.SetActive(true);
        }

        exImage.fillAmount = (float)PlayerManager.Instance.CurrentMaxExp / (float)PlayerManager.MaxExp;
    }

}
