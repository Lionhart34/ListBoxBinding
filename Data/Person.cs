using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListBoxBinding.Data
{
    public class Person
    {
        #region Constructors
        public Person() { }
        public Person(string name)
        {
            Name=name;
        }
        #endregion

        #region Data
        public string Name { get; set; }
        public Skill MasterSkill{ get; set; }
        #endregion
    }
}
