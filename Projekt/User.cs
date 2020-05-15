using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class User
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int publicid;

        public int PublicId
        {
            get { return publicid; }
            set { publicid = value; }
        }
        private int group;

        public int Group
        {
            get { return group; }
            set { group = value; }
        }
        private int privateid;

        public int PrivateId
        {
            get { return privateid; }
            set { privateid = value; }
        }
        private int shoot;
        public int Shoot
        {
            get { return shoot; }
            set { shoot = value; }
        }

    }
}
