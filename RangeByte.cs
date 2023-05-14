namespace RangeVariable
{
    public struct RangeByte : IDecrease<byte>, IIncrease<byte>, ISet<byte>, IZeroOrNegative<byte>
    {
        public byte MinValue { get; set; }
        public byte MaxValue { get; set; }
        public byte Value { get; private set; }

        /// <inheritdoc/>
        public void DecreaseBy(byte deductValue)
        {
            if (IsZeroOrNegative(deductValue))
                return;

            DecreaseValueBy(deductValue);
        }

        private void DecreaseValueBy(byte deductValue)
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
        public bool TryDecreaseBy(byte deductValue, out byte result)
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

        public bool IsDecreasableBy(byte deductValue)
        {
            return (Value - deductValue) > MinValue;
        }

        /// <inheritdoc/>
        public void IncreaseBy(byte additionalValue)
        {
            if (IsZeroOrNegative(additionalValue))
                return;

            IncreaseValueBy(additionalValue);
        }

        private void IncreaseValueBy(byte additionalValue)
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
        public bool TryIncreasableBy(byte additionalValue, out byte result)
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

        public bool IsIncreasableBy(byte additionalValue)
        {
            return (Value + additionalValue) < MaxValue;
        }

        public bool IsZeroOrNegative(byte checkValue)
        {
            return checkValue <= 0 ? throw new System.Exception("The value must be positive.") : false;
        }

        /// <inheritdoc/>
        public void SetTo(byte newValue)
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