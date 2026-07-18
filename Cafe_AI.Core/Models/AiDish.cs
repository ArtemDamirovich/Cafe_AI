using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_AI.Core.Models
{
    public class AiDish
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Recipe { get; set; } = "";
        public List<string> Ingredients { get; set; } = new();
        public List<string>? Allergens { get; set; }
        public int Calories { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
        public int CookingTimeMinutes { get; set; }
        public string? Difficulty { get; set; }
    }
}
