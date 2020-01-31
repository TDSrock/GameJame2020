using UnityEngine;

namespace SjorsGielen.Helpers.WaveForms
{
    public enum WaveForm { sin, cos, triangle, square, saw, invertedSaw, noise };

    static class WaveMathHelper
    {
        static public float EvalWave(float phase, float frequency, float amplitude, float baseStart, WaveForm waveForm)
        {
            float x = baseStart + phase * frequency;
            float y = 0f;
            x = x - Mathf.Floor(x);//ensure x is between 0 and 1
            switch (waveForm)
            {
                case WaveForm.invertedSaw:
                    y = 1f + x * -2;
                    break;
                case WaveForm.noise:
                    y = 1f - Random.value * 2;
                    break;
                case WaveForm.saw:
                    y = -1f + x * 2;
                    break;
                case WaveForm.sin:
                    y = Mathf.Sin(x * 2 * Mathf.PI);
                    break;
                case WaveForm.cos:
                    y = Mathf.Cos(x * 2 * Mathf.PI);
                    break;
                case WaveForm.square:
                    if (x < 0.5f)
                    {
                        y = 1f;
                    }
                    else
                    {
                        y = -1f;
                    }
                    break;
                case WaveForm.triangle:
                    if (x < 0.5f)
                    {
                        y = 4f * x - 1f;
                    }
                    else
                    {
                        y = -4f * x + 3f;
                    }
                    break;
                default:
                    Debug.LogError("Missing waveform in WaveMathHelper");
                    break;
            }
            return y * amplitude;
        }
    }
}
