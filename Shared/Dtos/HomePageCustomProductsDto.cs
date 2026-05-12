using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class HomePageCustomProductsDto
    {
        public ICollection<HomePageProductDto> FeaturedProducts { get; set; } = [];
        public ICollection<HomeProductCardDto> AdvertisedProducts { get; set; } = [];

        public ICollection<HomeProductCardDto> SelectedGroups { get; set; } = [];

        public ICollection<HomePageProductDto> GroupProducts { get; set; } = [];
    }
}