//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class SkillDamageUI : MonoBehaviour
//{
//    private float moveSpeed;
//    private float alphaSpeed;
//    private float destroyTime;
//    GameObject 
//    TextMeshPro text;
//    Color alpha;

//    private float damage;

//    // Start is called before the first frame update
//    void Start()
//    {
//        moveSpeed = 2.0f;
//        alphaSpeed = 2.0f;
//        destroyTime = 2.0f;

//        _MonsterScript = GetComponent<MonsterScript>();
//        text = GetComponent<TextMeshPro>();

//        alpha = text.color;
//        text.text = damage.ToString();
//        invoke("DestroyObject", destroyTime);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // �ؽ�Ʈ ��ġ
//        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // �ؽ�Ʈ ���İ�
//        text.color = alpha;
//    }

//    private void DestroyObject()
//    {
//        Destroy(gameObject);
//    }
//}
