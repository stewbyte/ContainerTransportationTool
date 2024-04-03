namespace ContainerTransportationTool
{
    internal class Validator
    {
        public bool Validate(int integer, int minValue, int maxValue, bool onlyCheckForInteger = true)
        {
            if (integer < minValue || integer > maxValue)
            {
                throw new ArgumentOutOfRangeException("integer", $"Value must be between {minValue} and {maxValue}!");
            }

            if (integer % 1 != 0)
            {
                throw new ArgumentException("integer", "Value must be an integer!");
            }

            if (onlyCheckForInteger && !int.TryParse(integer.ToString(), out _))
            {
                throw new ArgumentException("integer", "Value must be a number!");
            }

            return true;
        }
    }
}