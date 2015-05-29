using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VTL
{
    class SumOfSinesDisturbance
    {
        public struct WaveformComponent
        {
            public float amplitude;
            public float frequency;
            public float phase;

            public WaveformComponent(float amplitude, float frequency, float phase)
            {
                this.amplitude = amplitude;
                this.frequency = frequency;
                this.phase = phase;
            }
            public override string ToString()
            {
                return string.Format("{0:0.00000}, {1:0.00000}, {2:0.00000}", 
                                     amplitude, frequency, phase);
            }
        }

        List<WaveformComponent> waveformComponents;
        float stdev;
        float bias;

        public SumOfSinesDisturbance(List<WaveformComponent> waveformComponents, float stdev = 0.5f, float bias = 0.0f)
        {
            this.waveformComponents = waveformComponents;
            this.stdev = stdev;
            this.bias = bias;
        }

        public float Sample(float t)
        {
            float foo = 0;

            foreach (var w in waveformComponents)
            {
                foo += w.amplitude * Mathf.Sin(t * 2.0f * Mathf.PI * w.phase * w.frequency);
            }

            foo += bias;
            foo += Normal(stdev);

            return foo;
        }

        float Normal(float stdDev)
        {
            // http://stackoverflow.com/questions/218060/random-gaussian-variables

            if (stdDev == 0.0)
            {
                return 0.0f;
            }
            float u1 = Random.Range(0f, 1f);
            float u2 = Random.Range(0f, 1f);
            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                                  Mathf.Sin(2.0f * Mathf.PI * u2);
            return stdDev * randStdNormal;
        }
    }
}
