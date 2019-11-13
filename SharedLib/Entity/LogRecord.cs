using System;
using Newtonsoft.Json;

namespace ConnectorEntity
{
    public class LogRecord
    {
        /// <summary>
        ///  module Name
        /// </summary>
        [JsonProperty(PropertyName = "module")]
        public string Module { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Success | Fail | Ignored
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// ���� "triggerId" �� �������
        /// </summary>
        [JsonProperty(PropertyName = "trigger_id")]
        public string TriggerId { get; set; }

        /// <summary>
        /// ���� "mainLogId" �� �������
        /// </summary>
        [JsonProperty(PropertyName = "uid")]
        public string Uid { get; set; }

        /// <summary>
        /// ������������ JSON � �������� ������, ��� {}
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        /// <summary>
        /// ���� � ������� ISO
        /// </summary>
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// action id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }


        [JsonProperty(PropertyName = "requestbody")]
        public string requestbody { get; set; }
    }
}