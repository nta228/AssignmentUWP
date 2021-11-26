using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2009M1HelloUWP.Entities
{
    public class Note
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"{Title, -100} {CreatedAt.ToString("dd-MM-yyyy HH:mm"), -50}";
        }
    }
}
