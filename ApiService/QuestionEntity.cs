using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPT.API
{
    class QuestionEntity
    {
        public QuestionEntity(string question="")
        {
            model = "text-davinci-003";
            prompt = question;
            temperature = 0.9;
            max_tokens = 500;
            top_p = 0.5;
            frequency_penalty = 0;
            presence_penalty = 0.6;
            //stop.Add("Human:");
            //stop.Add("AI: ");

        }
        /// <summary>
        /// 
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string prompt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double temperature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int max_tokens { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double top_p { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int frequency_penalty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double presence_penalty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> stop { get; set; }
    }
}
