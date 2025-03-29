using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class DogBreedsModel
{
    [Inject] private RequestQueueSystem _requestQueue;
    [Inject] private DogBreedsView _dogBreedsView;

    public void RequestDogBreeds()
    {
        _dogBreedsView.ShowLoading(true);
        
        _requestQueue.AddRequest(() =>
        {
            UnityWebRequest request = UnityWebRequest.Get("https://dogapi.dog/api/v2/breeds");
            _requestQueue.SetCurrentRequest(request);
            
            request.SendWebRequest().completed += _ =>
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    DogBreedData data = JsonUtility.FromJson<DogBreedData>(request.downloadHandler.text);
                    if (data?.data != null)
                    {
                        _dogBreedsView.DisplayDogBreeds(data.data);
                    }
                    else
                    {
                        Debug.LogError("Failed to parse breeds data");
                    }
                }
                else
                {
                    Debug.LogError("Request failed: " + request.error);
                }
                
                _dogBreedsView.ShowLoading(false);
                _requestQueue.CompleteRequest();
                request.Dispose();
            };
        });
    }

    public void RequestBreedDetails(string breedId)
    {
        _requestQueue.AddRequest(() =>
        {
            UnityWebRequest request = UnityWebRequest.Get($"https://dogapi.dog/api/v2/breeds/{breedId}");
            _requestQueue.SetCurrentRequest(request);
            
            request.SendWebRequest().completed += _ =>
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    DogBreedWrapper data = JsonUtility.FromJson<DogBreedWrapper>(request.downloadHandler.text);
                    if (data?.data?.attributes != null)
                    {
                        _dogBreedsView.ShowBreedPopup(data.data.attributes.name, data.data.attributes.description);
                    }
                    else
                    {
                        Debug.LogError("Failed to parse breed details");
                    }
                }
                else
                {
                    Debug.LogError("Breed request failed: " + request.error);
                }
                
                _requestQueue.CompleteRequest();
                request.Dispose();
            };
        });
    }

    public void CancelRequests()
    {
        _requestQueue.ClearQueue();
        _dogBreedsView.ShowLoading(false);
    }
}