namespace wp4me.SnSHTMLUtils
{
    /// <summary>
    /// Class that provides functions to manipulate HTML.
    /// </summary>
    public sealed class SnSHtml
    {
        /*******************************************************/
        /** METHODS AND FUNCTIONS.
        /*******************************************************/
        /// <summary>
        /// Fonction that transforms HTML to text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string HtmlToText(string text)
        {          
            return text.Replace("<br/>", "\n").Replace("&nbsp;", " ");
        }
    }
}
