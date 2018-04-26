using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListBoxBinding.Data
{
    public class Skill
    {
        #region Constructors
        public Skill(){}
        #endregion

        # region Data
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Color { get; set; }
        #endregion
    }
}
