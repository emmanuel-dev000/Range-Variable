namespace RangeVariable
{
    public struct RangeFloat : IDecrease<float>, IIncrease<float>, ISet<float>, IZeroOrNegative<float>
    {
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
        public float Value { get; private set; }

        /// <inheritdoc/>
        public void DecreaseBy(float deductValue)
        {
            if (IsZeroOrNegative(deductValue))
                return;

            DecreaseValueBy(deductValue);
        }

        private void DecreaseValueBy(float deductValue)
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
        public bool TryDecreaseBy(float deductValue, out float result)
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

        public bool IsDecreasableBy(float deductValue)
        {
            return (Value - deductValue) > MinValue;
        }

        /// <inheritdoc/>
        public void IncreaseBy(float additionalValue)
        {
            if (IsZeroOrNegative(additionalValue))
                return;

            IncreaseValueBy(additionalValue);
        }

        private void IncreaseValueBy(float additionalValue)
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
        public bool TryIncreasableBy(float additionalValue, out float result)
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

        public bool IsIncreasableBy(float additionalValue)
        {
            return (Value + additionalValue) < MaxValue;
        }

        public bool IsZeroOrNegative(float checkValue)
        {
            return checkValue <= 0 ? throw new System.Exception("The value must be positive.") : false;
        }

        /// <inheritdoc/>
        public void SetTo(float newValue)
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