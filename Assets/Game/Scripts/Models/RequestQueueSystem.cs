using System;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RequestQueueSystem : IDisposable
{
    private Queue<Action> _requestQueue = new();
    private bool _isProcessing;
    private UnityWebRequest _currentRequest;

    public void AddRequest(Action request)
    {
        _requestQueue.Enqueue(request);
        ProcessQueue();
    }

    private void ProcessQueue()
    {
        if (_isProcessing || _requestQueue.Count == 0) return;
        
        _isProcessing = true;
        
        Action nextRequest = _requestQueue.Dequeue();
        nextRequest.Invoke();
    }

    public void CompleteRequest()
    {
        _isProcessing = false;
        
        if (_currentRequest != null)
        {
            _currentRequest.Dispose();
            _currentRequest = null;
        }
        
        ProcessQueue();
    }

    private void CancelCurrentRequest()
    {
        if (_currentRequest != null && !_currentRequest.isDone)
        {
            _currentRequest.Abort();
            _currentRequest.Dispose();
            _currentRequest = null;
        }
        
        _isProcessing = false;
        ProcessQueue();
    }

    public void ClearQueue()
    {
        _requestQueue.Clear();
        CancelCurrentRequest();
    }

    public void Dispose()
    {
        ClearQueue();
    }

    public void SetCurrentRequest(UnityWebRequest request)
    {
        if (_currentRequest != null && !_currentRequest.isDone)
        {
            _currentRequest.Dispose();
        }
        
        _currentRequest = request;
    }
}