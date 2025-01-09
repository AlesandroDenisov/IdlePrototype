using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FPSCounter
{
    public int AvgFPS { get; private set; }
    public int MaxFPS { get; private set; }

    public int MinFPS { get; private set; }


    private int _frameRange;

    private int[] _fpsBuffer;

    private int _fpsBufferIndex;

    public FPSCounter(int frameRange)
    {
        _frameRange = frameRange;
        CreateBuffer();
    }
    public void Update()
    {
        UpdateBuffer();
        CalculateFPS();
    }

    private void CreateBuffer()
    { 
        if (_frameRange <= 0 ) 
        {
            _frameRange = 1;
        }

        _fpsBuffer = new int[_frameRange];
        _fpsBufferIndex = 0;
    }

    private void UpdateBuffer()
    {
        _fpsBuffer[_fpsBufferIndex++] = Mathf.Clamp((int)(1f / Time.unscaledDeltaTime), 0, 99);
        
        if (_fpsBufferIndex >= _fpsBuffer.Length )
        {
            _fpsBufferIndex = 0;
        }
    }

    private void CalculateFPS()
    { 
        int sum = 0;
        int min = int.MaxValue;
        int max = 0;

        for (int i = 0; i < _fpsBuffer.Length; i++)
        {
            int fps = _fpsBuffer[i];
            sum += fps;

            if (fps > max )
            {
                max = fps;
            }

            if (fps < min)
            { 
                min = fps;
            }
        }

        AvgFPS = sum / _fpsBuffer.Length;
        MinFPS = min;
        MaxFPS = max;
    }
}
