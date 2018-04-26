using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ListBoxBinding.Data
{
    public class DataProvider
    {
        private List<Skill> _Skills;
        public List<Skill> Skills
        {
            get 
            { 
                if(_Skills == null)
                {
                    _Skills=new List<Skill>();
                    _Skills.Add(new Skill() { Color = "#4f6228", LongName = "Archi Analog", ShortName = "AA" });
                    _Skills.Add(new Skill() { Color = "#75923c", LongName = "Design Analog", ShortName = "DA" });
                    _Skills.Add(new Skill() { Color = "#9bbb59", LongName = "Layout Analog", ShortName = "LA" });
                    _Skills.Add(new Skill() { Color = "#17375d", LongName = "AD", ShortName = "AD" });
                    _Skills.Add(new Skill() { Color = "#1f497d", LongName = "Design Digital", ShortName = "DN" });
                    _Skills.Add(new Skill() { Color = "#4f81bd", LongName = "Verif. Fonct.", ShortName = "VF" });
                    _Skills.Add(new Skill() { Color = "#4bacc6", LongName = "Design For Test", ShortName = "DFT" });
                    _Skills.Add(new Skill() { Color = "#b2a1c7", LongName = "Middle End (Synthèse, TA)", ShortName = "ME" });
                    _Skills.Add(new Skill() { Color = "#8064a2", LongName = "Implémentation Physique", ShortName = "P&R" });
                    _Skills.Add(new Skill() { Color = "#953735", LongName = "SV", ShortName = "SV" });
                    _Skills.Add(new Skill() { Color = "#953735", LongName = "Caractérisation", ShortName = "Ca" });
                    _Skills.Add(new Skill() { Color = "#8064a2", LongName = "INDUS", ShortName = "Ind" });
                    _Skills.Add(new Skill() { Color = "#00b050", LongName = "Hw (Carte)", ShortName = "Hw" });
                    _Skills.Add(new Skill() { Color = "#00b050", LongName = "Software", ShortName = "Sw" });
                    _Skills.Add(new Skill() { Color = "#00b050", LongName = "Banc de Test", ShortName = "BT" });
                    _Skills.Add(new Skill() { Color = "#00b050", LongName = "VA", ShortName = "VA" });
                }
                return _Skills;
            }
            set 
            {
                _Skills = value;
            }
        }

        private List<Person> _Persons;
        public List<Person> Persons
        {
            get 
            {
                if(_Persons == null)
                {
                    _Persons=new List<Person>();
                    _Persons.Add(new Person("Nicola") { MasterSkill = Skills.First(C => C.LongName == "Hw (Carte)") });
                    _Persons.Add(new Person("Guillaume") { MasterSkill = Skills.First(C => C.LongName == "INDUS") });
                }
                return _Persons;
            }
            set 
            {
                _Persons = value;
            }
        }

    }
}
