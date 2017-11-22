namespace Ontourage.Core.Entities
{
    public class FoodType
    {
        public int Id { get; }

        public string Name { get; }

        public FoodType(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
