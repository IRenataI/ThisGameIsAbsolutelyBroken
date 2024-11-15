using System.Collections;
using UnityEngine;
using UniGLTF;

namespace VRM
{
    public class TalkingAnimator : MonoBehaviour
    {
        private VRMBlendShapeProxy m_blendShapes;

        [SerializeField] public float OpenTime = 0.1f;
        [SerializeField] public float CloseTime = 0.1f;
        [SerializeField] public float SmileDuration = 1.0f;

        private Coroutine m_coroutine;
        private Coroutine smileCoroutine;
        public bool IsTalking = false;

        private IEnumerator TalkRoutine()
        {
            while (true)
            {
                if (IsTalking)
                {
                    float value = 0.0f;
                    float openSpeed = 1.0f / OpenTime;
                    while (value < 1.0f)
                    {
                        value += Time.deltaTime * openSpeed;
                        m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), value);
                        yield return null;
                    }

                    m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 1.0f);

                    value = 1.0f;
                    float closeSpeed = 1.0f / CloseTime;
                    while (value > 0.0f)
                    {
                        value -= Time.deltaTime * closeSpeed;
                        m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), value);
                        yield return null;
                    }

                    m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.0f);
                }
                else
                {
                    m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), 0.0f);
                    yield return null;
                }
            }
        }

        private void OnEnable()
        {
            m_blendShapes = this.GetComponent<VRMBlendShapeProxy>();
            m_coroutine = StartCoroutine(TalkRoutine());
        }

        private void OnDisable()
        {
            if (m_coroutine != null)
            {
                StopCoroutine(m_coroutine);
                m_coroutine = null;
            }
        }
        public void ToggleTalking(bool enable)
        {
            IsTalking = enable;
        }
        public void StartSmile()
        {
            if (smileCoroutine != null)
            {
                StopCoroutine(smileCoroutine);
            }
            smileCoroutine = StartCoroutine(SmileRoutine());
        }

        private IEnumerator SmileRoutine()
        {
            m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy), 1.0f);

            yield return new WaitForSeconds(SmileDuration);

            m_blendShapes.ImmediatelySetValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy), 0.0f);
        }
    }
}
