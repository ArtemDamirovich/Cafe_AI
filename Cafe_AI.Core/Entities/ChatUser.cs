using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cafe_AI.Core.Entities
{
    public class ChatUser
    {
        public string Name { get; set; } = "";
        public string Role { get; set; } = "";
        public bool IsOnline { get; set; }
        public string ConnectionId { get; set; } = "";
    }
}
