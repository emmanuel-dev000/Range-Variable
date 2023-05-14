namespace RangeVariable
{
    public interface IDecrease<T>
    {
        /// <summary>
        /// Subtracts the deduct value to the current value.
        /// The deduct value must be positive.
        /// </summary>
        void DecreaseBy(T TParam);

        /// <summary>
        /// Checks if it is decreasable by deduct value without returning an exception.
        /// </summary>
        bool TryDecreaseBy(T TParam, out T TResult);

        bool IsDecreasableBy(T TParam);
    }

    public interface IIncrease<T>
    {
        /// <summary>
        /// Adds the additional value to the current value.
        /// The additional value must be positive.
        /// </summary>
        void IncreaseBy(T TParam);

        /// <summary>
        /// Checks if it is increasable by additional value without returning an exception.
        /// </summary>
        bool TryIncreasableBy(T TParam, out T TResult);

        bool IsIncreasableBy(T TParam);
    }

    public interface ISet<T>
    {
        /// <summary>
        /// Clamps the value in its min value and max value.
        /// </summary>
        void SetTo(T TParam);

        /// <summary>
        /// Clamps the value to min value.
        /// </summary>
        void ClampToMin();

        /// <summary>
        /// Clamps the value to max value.
        /// </summary>
        void ClampToMax();
    }

    public interface IZeroOrNegative<T>
    {
        bool IsZeroOrNegative(T TParam);
    }
}