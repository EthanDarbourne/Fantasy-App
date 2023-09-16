using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Data
{
    public abstract class Info
    {

        public abstract string Stringify();

        public abstract void LoadFromLine(string line);

    }
}
