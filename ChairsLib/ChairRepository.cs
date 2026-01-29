using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChairsLib
{
    public class ChairRepository
    {
        private List<Chair> _chairs = new List<Chair>();
        private int nextId = 1;
        public ChairRepository() 
        {
            _chairs.Add(new Chair(nextId++, "The Egg", 100, true));
            _chairs.Add(new Chair(nextId++, "Swan", 120, true));
            _chairs.Add(new Chair(nextId++, "E27", 130, true));
        }


        public List<Chair> GetAll()
        {
            return new List<Chair>(_chairs);
        }

        public Chair? GetById(int id)
        {
            return _chairs.FirstOrDefault(c => c.Id == id);
        }

        public Chair Add(Chair chair)
        {
            chair.Id = nextId++;
            _chairs.Add(chair);
            return chair;
        }

        public Chair? Remove(int id)
        {
            Chair? chair = GetById(id);
            if (chair == null)
            {
                return null;
            }
                _chairs.Remove(chair);
            return chair;
        }
    }
}
