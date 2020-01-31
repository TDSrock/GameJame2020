using UnityEngine;
using UnityEditor;
using SjorsGielen.Helpers.WaveForms;

namespace SjorsGielen.UsefullScripts
{
    [System.Serializable]
    struct Wave
    {
        public WaveForm waveForm;
        public float amplitude;
        public float frequency;
        public float baseStart;
        public float phase;
        public Vector3 moveToPossition;
        [HideInInspector]
        public float prevDistance;
        [HideInInspector]
        public float distance;

        public Wave(WaveForm waveForm, float amplitude = 1f, float frequency = 1f, float baseStart = 0, Vector3 moveToPossition = new Vector3())
        {
            this.waveForm = waveForm;
            this.amplitude = amplitude;
            this.frequency = frequency;
            this.baseStart = baseStart;
            this.phase = baseStart;
            this.moveToPossition = moveToPossition;
            prevDistance = 0;
            distance = 0;
        }
    }

    public class WaveMovingObject : MonoBehaviour
    {
        [SerializeField][HideInInspector]
        Vector3 startPosition;
        [SerializeField]
        Wave[] waves = new Wave[1];

#if UNITY_EDITOR
        [Range(10, 150)]
        public int gizmoDetail = 25;
        [Range(1,20)]
        public float gizmoLength = 2f;
#endif

        void Awake()
        {
            startPosition = this.transform.position;
            for (int i = 0; i < waves.Length; i++)
            {
                waves[i].distance = WaveMathHelper.EvalWave(waves[i].phase, waves[i].frequency, waves[i].amplitude, waves[i].baseStart, waves[i].waveForm);
                Debug.Log(waves[i].distance);
            }
        }

        void Update()
        {
            //due to how this moves objets, it is not compatible with other movement scripts.
            this.transform.position = startPosition + CalculateChangeInPossition(waves, Time.deltaTime);
        }

        private Vector3 CalculateChangeInPossition(Wave[] waves, float deltaTime)
        {
            var currentChangeInPossition = Vector3.zero;
            for (int i = 0; i < waves.Length; i++)
            {
                waves[i].prevDistance = waves[i].distance;
                waves[i].phase += deltaTime;
                waves[i].distance = WaveMathHelper.EvalWave(waves[i].phase, waves[i].frequency, waves[i].amplitude, waves[i].baseStart, waves[i].waveForm);
                currentChangeInPossition += Vector3.LerpUnclamped(Vector3.zero, waves[i].moveToPossition, waves[i].distance);
            }
            return currentChangeInPossition;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (!EditorApplication.isPlaying)
                startPosition = this.transform.position;

            Gizmos.color = Color.cyan;
            float detailPer = gizmoLength / gizmoDetail;
            float phase = 0;
            Vector3 pos1 = Vector3.negativeInfinity, pos2 = Vector3.negativeInfinity;
            for (int j = 0; j < gizmoDetail + 1; j ++)
            {
                phase = detailPer * j;
                pos2 = pos1;
                var posCalc = Vector3.zero;
                for (int i = 0; i < waves.Length; i++)
                {
                    //where we are on this wave
                    var d = WaveMathHelper.EvalWave(phase, waves[i].frequency, waves[i].amplitude, waves[i].baseStart, waves[i].waveForm);
                    //position of this wave
                    posCalc += Vector3.LerpUnclamped(Vector3.zero, waves[i].moveToPossition, d);
                }
                pos1 = posCalc;
                if (pos1 != Vector3.negativeInfinity && pos2 != Vector3.negativeInfinity)
                {
                    Gizmos.DrawLine(startPosition + pos1, startPosition + pos2);
                }
            }
        }
#endif
    }
}
