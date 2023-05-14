namespace RangeVariable
{
    public struct RangeDouble : IDecrease<double>, IIncrease<double>, ISet<double>, IZeroOrNegative<double>
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double Value { get; private set; }

        /// <inheritdoc/>
        public void DecreaseBy(double deductValue)
        {
            if (IsZeroOrNegative(deductValue))
                return;

            DecreaseValueBy(deductValue);
        }

        private void DecreaseValueBy(double deductValue)
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
        public bool TryDecreaseBy(double deductValue, out double result)
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

        public bool IsDecreasableBy(double deductValue)
        {
            return (Value - deductValue) > MinValue;
        }

        /// <inheritdoc/>
        public void IncreaseBy(double additionalValue)
        {
            if (IsZeroOrNegative(additionalValue))
                return;

            IncreaseValueBy(additionalValue);
        }

        private void IncreaseValueBy(double additionalValue)
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
        public bool TryIncreasableBy(double additionalValue, out double result)
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

        public bool IsIncreasableBy(double additionalValue)
        {
            return (Value + additionalValue) < MaxValue;
        }

        public bool IsZeroOrNegative(double checkValue)
        {
            return checkValue <= 0 ? throw new System.Exception("The value must be positive.") : false;
        }

        /// <inheritdoc/>
        public void SetTo(double newValue)
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