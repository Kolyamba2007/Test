using (var fileStream = new FileStream("data.dat", FileMode.OpenOrCreate))
{
    ListRand ListRand = new();
    
    ListRand.FillListRand(16);
    ListRand.Serialize(fileStream);

    //ListRand.Deserialize(fileStream);
}