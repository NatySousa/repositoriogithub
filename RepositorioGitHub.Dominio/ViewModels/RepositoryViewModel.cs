using Newtonsoft.Json;
using RepositorioGitHub.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGitHub.Dominio
{
    public class RepositoryViewModel
    {     
        public long TotalCount { get; set; }

        public GitHubRepository[] Repositories { get; set; }

        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset LastUpdate { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
