using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class BuildLink
    {
        public static string BuildNewsURL(string title, string CatParent_ID, string Cat_ID, string _intNewsID)
        {
            string NewsURL = "/{0}p{1}c{2}/{3}.htm";
            return string.Format(NewsURL, _intNewsID, CatParent_ID, Cat_ID,Unicode.UnicodeToKoDauAndGach(title).Replace("\"", "").Replace("'", ""));
        }
    }
}
