using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client.TokenHandler
{
    public interface ITokenHandler
    {
        string Get();
        bool Save(string token);
        bool Clear();
    }
}
