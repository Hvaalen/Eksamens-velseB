using System;

namespace ChairsLib
{
    public class Chair
    {
        private string _model;
        private int _maxWeight;

        public int Id { get; set; }

        public string Model
        {
            get => _model;
            set
            {
                ValidateModel(value); // Kalder validate metoden
                _model = value;
            }
        }

        public int MaxWeight
        {
            get => _maxWeight;
            set
            {
                ValidateMaxWeight(value); 
                _maxWeight = value;
            }
        }

        public bool HasPillow { get; set; }

        public Chair() { }

        public Chair(int id, string model, int maxWeight, bool hasPillow)
        {
            Id = id;
            Model = model;
            MaxWeight = maxWeight; 
            HasPillow = hasPillow;
        }

        public void ValidateModel(string model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Model cannot be null");
            }
            if (model.Length < 2)
            {
                throw new ArgumentException("Model text must be at least 2 characters long.");
            }
        }

        public void ValidateMaxWeight(int maxWeight)
        {
            if (maxWeight < 50)
            {
                throw new ArgumentOutOfRangeException(nameof(maxWeight), "MaxWeight must be at least 50");
            }
        }

        public override string ToString()
        {
            return $"Chair [Id={Id}, Model={Model}, MaxWeight={MaxWeight}, HasPillow={HasPillow}]";
        }
    }
}