using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slugify;



namespace Services.BusinessRules
{
    public class SlugService
    {
        private readonly SlugHelper _slugHelper;

        public SlugService()
        {
            var config = new SlugHelper.Config
            {
                ForceLowerCase = true,
                CollapseWhiteSpace = true,
                DeniedCharactersRegex = @"[^a-zA-Z0-9\-\._]"
            };

            config.CharacterReplacements.Add("&", "and");
            config.CharacterReplacements.Add("@", "at");

            _slugHelper = new SlugHelper(config);
        }

        public string Generate(string name)
        {
            return _slugHelper.GenerateSlug(name);
        }
    }
}