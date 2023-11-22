﻿using System.Text.Json.Serialization;


namespace ShopTARge22.Core.Dto.ChuckNorrisJokesDtos
{
    public class ChuckNorrisJokesResponseRootDto
    {
        [JsonPropertyName("categories")]
        public List<string> Categories { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

         [JsonPropertyName("value")]
          public string Value { get; set; }
        }

    }
