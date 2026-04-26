using Contracts.Repo;
using Entities.Exceptions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BusinessRules
{
    public static  class SellerRules
    {

        public static async Task<(int commodityClass,Guid sellerProfileId)> CommodityClassToSellerRef(Guid sellerId,IRepositoryManager repository)
        {
            int commodityClassId = 0;


                 var sellerProfile = await repository.SellerProfileRepo.FindSellerProfileBySellerId(sellerId);
            if (sellerProfile is null)
            {
                throw new ObjectBadRequestExeption($"Seller Profile for user with id: {sellerId} doesnot exists");

            }
            commodityClassId = sellerProfile.SellerPolicyId switch
            {
                1 => 1,
                2 => 2,
                3 => 3,
                _ => 2,
            };
            return (commodityClassId,sellerProfile.SellerProfileId);

        }



        public static int SetSellerMaximumAllowedItems(int groupCategoryId)
        {
            int maximumAllowedItems = 0;
             maximumAllowedItems = groupCategoryId switch
            {
                1 => 10,
                2 => 20,
                3 => 50,
                _ => 10,
            };
            return maximumAllowedItems;

        }

    }
}
