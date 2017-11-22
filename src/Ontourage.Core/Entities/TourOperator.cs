namespace Ontourage.Core.Entities
{
   public class TourOperator
    {
        public int Id { get; set; }

        public string TourOperatorName {get; set;}

        public TourOperator(int id, string tourOperatorName)
        {
            Id = id;
            TourOperatorName = tourOperatorName;
        }
    }
}
