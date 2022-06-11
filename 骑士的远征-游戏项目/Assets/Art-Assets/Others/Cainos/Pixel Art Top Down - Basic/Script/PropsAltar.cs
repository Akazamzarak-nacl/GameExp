using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;

        private Color curColor;
        private Color targetColor;

        private static int Scene = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 1);

            //只有这一点是我自己写的，意图在使得传送门能够将主角传送到下一个场景，其余脚本内容不建议修改
            //但这个传送是无条件的
            //添加敌人的同学应当适当修改为击败boss后才能传送
            PlayerController controller = other.GetComponent<PlayerController>();
            if (controller != null) //只需要在这个if语句里添加“且boss血量==0”之类的逻辑即可
            {
                Scene = Scene % 3+1;
                Debug.Log(Scene);
                SceneManager.LoadScene(Scene);
            }

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 0);
        }

        private void Update()
        {
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            foreach (var r in runes)
            {
                r.color = curColor;
            }
        }
    }
}
