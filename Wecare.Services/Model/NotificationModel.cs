using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wecare.Services.Model
{
    public class NotificationModel : BaseModel
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public UserModel User { get; set; }
    }
}
