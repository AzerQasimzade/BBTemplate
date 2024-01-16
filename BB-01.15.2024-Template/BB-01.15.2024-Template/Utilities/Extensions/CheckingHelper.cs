using BB_01._15._2024_Template.Areas.BBAdmin.ViewModels;

namespace BB_01._15._2024_Template.Utilities.Extensions
{
    public static class CheckingHelper
    {
        public static bool IssDigit(this RegisterVM userVM,string word)
        {
            return word.Any(char.IsDigit);
        }
        public static bool IssSymbol(this RegisterVM userVM, string word)
        {
            bool result= false;
            foreach(Char letter in word)
            {
                result =Char.IsSymbol(letter);
            }
            return result;
        }
    }
}
