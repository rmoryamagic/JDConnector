using System;


namespace ConnectorEntity
{
    public class QueryRequest
    {
        public string Module { get; set; }
        public string Action { get; set; }
        public string SQLquery { get; set; }
        public string StartPoolingFrom { get; set; }
        public string PoolingColumnType { get; set; }
        public int ChunkSize { get; set; }
        public DateTime LastSync { get; set; }

    }



}
