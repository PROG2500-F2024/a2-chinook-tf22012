using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2500_A2_Chinook.Models
{
    public partial class Track
    {
        public string FormattedComposer 
        {
            get { return "Composer: " + (this.Composer ?? "None found."); }
        }

        public string FormattedLength
        {
            get { return "Length: " + this.Milliseconds.ToString("N0") + " ms"; }
        }
        public string FormattedBytes
        {
            get { return "Size: " + this.Bytes?.ToString("N0") + " bytes"; }
        }
        public string FormattedPrice
        {
            get { return "Price: $" + this.UnitPrice.ToString("N"); }
        }
    }
}
