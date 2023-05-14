namespace RangeVariable
{
    public struct RangeLong : IDecrease<long>, IIncrease<long>, ISet<long>, IZeroOrNegative<long>
    {
        public long MinValue { get; set; }
        public long MaxValue { get; set; }
        public long Value { get; private set; }

        /// <inheritdoc/>
        public void DecreaseBy(long deductValue)
        {
            if (IsZeroOrNegative(deductValue))
                return;

            DecreaseValueBy(deductValue);
        }

        private void DecreaseValueBy(long deductValue)
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
        public bool TryDecreaseBy(long deductValue, out long result)
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

        public bool IsDecreasableBy(long deductValue)
        {
            return (Value - deductValue) > MinValue;
        }

        /// <inheritdoc/>
        public void IncreaseBy(long additionalValue)
        {
            if (IsZeroOrNegative(additionalValue))
                return;

            IncreaseValueBy(additionalValue);
        }

        private void IncreaseValueBy(long additionalValue)
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
        public bool TryIncreasableBy(long additionalValue, out long result)
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

        public bool IsIncreasableBy(long additionalValue)
        {
            return (Value + additionalValue) < MaxValue;
        }

        public bool IsZeroOrNegative(long checkValue)
        {
            return checkValue <= 0 ? throw new System.Exception("The value must be positive.") : false;
        }

        /// <inheritdoc/>
        public void SetTo(long newValue)
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