namespace 极简浏览器.Api
{
    public static class StandardApi
    {
        public static string GenerateSpace(string gen, int longest = 40)
        {
            int len = longest - gen.Length;
            string spaces = "";
            for(int i = 0;i < len;i ++)
            {
                spaces += " ";
            }
            return spaces;
        }
    }
}
