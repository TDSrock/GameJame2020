namespace SjorsGielen.CustomVariables
{
    public abstract class AbstractRangeVariable<T>
    {
        public T MinValue;
        public T MaxValue;

        /// <summary>
        /// Gets a random value between this range variables min(including) and max(excluding)
        /// </summary>
        /// <returns>A random value between this range variables min(including) and max(excluding)</returns>
        abstract public T GetRandomValue();

        abstract public bool IsInRange(T valueToCheckIfIsInRange);

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.MinValue, this.MaxValue);
        }
    }
}