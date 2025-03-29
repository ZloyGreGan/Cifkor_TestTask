using System;

[Serializable]
public class DogBreedData
{
    public DogBreed[] data;
}

[Serializable]
public class DogBreedWrapper
{
    public DogBreed data;
}

[Serializable]
public class DogBreed
{
    public string id;
    public BreedAttributes attributes;
}

[Serializable]
public class BreedAttributes
{
    public string name;
    public string description;
}