using System.Collections.Generic;

namespace ChatGPT.API
{
    class AnswerEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string @object { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int created { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ChoicesItem> choices { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Usage usage { get; set; }
    }
    public class Usage
    {
        /// <summary>
        /// 
        /// </summary>
        public int prompt_tokens { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int completion_tokens { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total_tokens { get; set; }
    }
    public class ChoicesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int index { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string logprobs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string finish_reason { get; set; }
    }
}
