using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Badges
{
    class Badge
    {
        public string BadgeID { get; set; }
        public List<string> ValidDoors { get; set; }

        public Badge() { }
        public Badge(string idnum, List<string> doors)
        {
            BadgeID = idnum;
            ValidDoors = doors;
        }

        public string DoorsAsString()
        {
            string doors = "";
            for (int i = 0; i < ValidDoors.Count; i++)
            {
                if (i + 1 == ValidDoors.Count) { doors += $"{ValidDoors[i]}"; }
                else { doors += ValidDoors[i] + ", "; }
            }
            return doors;
        }
    }

    class BadgeRepository
    {
        private readonly Dictionary<string, Badge> _badges = new Dictionary<string, Badge>();
              
        public void Add(Badge badge)
        {
            //if (badge != null)
            //{
                _badges.Add(badge.BadgeID, badge);
            //    return true;
            //}
            //else { return false;  }
        }

        public Dictionary<string, Badge> GetBadges()
        {
            //List<string> somestuff = _badges.AsEnumerable();
            return _badges;
        }
        /*
        public List<Badge> GetBadgeList()
        {
            List<Badge> results = new List<Badge>();
            //results = _badges.;
            return _badges;
        }*/

        public int Count()
        {
            return _badges.Count;
        }

        public Badge GetBadge(string idnum)
        {
            _badges.TryGetValue(idnum, out Badge badge);
            return badge;
        }

        public bool UpdateBadge(string idnum, Badge newBadge)
        {

            Badge target = GetBadge(idnum);

            if (target != null)
            {
                _badges.Remove(idnum);
                _badges.Add(newBadge.BadgeID, newBadge);
                return true;
            }
            else { return false; }
        }

        public bool DeleteBadge(string idnum)
        {
            Badge target = GetBadge(idnum);
                
            if (target != null) { _badges.Remove(idnum); return true; }
            return false;
        }
    }
}
