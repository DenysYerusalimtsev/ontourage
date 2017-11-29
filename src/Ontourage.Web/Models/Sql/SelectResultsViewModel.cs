using System.Collections.Generic;
using Ontourage.Core.Entities.Sql;

namespace Ontourage.Web.Models.Sql
{
    public class SelectResultsViewModel
    {
        public SelectResultsViewModel(SelectResult selectResult)
        {
            Result = selectResult.Result;
        }

        public List<List<string>> Result { get; }
    }
}
