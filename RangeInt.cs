namespace RangeVariable
{
    public struct RangeInt : IDecrease<int>, IIncrease<int>, ISet<int>, IZeroOrNegative<int>
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int Value { get; private set; }

        /// <inheritdoc/>
        public void DecreaseBy(int deductValue)
        {
            if (IsZeroOrNegative(deductValue))
                return;
            
            DecreaseValueBy(deductValue);
        }

        private void DecreaseValueBy(int deductValue)
        {
            if (!IsDecreasableBy(deductValue))
            {
                ClampToMin();
                return;
            }

            Value -= deductValue;
        }

        /// <inheritdoc/>
        /// <returns>Returns TRUE if it is decreasable, else FALSE.</returns>
        public bool TryDecreaseBy(int deductValue, out int result)
        {
            result = Value;
            if (!IsDecreasableBy(deductValue))
            {
                return false;
            }

            DecreaseValueBy(deductValue);
            result = Value;
            return true;
        }

        public bool IsDecreasableBy(int deductValue)
        {
            return (Value - deductValue) > MinValue;
        }

        /// <inheritdoc/>
        public void IncreaseBy(int additionalValue)
        {
            if (IsZeroOrNegative(additionalValue))
                return;

            IncreaseValueBy(additionalValue);
        }

        private void IncreaseValueBy(int additionalValue)
        {
            if (!IsIncreasableBy(additionalValue))
            {
                ClampToMax();
                return;
            }

            Value += additionalValue;
        }

        /// <inheritdoc/>
        /// <returns>Returns TRUE if it is decreasable, else FALSE.</returns>
        public bool TryIncreasableBy(int additionalValue, out int result)
        {
            result = Value;
            if (!IsIncreasableBy(additionalValue))
            {
                return false;
            }

            IncreaseValueBy(additionalValue);
            result = Value;
            return true;
        }

        public bool IsIncreasableBy(int additionalValue)
        {
            return (Value + additionalValue) < MaxValue;
        }

        public bool IsZeroOrNegative(int checkValue)
        {
            return checkValue <= 0 ? throw new System.Exception("The value must be positive.") : false;
        }

        /// <inheritdoc/>
        public void SetTo(int newValue)
        {
            if (newValue < MinValue)
            {
                ClampToMin();
                return;
            }

            if (newValue > MaxValue)
            {
                ClampToMax();
                return;
            }

            Value = newValue;
        }

        /// <inheritdoc/>
        public void ClampToMin()
        {
            Value = MinValue;
        }

        /// <inheritdoc/>
        public void ClampToMax()
        {
            Value = MaxValue;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}