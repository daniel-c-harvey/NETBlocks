﻿namespace NetBlocks.Models
{
    public abstract class ApiClient<TConfig>
        where TConfig : ClientConfig
    {
        protected TConfig config;
        protected HttpClient http { get; set; }

        public ApiClient(TConfig config)
        {
            this.config = config;
            http = new HttpClient();
            try
            {
                http.BaseAddress = new Uri(config.URL);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}