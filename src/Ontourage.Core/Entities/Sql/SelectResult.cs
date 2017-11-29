using System.Collections.Generic;

namespace Ontourage.Core.Entities.Sql
{
    public class SelectResult
    {
        public SelectResult(List<List<string>> result)
        {
            Result = result;
        }

        public List<List<string>> Result { get; }
    }
}
