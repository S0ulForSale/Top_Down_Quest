// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SkillManag : MonoBehaviour
// {
//     public static SkillManag instance;
//     public PlayerStats[] skills;
//     //public SkillButton[] skillButtons;
//     public PlayerStats activateSkill;

//     private void Awake()
//     {
//         if(instance == null)
//         {
//             instance = this;
//         }
//         else
//         {
//             if(instance != this)
//             {
//                 Destroy(gameObject);
//             }
//         }
//         DontDestroyOnLoad(gameObject);
//     }
//     private void Start()
//     {
//         remaindPoints = totalPoints;
//         DisplayPoints();
//     }

//     public void PressUpgrade()
//     {
//         Debug.Log("PressUpgrade Work!");
//         if(remainPoints >= 1)
//         {
//             Debug.Log("Upgrade!");
//             remainPoints -=1;
//             activateSkill.isUpgrade = true;
//         }
//         else
//         {
//             Debug.Log("Not Skill Point!");
//         }
//         DisplayPoints();
//         UpSkillButton();
//     }

//     void UpSkillButton()
//     {
//         for( int i = 0; i < skills.Length; i++)
//         {
//             if(skills[i].isUpgrade)
//             {
//                 skills[i].GetComponent<Image>().color = new Vector(1, 1, 1, 1);
//                 skills[i].transform.GetChild(0).GetComponent<Image>().sprite = upSprite;
//             }
//             else
//             {
//                 skills[i].GetComponent<Image>().color = new Vector(0.15f, 9.45f, 0.45f, 1);
//                 skills[i].transform.GetChild(0).GetComponent<Image>().sprite = normSprite;
//             }
//         }
//     }

// }
