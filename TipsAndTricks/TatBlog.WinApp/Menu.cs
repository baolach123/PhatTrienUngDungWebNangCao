using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using TatBlog.WinApp;



namespace TatBlog.WinApp
{
    public class Menu
    {
        public void XuatMenu()
        {
            Console.WriteLine("---------Menu--------");
            Console.WriteLine("0.Thoat");
            Console.WriteLine("1.Tim mot the theo ten dind danh");
            Console.WriteLine("2.Lay tat ca cac the kem theo so bai viet chua the do");
            Console.WriteLine("3.Xoa mot ma the cho truoc");
            Console.WriteLine("4.Tim mot chuyen muc theo ten dinh danh");
            Console.WriteLine("5.Tim mot chuyen muc theo ma so cho truoc");
            Console.WriteLine("6.Them hoac cap nhat mot chuyen muc/ chu de");
            Console.WriteLine("7.Xoa mot chuyen muc theo ma so cho truoc");
            Console.WriteLine("8.Kiem tra ten dinh danh cua mot chuyen muc da ton tai hay chua");
            Console.WriteLine("9.Lay va phan trang danh sach chuyen muc");
            Console.WriteLine("10.Dem so luong bai viet trong N thang gan nhat");
            Console.WriteLine("11.Tim mot bai viet theo ma so");
            Console.WriteLine("12.Them hay cap nhat mot so bai viet");
        }

        public int ChonMenu()
        {
            
            Console.WriteLine("Chon chuc nang:");
            int n = Convert.ToInt32(Console.ReadLine());
            return n;            
        }

        public async void XyLyMenu(int n)
        {
            var context = new BlogDbContext();
            IBlogRepository blogRepo = new BlogRepository(context);          
            switch (n)
            {
                case 0:
                    break;
                case 1:
                    Console.WriteLine("Tim mot the theo ten dind danh:");
                    var tagf = await blogRepo.SeekTagWithUrlslugAync("ADuGoogle");
                    Console.WriteLine(tagf.Name + "");                 
                    break;

                case 2:
                    Console.WriteLine("Lay tat ca cac the kem theo so bai viet chua the do");
                    var tagList = await blogRepo.GetListTagAndAmountOfPostInTagAsync();
                    foreach (var tag in tagList)
                    {
                        Console.WriteLine("{0,-50}{1,-3}", tag.Name, tag.PostCount);                        
                    }
                    break;
                   
                case 3:
                    Console.WriteLine("Xoa mot ma the cho truoc");
                    await blogRepo.RemoveTagByIdAsync(7);
                    break;

                case 4:
                    Console.WriteLine("Tim mot chuyen muc theo ten dinh danh");
                    var category = await blogRepo.SeekCategoryAsync("netcore");     
                    Console.WriteLine(category.Name+"");
                    break;

                case 5:
                    Console.WriteLine("Tim mot chuyen muc theo ma so cho truoc");
                    var categoryA = await blogRepo.SeekCategoryByIdAsync(1);
                    Console.WriteLine(categoryA.Name + "");
                    break;

                case 6:
                    Console.WriteLine("Them hoac cap nhat mot chuyen muc/ chu de");
                    Category category1 = new Category()
                    {
                        Name = "Du2",
                        Description = "Du-Abc",
                        UrlSlug = "DuAbc",
                        Id = -1
                    };
                    
                     await blogRepo.AddOrUpdateCategoryAsysn(category1);
                    break;
            }
        }
    }

}
