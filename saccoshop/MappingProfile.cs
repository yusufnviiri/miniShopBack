using AutoMapper;
using Entities.Models;
using saccoshop;
using Shared.Dtos;

namespace saccoshop
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {           //source,destination
            CreateMap<NewProductDto, Product>();
            CreateMap<GroupMemberSellerprofileDto, SellerProfile>();

            CreateMap<TradeImageDto, TradeImage>();
            CreateMap<NewTradeDto, Trade>();
            CreateMap<Product, ShowProductDto>();
            CreateMap<ProductImageDto, ProductImage>();
            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, ShowAllCategoriesDto>();
            CreateMap<CategoryDto,SubCategory>();
            CreateMap<SubCategory, ShowAllCategoriesDto>();
            CreateMap<ShowReviewDto, Review>();
            CreateMap<Review, ShowReviewDto>();
            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();
            CreateMap<GroupMemberDto, GroupMember>();
            CreateMap<NewReviewDto, Review>();
            CreateMap<NewUserGroupDto, UserGroup>();
            CreateMap<CartItem,CartItemDto>();
            CreateMap<CartItemDto, CartItem>();
            CreateMap<SubCategoryDto, SubCategory>();
            CreateMap<SubCategory, SubCategory>();
            CreateMap<NewUserDataDto, UserProfile>();
            CreateMap<GroupMemberJoinNewUserProfileDataDto, NewUserDataDto>();
            CreateMap<GroupMemberJoinNewUserProfileDataDto, GroupMemberDto>();

        }
    }
}




    //CreateMap<CreateStudentDto, Student>().ForMember(k => k.DateOfBirth, opt => opt.MapFrom(x => DateTime.Parse(x.DateOfBirth))).AfterMap((src, dest) => dest.SchoolFees=new SchoolFees(src.Fees)); 
    //CreateMap<CreateStudentDto, Student>().ForMember(k => k.DateOfBirth, opt => opt.MapFrom(x => DateTime.Parse(x.DateOfBirth))).ForPath(m => m.SchoolFees.Balance, opt => opt.MapFrom(p => p.SchoolFees)).ForMember(k=>k.SchoolFees.Balance,opt=>opt.MapFrom(k=>k.SchoolFees));
    //CreateMap<CreateStudentDto, Student>().ForMember(k => k.DateOfBirth, opt => opt.MapFrom(x => DateTime.Parse(x.DateOfBirth))).ForSourceMember(src=>src.Fees,opt=>opt.DoNotValidate());
    //CreateMap<CreateStudentDto, Student>().ForMember(k => k.DateOfBirth, opt => opt.MapFrom(x => DateTime.Parse(x.DateOfBirth))).ForSourceMember(src => src.Fees, opt => opt.DoNotValidate());

  
    //CreateMap<CreateNoticeDto, Notice>().ForMember(k => k.DueDate, opt => opt.MapFrom(x => DateTime.Parse(x.DueDate)));
    //CreateMap<Notice, ShowNoticeDto>().ForCtorParam("DueDate",opt => opt.MapFrom(x => $"{x.DueDate.Day}/{x.DueDate.Month}/{x.DueDate.Year}"));
    //CreateMap<CreateAssetDto, Asset>().ForMember(k => k.DateOfPurchase, opt => opt.MapFrom(x => DateTime.Parse(x.DateOfPurchase)));
    //CreateMap<Asset, ShowAssetDto>().ForCtorParam("DateOfPurchase", opt => opt.MapFrom(x => $"{x.DateOfPurchase.Value.Day}/{x.DateOfPurchase.Value.Month}/{x.DateOfPurchase.Value.Year}"));

    //CreateMap<Student, StudentOldData>().ForMember(k => k.StudentId, opt => opt.MapFrom(x => x.Id));
    //CreateMap<CreateStaffDto, Staff>().ForMember(k => k.DateOfBirth, opt => opt.MapFrom(x => DateTime.Parse(x.DateOfBirth)));
  

    //CreateMap<Student, ShowPersonDto>().ForCtorParam("BirthDate",opt => opt.MapFrom(x => $"{x.DateOfBirth.Value.Day}/{x.DateOfBirth.Value.Month}/{x.DateOfBirth.Value.Year}"));
    //CreateMap<Staff, ShowstaffDto>() .ForCtorParam("BirthDate",opt => opt.MapFrom(x => $"{x.DateOfBirth.Value.Day}/{x.DateOfBirth.Value.Month}/{x.DateOfBirth.Value.Year}"));
//CreateMap<Wage,ShowWagesDto>().ForSourceMember(src => src.Staff, opt => opt.DoNotValidate()); ;

