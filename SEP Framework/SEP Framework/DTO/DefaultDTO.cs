using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP_Framework.DTO
{
    public class DefaultDTO
    {
        public Dictionary<string, string> data;
        public DefaultDTO()
        {
            data = new Dictionary<string, string>();
        }

        public DefaultDTO(Dictionary<string,string> _data)
        {
            data = _data;
        }
    }
}
