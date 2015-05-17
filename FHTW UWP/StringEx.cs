using System.Linq;

namespace FHTW_Universal.Logics
{
    public static class StringEx
    {
        public static int CharCount(this string s, char charToFind)
        {
            return s.Count(cc => cc == charToFind);
        }
    }
}
