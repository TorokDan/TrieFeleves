using System;

namespace TrieFeleves
{
    public class NotDefinedCharacter : Exception
    {
        public NotDefinedCharacter(char c)
            : base($"\"{c}\" character not defined")
        {
            
        }

        public NotDefinedCharacter(int num)
            : base($"\"{num}\" character not defined")
        {
            
        }
    }
}