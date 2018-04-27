using ListBoxBinding.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListBoxBinding.ViewModel
{
    public class ManageSkillsViewModel : BaseViewModel
    {

        private ObservableCollection<Person> _persons = null;
        public ObservableCollection<Person> Persons
        {
            get
            {
                if (_persons == null)
                    _persons = new ObservableCollection<Person>(
                        from Pers in Global.Context.Persons
                        select Pers);
                return _persons;
            }
            set
            {
                _persons = value;
            }
        }

        private  ObservableCollection<Skill> _skills;
        public ObservableCollection<Skill> Skills
        {
            get
            {
                if (_skills == null)
                    _skills = new ObservableCollection<Skill>(
                        from skill in Global.Context.Skills
                        select skill);
                return _skills;

            }
            set
            {
                _skills = value;
            }

        }
    }
}
