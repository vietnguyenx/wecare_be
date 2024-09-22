using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.API.SearchModel
{
    public class MenuSearchRequest
    {
        public string MenuName { get; set; }
        public SuitableFor? SuitableFor { get; set; }
    }
}
