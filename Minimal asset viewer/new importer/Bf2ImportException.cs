using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimal_asset_viewer.new_importer
{
    public class Bf2ImportException : Exception
    {
        public Bf2ImportException(string message) : base(message)
        {
        }
    }
}
