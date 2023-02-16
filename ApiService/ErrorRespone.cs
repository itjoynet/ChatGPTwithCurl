
namespace ChatGPT.API
{
    public class ErrorRespone
    {
        /// <summary>
        /// 
        /// </summary>
        public Error error { get; set; }
    }
    public class Error
    {
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string param { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
    }

}
